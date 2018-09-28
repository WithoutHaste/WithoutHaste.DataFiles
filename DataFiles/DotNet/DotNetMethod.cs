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
		Static
	};

	/// <summary>
	/// Represents a method.
	/// </summary>
	public class DotNetMethod : DotNetMember
	{
		/// <summary></summary>
		public MethodCategory Category { get; protected set; }

		/// <summary>Fully qualified name of return data type, if known. Null if not known.</summary>
		public DotNetQualifiedTypeName ReturnTypeName { get; protected set; }

		/// <summary></summary>
		public List<DotNetParameter> Parameters = new List<DotNetParameter>();

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetMethod() : base(new DotNetQualifiedName(null))
		{
		}

		/// <summary>Normal constructor</summary>
		public DotNetMethod(DotNetQualifiedName name, List<DotNetParameter> parameters) : base(name)
		{
			this.Parameters.AddRange(parameters);
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

			//parameterless methods don't have a () at all
			int divider = signature.IndexOf('(');
			string name = null;
			string parameters = null;
			if(divider == -1)
			{
				name = signature;
			}
			else
			{
				name = signature.Substring(0, divider);
				parameters = signature.Substring(divider);
			}

			DotNetQualifiedName qualifiedName = DotNetQualifiedName.FromVisualStudioXml(name);

			//for constructors
			bool isConstructor = qualifiedName.LocalName.EndsWith("#ctor");
			if(isConstructor)
			{
				qualifiedName = qualifiedName.SetLocalName(qualifiedName.FullNamespace.LocalName);
			}

			//for operators
			bool isOperator = qualifiedName.LocalName.StartsWith("op_");

			//parse parameters
			List<DotNetParameter> qualifiedParameters = ParametersFromVisualStudioXml(parameters);

			if(isConstructor)
				return new DotNetMethodConstructor(qualifiedName, qualifiedParameters);
			else if(isOperator)
				return new DotNetMethodOperator(qualifiedName, qualifiedParameters);
			else
				return new DotNetMethod(qualifiedName, qualifiedParameters);
		}

		#endregion

		/// <summary>
		/// Parse .Net XML documentation parameter lists.
		/// </summary>
		/// <param name="text">
		/// Expects: null
		/// Expects: empty string
		/// Expects: "(type, type, type)"
		/// </param>
		public static List<DotNetParameter> ParametersFromVisualStudioXml(string text)
		{
			List<DotNetParameter> parameters = new List<DotNetParameter>();
			if(!string.IsNullOrEmpty(text))
			{
				//remove possible { } and possible ( )
				text = text.RemoveOuterBraces(); 
				text = text.RemoveOuterBraces();

				string[] fields = text.SplitIgnoreNested(',');
				for(int i = 0; i < fields.Length; i++)
				{
					string f = fields[i];
					if(!String.IsNullOrEmpty(f))
					{
						parameters.Add(DotNetParameter.FromVisualStudioXml(f));
					}
				}
			}
			return parameters;
		}

		/// <summary>
		/// Returns true if this method's signature matches the reflected MethodInfo.
		/// </summary>
		public bool MatchesSignature(MethodInfo methodInfo)
		{
			if(methodInfo.Name != this.Name.LocalName)
				return false;
			return MatchesArguments(methodInfo.GetParameters());
		}

		/// <summary>
		/// Returns true if this method's parameter list matches the reflected ParameterInfo.
		/// </summary>
		public bool MatchesArguments(ParameterInfo[] otherParameters)
		{
			if(Parameters.Count != otherParameters.Length)
				return false;

			for(int i = 0; i < Parameters.Count; i++)
			{
				string otherName = DotNetQualifiedTypeName.FromAssemblyInfo(otherParameters[i].ParameterType).FullName;
				//todo: something about parameters that end with @ vs &
				if(Parameters[i].FullTypeName != otherName)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(MethodInfo methodInfo)
		{
			if(methodInfo.Attributes.IsStatic())
				Category = MethodCategory.Static;
			else
				Category = MethodCategory.Normal;

			if(methodInfo.ReturnType != null)
				ReturnTypeName = DotNetQualifiedTypeName.FromAssemblyInfo(methodInfo.ReturnType);

			int index = 0;
			foreach(ParameterInfo parameterInfo in methodInfo.GetParameters())
			{
				Parameters[index].AddAssemblyInfo(parameterInfo);
				index++;
			}

			if(Name != null && Name is DotNetQualifiedMethodName)
			{
				(Name as DotNetQualifiedMethodName).AddAssemblyInfo(methodInfo);
			}
		}
	}
}
