using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a fully qualified method name.
	/// </summary>
	/// <remarks>
	/// Cannot handle methods that declare more than 12 generic types,
	/// such as <![CDATA[MyMethod<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>]]>.
	/// </remarks>
	public class DotNetQualifiedMethodName : DotNetQualifiedName, IComparable
	{
		/// <summary>Default names that will be given to generic-method-types, in order.</summary>
		public static string[] GenericTypeNames = new string[] { "A", "B", "C", "A2", "B2", "C2", "A3", "B3", "C3", "A4", "B4", "C4" };

		/// <summary>Local method name with generic type parameters (if applicable).</summary>
		public override string LocalName {
			get {
				if(GenericTypeCount == 0)
					return localName;
				if(genericTypeAliases != null)
					return String.Format("{0}<{1}>", localName, String.Join(",", genericTypeAliases));
				else
					return String.Format("{0}<{1}>", localName, String.Join(",", GenericTypeNames.Take(GenericTypeCount).ToArray()));
			}
		}

		/// <inheritdoc/>
		public override string LocalXmlName {
			get {
				if(GenericTypeCount == 0)
					return localName;
				return String.Format("{0}``{1}", localName, GenericTypeCount);
			}
		}

		/// <summary>Returns parameter list formatted as: (TypeA a, TypeB b)</summary>
		public string ParametersWithNames {
			get {
				return String.Format("({0})", String.Join(", ", Parameters.Select(p => p.SignatureWithName).ToArray()));
			}
		}

		/// <summary>Returns parameter list formatted as: (TypeA, TypeB)</summary>
		public string ParametersWithoutNames {
			get {
				return String.Format("({0})", String.Join(", ", Parameters.Select(p => p.SignatureWithoutName).ToArray()));
			}
		}

		/// <summary>True for generic methods.</summary>
		public bool IsGeneric { get { return (GenericTypeCount > 0); } }

		/// <summary>The number of generic-types required by the method declaration.</summary>
		public int GenericTypeCount { get; protected set; }

		/// <summary>Specific generic type aliases for this method. If null, the shared <see cref="GenericTypeNames"/> will be used.</summary>
		protected string[] genericTypeAliases;

		/// <summary></summary>
		public List<DotNetParameter> Parameters = new List<DotNetParameter>();

		/// <summary>Fully qualified name of return data type, if known. Null if not known.</summary>
		public DotNetQualifiedTypeName ReturnTypeName { get; protected set; }

		/// <summary>
		/// True if the return type is necessary for distinguishing this method name from others.
		/// </summary>
		/// <example>True for implicit and explicit conversion operators.</example>
		public bool ReturnTypeIsPartOfSignature {
			get {
				return (LocalName == "op_Implicit" || LocalName == "op_Explicit");
			}
		}

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedMethodName() : base()
		{
			GenericTypeCount = 0;
		}

		/// <summary></summary>
		public DotNetQualifiedMethodName(string localName, List<DotNetParameter> parameters, DotNetQualifiedTypeName returnTypeName = null, int genericTypeCount = 0, DotNetQualifiedName explicitInterface = null) : base(localName, explicitInterface)
		{
			GenericTypeCount = genericTypeCount;
			Parameters.AddRange(parameters);
			ReturnTypeName = returnTypeName;
		}

		/// <summary></summary>
		public DotNetQualifiedMethodName(string localName, DotNetQualifiedName fullNamespace, List<DotNetParameter> parameters, DotNetQualifiedTypeName returnTypeName = null, int genericTypeCount = 0, DotNetQualifiedName explicitInterface = null) : base(localName, fullNamespace, explicitInterface)
		{
			GenericTypeCount = genericTypeCount;
			Parameters.AddRange(parameters);
			ReturnTypeName = returnTypeName;
		}

		/// <summary></summary>
		public DotNetQualifiedMethodName(DotNetQualifiedName name, DotNetQualifiedName explicitInterface = null) : base(name.LocalName, name.FullNamespace, explicitInterface)
		{
			GenericTypeCount = 0;
		}

		/// <summary>
		/// Parses a .Net XML documentation method signature.
		/// </summary>
		/// <example>
		///     How .Net xml documentation formats generic types:
		///     Backtics are followed by integers, identifying generic types.
		///
		///     Double backtics (such as ``1) on a method name indicate a count of generic types for the method.
		///     Example: <![CDATA[MyMethod<A,B,C> is documented as MyMethod``3]]>
		///     
		///		Anywhere else within this method's documentation that a double backtic appears, it indicates the index of the generic type in reference to the method declaration.
		///     Example: <![CDATA[MyMethod<A,B,C>(A,B,C) is documented as MyMethod``3(``0,``1,``2)]]>
		///     
		///		A method that uses both its own generic types AND generic types from the class declaration will look like this:
		///     Example: <![CDATA[MyMethod<A,B,C>(A,B,C,T,U) is documented as MyMethod``3(``0,``1,``2,`0,`1)]]>
		/// </example>
		/// <example>
		///		How .Net xml documentation formats implicit and explicit operators:
		///		
		///		static explicit operator int(MyClass a)
		///		becomes
		///		MyClass.op_Explicit(MyClass)~System.Int32
		///		
		///		static implicit operator int(MyClass a)
		///		becomes
		///		MyClass.op_Implicit(MyClass)~System.Int32
		/// </example>
		/// <param name="signature">Name may or may not start with "M:". Includes parameter list.</param>
		public new static DotNetQualifiedMethodName FromVisualStudioXml(string signature)
		{
			if(signature.StartsWith("M:")) signature = signature.Substring(2);
			if(signature.StartsWith("P:")) signature = signature.Substring(2); //because of indexers

			string returnType = null;
			DotNetQualifiedTypeName qualifiedReturnType = null;
			if(signature.Contains("~"))
			{
				returnType = signature.Substring(signature.IndexOf("~") + 1);
				signature = signature.Substring(0, signature.IndexOf("~"));
				qualifiedReturnType = DotNetQualifiedTypeName.FromVisualStudioXml(returnType);
			}

			string parameters = null;
			List<DotNetParameter> qualifiedParameters = new List<DotNetParameter>();
			if(signature.Contains("("))
			{
				parameters = signature.Substring(signature.IndexOf("("));
				signature = signature.Substring(0, signature.IndexOf("("));
				qualifiedParameters = ParametersFromVisualStudioXml(parameters);
			}


			int divider = signature.LastIndexOf('.');
			string localName = signature;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = signature.Substring(divider + 1);
				fullNamespace = signature.Substring(0, divider);
			}

			int methodGenericTypeCount = 0;
			if(localName.Contains("``"))
			{
				Int32.TryParse(localName.Substring(localName.IndexOf("``") + 2), out methodGenericTypeCount);
				localName = localName.Substring(0, localName.IndexOf("``"));
			}

			DotNetQualifiedName explicitInterface = null;
			if(localName.Contains("#") && !localName.Contains("#ctor") && !localName.Contains("#cctor"))
			{
				int lastIndex = localName.LastIndexOf("#");
				string interfaceName = localName.Substring(0, lastIndex).Replace("#", ".");
				explicitInterface = DotNetQualifiedName.FromVisualStudioXml(interfaceName);
				localName = localName.Substring(lastIndex + 1);
			}

			if(String.IsNullOrEmpty(fullNamespace))
				return new DotNetQualifiedMethodName(localName, qualifiedParameters, qualifiedReturnType, methodGenericTypeCount, explicitInterface);

			return new DotNetQualifiedMethodName(localName, DotNetQualifiedClassName.FromVisualStudioXml(fullNamespace), qualifiedParameters, qualifiedReturnType, methodGenericTypeCount, explicitInterface);
		}

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

		#endregion

		/// <summary>
		/// Returns true if this method's signature matches the reflected MethodInfo.
		/// </summary>
		public bool MatchesSignature(MethodInfo methodInfo)
		{
			if(IsGeneric)
			{
				if(methodInfo.Name + "``" + methodInfo.GetGenericArguments().Length != DotNetQualifiedName.Combine(ExplicitInterface, LocalXmlName))
					return false;
			}
			else
			{
				if(methodInfo.Name != DotNetQualifiedName.Combine(ExplicitInterface, LocalName))
					return false;
			}

			if(ReturnTypeIsPartOfSignature)
			{
				if(ReturnTypeName != DotNetQualifiedTypeName.FromAssemblyInfo(methodInfo.ReturnType))
					return false;
			}

			return MatchesArguments(methodInfo.GetParameters());
		}

		/// <summary>
		/// Returns true if this method's signature matches the other method signature.
		/// </summary>
		public bool MatchesSignature(DotNetQualifiedMethodName other)
		{
			if((this as DotNetQualifiedName) != (other as DotNetQualifiedName))
				return false;

			if(this.GenericTypeCount != other.GenericTypeCount)
				return false;

			if(ReturnTypeIsPartOfSignature)
			{
				if(this.ReturnTypeName != other.ReturnTypeName)
					return false;
			}

			return MatchesArguments(other.Parameters);
		}

		/// <summary>
		/// Returns true if this method's parameter list matches the reflected ParameterInfo. Checks parameter types, not names.
		/// </summary>
		public bool MatchesArguments(ParameterInfo[] otherParameters)
		{
			if(Parameters.Count != otherParameters.Length)
				return false;

			for(int i = 0; i < Parameters.Count; i++)
			{
				string otherName = DotNetQualifiedTypeName.FromAssemblyInfo(otherParameters[i].ParameterType).FullName;
				if(Parameters[i].FullTypeName != otherName)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Returns true if this method's parameter list matches the provided parameter list. Checks parameter types, not names.
		/// </summary>
		public bool MatchesArguments(List<DotNetParameter> otherParameters)
		{
			if(Parameters.Count != otherParameters.Count)
				return false;

			for(int i = 0; i < Parameters.Count; i++)
			{
				if(Parameters[i].TypeName.FullName != otherParameters[i].TypeName.FullName)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(MethodInfo methodInfo)
		{
			if(IsGeneric)
			{
				Type[] genericTypes = methodInfo.GetGenericArguments();
				this.genericTypeAliases = genericTypes.Select(g => g.ToString()).ToArray();
			}

			if(methodInfo.DeclaringType != null && FullNamespace != null && FullNamespace is DotNetQualifiedClassName)
			{
				(FullNamespace as DotNetQualifiedClassName).AddAssemblyInfo(methodInfo.DeclaringType.GetTypeInfo());
			}

			if(methodInfo.ReturnType != null)
			{
				ReturnTypeName = DotNetQualifiedTypeName.FromAssemblyInfo(methodInfo.ReturnType);
			}

			//if parameter type name is already known, keep it (required for generic type names)
			//if parameter is totally unknown, add it (required for delegates)
			//always add the parameter name
			int index = 0;
			foreach(ParameterInfo parameterInfo in methodInfo.GetParameters())
			{
				if(this.Parameters.Count <= index)
				{
					this.Parameters.Add(new DotNetParameter(DotNetQualifiedTypeName.FromAssemblyInfo(parameterInfo.ParameterType)));
				}
				this.Parameters[index].AddAssemblyInfo(parameterInfo);
				index++;
			}
		}

		/// <summary>Set the local name of the method. Does not affect generic type parameters or method parameters.</summary>
		public void SetLocalName(string name)
		{
			this.localName = name;
		}


		#region Low Level

		/// <duplicate cref='CompareTo(object)' />
		public static bool operator <(DotNetQualifiedMethodName a, DotNetQualifiedMethodName b)
		{
			return (a.CompareTo(b) == -1);
		}

		/// <duplicate cref='CompareTo(Object)' />
		public static bool operator >(DotNetQualifiedMethodName a, DotNetQualifiedMethodName b)
		{
			return (a.CompareTo(b) == 1);
		}

		/// <summary>
		/// Methods are sorted:
		/// <list type='numbered'>
		///		<item>alphabetically by namespace</item>
		///		<item>alphabetically by explicit interface implementation</item>
		///		<item>then parameter list, shortest to longest</item>
		///		<item>then alphabetically by parameter types</item>
		///		<item>then alphabetically by return type (for some operators)</item>
		/// </list>
		/// </summary>
		public override int CompareTo(object b)
		{
			if(!(b is DotNetQualifiedMethodName))
				return -1;
			DotNetQualifiedMethodName other = (b as DotNetQualifiedMethodName);

			if(this.FullNamespace != other.FullNamespace)
				return this.FullNamespace.CompareTo(other.FullNamespace);

			if(this.LocalName != other.LocalName)
			{
				return this.LocalName.CompareTo(other.LocalName);
			}

			if(this.ExplicitInterface != null)
			{
				return this.ExplicitInterface.CompareTo(other.ExplicitInterface);
			}
			if(other.ExplicitInterface != null)
			{
				return -1;
			}

			if(this.Parameters.Count != other.Parameters.Count)
				return this.Parameters.Count.CompareTo(other.Parameters.Count);

			for(int i = 0; i < this.Parameters.Count; i++)
			{
				if(this.Parameters[i].TypeName == other.Parameters[i].TypeName)
					continue;
				return this.Parameters[i].TypeName.CompareTo(other.Parameters[i].TypeName);
			}

			return DotNetQualifiedName.Compare(this.ReturnTypeName, other.ReturnTypeName);
		}

		#endregion
	}
}
