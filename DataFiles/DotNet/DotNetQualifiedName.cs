using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a fully qualified type name or member name.
	/// </summary>
	public class DotNetQualifiedName
	{
		/// <summary>Fully qualified namespace.</summary>
		/// <remarks>Null if there is no namespace.</remarks>
		public DotNetQualifiedName FullNamespace { get; protected set; }

		/// <summary>Fully qualified name.</summary>
		public string FullName { get { return ToString(); } }

		/// <summary>Local data type name.</summary>
		public virtual string LocalName { get { return localName; } }

		/// <summary>Name without namespace or declaring type or generic type parameters.</summary>
		protected string localName;

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedName()
		{
		}

		/// <summary></summary>
		public DotNetQualifiedName(string localName)
		{
			this.localName = localName;
		}

		/// <summary></summary>
		public DotNetQualifiedName(string localName, DotNetQualifiedName fullNamespace)
		{
			this.localName = localName;
			this.FullNamespace = fullNamespace;
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
		/// Parses a .Net XML documentation type name or namespace name.
		/// </summary>
		/// <remarks>
		/// Does not differentiate between types and namespaces 
		/// because a nested type will have other type names in its namespace path
		/// and there are no important diffences in parsing the two.
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
		/// </example>
		/// <param name="name">Name may or may not start with "T:"</param>
		internal static DotNetQualifiedName TypeNameFromVisualStudioXml(string name)
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

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedClassName(localName, classGenericTypeCount);

			return new DotNetQualifiedClassName(localName, TypeNameFromVisualStudioXml(fullNamespace), classGenericTypeCount);
		}

		/// <summary>
		/// Parses a .Net XML documentation method signature.
		/// </summary>
		/// <example>
		///   <para>
		///     How .Net xml documentation formats generic types:
		///     Backtics are followed by integers, identifying generic types.
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

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedMethodName(localName, methodGenericTypeCount);

			return new DotNetQualifiedMethodName(localName, TypeNameFromVisualStudioXml(fullNamespace), methodGenericTypeCount);
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

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedName(localName);

			return new DotNetQualifiedName(localName, TypeNameFromVisualStudioXml(fullNamespace));
		}

		/// <summary>
		/// Parses a System.Reflection.AssemblyInfo full name.
		/// </summary>
		/// <list>
		///   <item>The escape character is '\'</item>
		///   <item>Nested types are separated with '+' instead of '.'</item>
		///   <item>Class declaration of generic types are shown the same as .Net XML documentation: MyType`1 for one generic type</item>
		///   <item>When a generic type is defined: System.Collections.Generic.List`1[U], where U is the type alias from the class declaration</item>
		/// </list>
		public static DotNetQualifiedName FromAssemblyInfo(TypeInfo typeInfo)
		{
			return FromAssemblyInfo(typeInfo.FullName);
		}

		/// <summary>See <see cref="FromAssemblyInfo(TypeInfo)"/></summary>
		public static DotNetQualifiedName FromAssemblyInfo(Type type)
		{
			return FromAssemblyInfo(type.FullName);
		}

		/// <summary>See <see cref="FromAssemblyInfo(TypeInfo)"/></summary>
		public static DotNetQualifiedName FromAssemblyInfo(string typeName)
		{
			typeName = typeName.ReplaceUnescapedCharacters('\\', '+', '.');
			return FromVisualStudioXml("T:" + typeName);
		}

		#endregion

		/// <summary></summary>
		public DotNetQualifiedName SetLocalName(string name)
		{
			return new DotNetQualifiedName() {
				FullNamespace = this.FullNamespace,
				localName = name
			};
		}

		/// <summary>
		/// Collect full list of local names used throughout documentation.
		/// Includes namespaces, internal types, external types, and members.
		/// Does not include generic paremeters.
		/// </summary>
		public virtual List<string> GetFullListOfLocalNames()
		{
			List<string> localNames = new List<string>();

			localNames.Add(localName);
			if(FullNamespace != null)
			{
				localNames.AddRange(FullNamespace.GetFullListOfLocalNames());
			}

			return localNames;
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
