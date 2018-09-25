using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a fully qualified type name or member name.
	/// </summary>
	/// <remarks>
	/// Cannot handle types or methods that declare more than 12 generic types,
	/// such as <![CDATA[MyType<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>]]>.
	/// </remarks>
	/// <example>
	///   <para>
	///     How .Net xml documentation formats generic types:
	///     Backtics are followed by integers, identifying generic types.
	///   </para>
	///   <para>
	///     Single backtics (such as `1) on a class declaration indicate a count of generic types for the class.
	///     <example><![CDATA[MyGenericType<T,U,V> is documented as MyGenericType`3]]></example>
	///     Anywhere else within this object's documentation that a single backtic appears, it indicates the index of the generic type in reference to the class declaration.
	///     <example><![CDATA[MyGenericType(T,U,V) is documented as MyGenericType.#ctor(`0,`1,`2)]]></example>
	///   </para>
	///   <para>
	///     Double backtics (such as ``1) on a method name indicate a count of generic types for the method.
	///     <example><![CDATA[MyMethod<A,B,C> is documented as MyMethod``3]]></example>
	///     Anywhere else within this method's documentation that a double backtic appears, it indicates the index of the generic type in reference to the method declaration.
	///     <example><![CDATA[MyMethod<A,B,C>(A,B,C) is documented as MyMethod``3(``0,``1,``2)]]></example>
	///     A method that uses both its own generic types AND generic types from the class declaration will look like this:
	///     <example><![CDATA[MyMethod<A,B,C>(A,B,C,T,U) is documented as MyMethod``3(``0,``1,``2,`0,`1)]]></example>
	///   </para>
	/// </example>
	public class DotNetQualifiedName
	{
		/// <summary>Default names that will be given to generic-types, in order.</summary>
		public static readonly string[] ClassGenericTypeNames = new string[] { "T", "U", "V", "W", "T2", "U2", "V2", "W2", "T3", "U3", "V3", "W3" };

		/// <summary>Default names that will be given to generic-method-types, in order.</summary>
		public static readonly string[] MethodGenericTypeNames = new string[] { "A", "B", "C", "A2", "B2", "C2", "A3", "B3", "C3", "A4", "B4", "C4" };

		/// <summary>Fully qualified namespace.</summary>
		/// <remarks>Null if there is no namespace.</remarks>
		public DotNetQualifiedName FullNamespace { get; protected set; }

		/// <summary>Fully qualified name.</summary>
		public string FullName { get { return ToString(); } }

		/// <summary>Name without namespace.</summary>
		public string LocalName {
			get {
				if(classGenericTypeCount == 0 && methodGenericTypeCount == 0)
					return localName;
				if(classGenericTypeCount > 0)
					return String.Format("{0}<{1}>", localName, String.Join(",", ClassGenericTypeNames.Take(classGenericTypeCount).ToArray()));
				else
					return String.Format("{0}<{1}>", localName, String.Join(",", MethodGenericTypeNames.Take(methodGenericTypeCount).ToArray()));
			}
		}

		private string localName;
		private int classGenericTypeCount;
		private int methodGenericTypeCount;

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedName()
		{
		}

		/// <summary></summary>
		public DotNetQualifiedName(string localName, int classGenericTypeCount = 0, int methodGenericTypeCount = 0)
		{
			ValidateConstructorCounts(classGenericTypeCount, methodGenericTypeCount);
			this.localName = localName;
			this.classGenericTypeCount = classGenericTypeCount;
			this.methodGenericTypeCount = methodGenericTypeCount;
		}

		/// <summary></summary>
		public DotNetQualifiedName(string localName, DotNetQualifiedName fullNamespace, int classGenericTypeCount = 0, int methodGenericTypeCount = 0)
		{
			ValidateConstructorCounts(classGenericTypeCount, methodGenericTypeCount);
			this.localName = localName;
			this.classGenericTypeCount = classGenericTypeCount;
			this.methodGenericTypeCount = methodGenericTypeCount;
			this.FullNamespace = fullNamespace;
		}

		private void ValidateConstructorCounts(int classGenericTypeCount, int methodGenericTypeCount)
		{
			if(classGenericTypeCount < 0)
				throw new ArgumentException("ClassGenericTypeCount cannot be less than 0.", "classGenericTypeCount");
			if(methodGenericTypeCount < 0)
				throw new ArgumentException("MethodGenericTypeCount cannot be less than 0.", "methodGenericTypeCount");
			if(classGenericTypeCount > 0 && methodGenericTypeCount > 0)
				throw new ArgumentException("ClassGenericTypeCount and MethodGenericTypeCount cannot both be greater than 0.");
		}
		
		/// <summary>
		/// Parses a .Net XML documentation type, method, or other member name.
		/// </summary>
		/// <param name="name">
		///   <list>
		///     <item>Names starting with "T:" are parsed as Type names.</item>
		///     <item>Names starting with "M:" are parsed as Method names.</item>
		///     <item>Names starting with "F:" are parsed as Member names.</item>
		///     <item>Names starting with "P:" are parsed as Member names.</item>
		///     <item>Names starting with "E:" are parsed as Member names.</item>
		///   </list>
		/// </param>
		/// <exception cref="XmlFormatException">Name does not start with /[TMFPE]:/</exception>
		public static DotNetQualifiedName FromVisualStudioXml(string name)
		{
			if(String.IsNullOrEmpty(name)) return new DotNetQualifiedName();

			if(name.Length < 2 || name[1] != ':') throw new XmlFormatException(String.Format("Name '{0}' does not start with /[TMFPE]:/", name));

			switch(name[0])
			{
				case 'T': return TypeNameFromVisualStudioXml(name);
				case 'M': return MethodNameFromVisualStudioXml(name);
				case 'F':
				case 'P':
				case 'E': return MemberNameFromVisualStudioXml(name);
				default:
					throw new XmlFormatException(String.Format("Name '{0}' does not start with /[TMFPE]:/", name));
			}
		}

		/// <summary>
		/// Parses a .Net XML documentation parameter type name.
		/// </summary>
		public static DotNetQualifiedName ParameterTypeFromVisualStudioXml(string typeName)
		{
			if(String.IsNullOrEmpty(typeName)) return new DotNetQualifiedName();

			//fully qualified generic type parameters
			//such as System.Collections.Generic.List<T> which are formatted as System.Collections.Generic{`0}
			List<DotNetParameter> parameters = null;
			if(typeName.EndsWith("}"))
			{
				string parameterSignature = typeName.Substring(typeName.IndexOf("{"));
				typeName = typeName.Substring(0, typeName.IndexOf("{"));
				parameters = DotNetMethod.ParametersFromVisualStudioXml(parameterSignature);
			}

			int divider = typeName.LastIndexOf('.');
			string localName = typeName;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = typeName.Substring(divider + 1);
				fullNamespace = typeName.Substring(0, divider);
			}
			else
			{
				//method-generic-type parameters
				int genericTypeIndex = 0;
				if(localName.StartsWith("``"))
				{
					Int32.TryParse(localName.Substring(localName.IndexOf("``") + 2), out genericTypeIndex);
					localName = MethodGenericTypeNames[genericTypeIndex];
				}
				//class-generic-type parameters
				else if(localName.StartsWith("`"))
				{
					Int32.TryParse(localName.Substring(localName.IndexOf('`') + 1), out genericTypeIndex);
					localName = ClassGenericTypeNames[genericTypeIndex];
				}
			}

			if(parameters != null)
			{
				localName += String.Format("<{0}>", String.Join(",", parameters.Select(p => p.TypeName.ToString()).ToArray()));
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedName(localName);

			return new DotNetQualifiedName(localName, TypeNameFromVisualStudioXml(fullNamespace));
		}

		/// <summary>
		/// Parses a .Net XML documentation type name or namespace name.
		/// </summary>
		/// <remarks>
		/// Does not differentiate between types and namespaces 
		/// because a nested type will have other type names in its namespace path
		/// and there are no important diffences in parsing the two.
		/// </remarks>
		/// <param name="name">Name may or may not start with "T:"</param>
		private static DotNetQualifiedName TypeNameFromVisualStudioXml(string name)
		{
			if(name.StartsWith("T:")) name = name.Substring(2);

			int divider = name.LastIndexOf('.');
			string localName = name;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = name.Substring(divider + 1);
				fullNamespace = name.Substring(0, divider);
			}

			int classGenericTypeCount = 0;
			if(localName.Contains("`"))
			{
				Int32.TryParse(localName.Substring(localName.IndexOf('`') + 1), out classGenericTypeCount);
				localName = localName.Substring(0, localName.IndexOf('`'));
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedName(localName, classGenericTypeCount, methodGenericTypeCount: 0);

			return new DotNetQualifiedName(localName, TypeNameFromVisualStudioXml(fullNamespace), classGenericTypeCount, methodGenericTypeCount: 0);
		}

		/// <summary>
		/// Parses a .Net XML documentation method signature.
		/// </summary>
		/// <param name="signature">Name may or may not start with "M:". Includes parameter list.</param>
		private static DotNetQualifiedName MethodNameFromVisualStudioXml(string signature)
		{
			if(signature.StartsWith("M:")) signature = signature.Substring(2);

			string parameters = null;
			if(signature.Contains("("))
			{
				parameters = signature.Substring(signature.IndexOf("("));
				signature = signature.Substring(0, signature.IndexOf("("));
				//parameters are ignored
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

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedName(localName, classGenericTypeCount: 0, methodGenericTypeCount: methodGenericTypeCount);

			return new DotNetQualifiedName(localName, TypeNameFromVisualStudioXml(fullNamespace), classGenericTypeCount: 0, methodGenericTypeCount: methodGenericTypeCount);
		}

		/// <summary>
		/// Parses a .Net XML documentation member name.
		/// </summary>
		/// <remarks>
		/// There is no support for generic types here because .Net XMl documentation does not include member types, just the names.
		/// </remarks>
		/// <param name="name">Name may or may not start with /[FPE]:/</param>
		private static DotNetQualifiedName MemberNameFromVisualStudioXml(string name)
		{
			if(name.StartsWith("F:")) name = name.Substring(2);
			if(name.StartsWith("P:")) name = name.Substring(2);
			if(name.StartsWith("E:")) name = name.Substring(2);

			int divider = name.LastIndexOf('.');
			string localName = name;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = name.Substring(divider + 1);
				fullNamespace = name.Substring(0, divider);
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedName(localName, classGenericTypeCount: 0, methodGenericTypeCount: 0);

			return new DotNetQualifiedName(localName, TypeNameFromVisualStudioXml(fullNamespace), classGenericTypeCount: 0, methodGenericTypeCount: 0);
		}

		//todo parsing parameter types

		#endregion

		/// <summary></summary>
		public DotNetQualifiedName SetLocalName(string name)
		{
			return new DotNetQualifiedName() {
				FullNamespace = this.FullNamespace,
				localName = name
			};
		}

		/// <summary>Return the names combined with a '.' delimiter.</summary>
		public static string Combine(params string[] names)
		{
			return String.Join(".", names);
		}

		/// <summary>Return the names combined with a '.' delimiter.</summary>
		public static string Combine(List<string> names)
		{
			return String.Join(".", names);
		}

		#region Low Level Operations

		/// <summary>Returns dot notation of namespaces and local name.</summary>
		/// <example>A.B.C.LocalName</example>
		public static implicit operator string(DotNetQualifiedName name)
		{
			return name?.ToString();
		}

		/// <summary>Returns dot notation of namespaces and local name.</summary>
		/// <example>A.B.C.LocalName</example>
		public override string ToString()
		{
			if(FullNamespace == null)
			{
				return LocalName;
			}
			return FullNamespace + "." + LocalName;
		}

		#endregion
	}
}
