using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary></summary>
	public enum MethodCategory
	{
		/// <summary>Not enough information is available to determine method category.</summary>
		Unknown = 0,
		/// <summary>No special category.</summary>
		Normal,
		/// <summary>Static method.</summary>
		Static,
		/// <summary>Static extension method.</summary>
		Extension,
		/// <summary>Protected method.</summary>
		Protected,
		/// <summary>Abstract method.</summary>
		Abstract,
		/// <summary>Virtual method.</summary>
		Virtual,
		/// <summary>Delegate type.</summary>
		Delegate
	};

	/// <summary>
	/// Represents a method.
	/// </summary>
	public class DotNetMethod : DotNetMember
	{
		/// <summary></summary>
		public MethodCategory Category { get; protected set; }

		/// <summary>Strongly typed name.</summary>
		public DotNetQualifiedMethodName MethodName { get { return (Name as DotNetQualifiedMethodName); } }

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetMethod() : base(new DotNetQualifiedMethodName())
		{
		}

		/// <summary></summary>
		public DotNetMethod(DotNetQualifiedMethodName name) : base(name)
		{
		}

		/// <summary>
		/// Parse .Net XML documentation for method signature data.
		/// </summary>
		/// <param name="memberElement">Expects tag "member".</param>
		public static DotNetMethod FromVisualStudioXml(XElement memberElement)
		{
			string signature = memberElement.Attribute("name")?.Value;
			if(signature == null)
				return new DotNetMethod();

			DotNetQualifiedMethodName methodName = DotNetQualifiedMethodName.FromVisualStudioXml(signature);

			//for constructors
			bool isConstructor = (methodName.LocalName.EndsWith("#ctor") || methodName.LocalName.EndsWith("#cctor"));
			bool isStatic = methodName.LocalName.EndsWith("#cctor");

			//for destructors
			bool isDestructor = (methodName.LocalName == "Finalize" && methodName.Parameters.Count == 0);

			//for operators
			bool isOperator = methodName.LocalName.StartsWith("op_");

			DotNetMethod method = null;
			if(isConstructor)
				method = new DotNetMethodConstructor(methodName, isStatic);
			else if(isDestructor)
				method = new DotNetMethodDestructor(methodName);
			else if(isOperator)
				method = new DotNetMethodOperator(methodName);
			else
				method = new DotNetMethod(methodName);

			method.ParseVisualStudioXmlDocumentation(memberElement);

			return method;
		}

		#endregion

		/// <summary>
		/// Returns true if this member's name matches the provided name.
		/// </summary>
		public bool Is(DotNetQualifiedName name)
		{
			if(!(name is DotNetQualifiedMethodName))
				return false;
			return this.MethodName.MatchesSignature(name as DotNetQualifiedMethodName);
		}

		/// <duplicate cref='DotNetQualifiedMethodName.MatchesSignature(MethodInfo)'/>
		public bool MatchesSignature(MethodInfo methodInfo)
		{
			return MethodName.MatchesSignature(methodInfo);
		}

		/// <duplicate cref='DotNetQualifiedMethodName.MatchesSignature(DotNetQualifiedMethodName)'/>
		public bool MatchesSignature(DotNetQualifiedMethodName name)
		{
			return MethodName.MatchesSignature(name);
		}

		/// <duplicate cref='DotNetQualifiedMethodName.MatchesLocalSignature(DotNetQualifiedMethodName)'/>
		public bool MatchesLocalSignature(DotNetQualifiedMethodName name)
		{
			return MethodName.MatchesLocalSignature(name);
		}

		/// <duplicate cref='DotNetCommentMethodLink.MatchesSignature(DotNetMethod)' />
		public bool MatchesSignature(DotNetCommentMethodLink link)
		{
			return link.MatchesSignature(this);
		}

		/// <duplicate cref='DotNetQualifiedMethodName.MatchesArguments(ParameterInfo[])'/>
		public bool MatchesArguments(ParameterInfo[] parameters)
		{
			return MethodName.MatchesArguments(parameters);
		}

		/// <duplicate cref='DotNetQualifiedMethodName.MatchesArguments(List{DotNetParameter})'/>
		public bool MatchesArguments(List<DotNetParameter> parameters)
		{
			return MethodName.MatchesArguments(parameters);
		}


		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(MethodInfo methodInfo)
		{
			if(Category == MethodCategory.Unknown || Category == MethodCategory.Normal)
			{
				if(methodInfo.IsStatic)
				{
					Category = MethodCategory.Static;
					if(methodInfo.IsExtension())
						Category = MethodCategory.Extension;
				}
				else if(methodInfo.IsAbstract)
					Category = MethodCategory.Abstract;
				else if(methodInfo.IsVirtual && !methodInfo.IsFinal)
					Category = MethodCategory.Virtual;
				else if(methodInfo.IsFamily)
					Category = MethodCategory.Protected;
				else
					Category = MethodCategory.Normal;
			}

			if(MethodName != null)
			{
				MethodName.AddAssemblyInfo(methodInfo);
			}
		}

		/// <summary>
		/// Collect full list of local names used throughout documentation.
		/// Includes namespaces, internal types, external types, and members.
		/// </summary>
		/// <returns></returns>
		internal List<string> GetFullListOfLocalNames()
		{
			List<string> localNames = new List<string>();

			localNames.AddRange(Name.GetFullListOfLocalNames());
			foreach(DotNetParameter parameter in MethodName.Parameters.OfType<DotNetParameter>().Cast<DotNetParameter>())
			{
				localNames.AddRange(parameter.TypeName.GetFullListOfLocalNames());
			}

			return localNames;
		}

		#region Low Level

		/// <duplicate cref='Equals(Object)' />
		public static bool operator ==(DotNetMethod a, DotNetMethod b)
		{
			if(object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
				return true;
			if(object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
				return false;
			return a.Equals(b);
		}

		/// <duplicate cref='Equals(Object)' />
		public static bool operator !=(DotNetMethod a, DotNetMethod b)
		{
			if(object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
				return false;
			if(object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
				return true;
			return !a.Equals(b);
		}

		/// <summary>Equality is based on the full namespace/name/generic-type-parameters of the method, and on parameter-types.</summary>
		public override bool Equals(Object b)
		{
			if(!(b is DotNetMethod))
				return false;
			if(object.ReferenceEquals(this, null) && object.ReferenceEquals(b, null))
				return true;
			if(object.ReferenceEquals(this, null) || object.ReferenceEquals(b, null))
				return false;

			DotNetMethod other = (b as DotNetMethod);
			if(this.Name != other.Name)
				return false;
			if(this.MethodName.Parameters.Count != other.MethodName.Parameters.Count)
				return false;
			for(int i = 0; i < this.MethodName.Parameters.Count; i++)
			{
				if(this.MethodName.Parameters[i].TypeName != other.MethodName.Parameters[i].TypeName)
					return false;
			}
			return true;
		}

		/// <summary></summary>
		public override int GetHashCode()
		{
			return Name.GetHashCode() & MethodName.Parameters.GetHashCode();
		}

		#endregion
	}
}