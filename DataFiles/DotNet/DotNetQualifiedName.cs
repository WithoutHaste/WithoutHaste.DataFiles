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

		/// <summary>Local data type name, written in the c# style.</summary>
		/// <example><![CDATA[MyType<T> instead of MyType`1]]></example>
		public virtual string LocalName { get { return localName; } }

		/// <summary>Local data type name, written in the Xml style.</summary>
		/// <example><![CDATA[MyType`1 instead of MyType<T>]]></example>
		public virtual string LocalXmlName { get { return localName; } }

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

		/// <summary>Builds the qualified name from each segment provided, with the first string being the root namespace and the last string being the local name.</summary>
		/// <exception cref="ArgumentException">List of names cannot be empty.</exception>
		public DotNetQualifiedName(params string[] names)
		{
			if(names == null || names.Length == 0)
				throw new ArgumentException("List of names cannot be empty.");
			this.localName = names.Last();
			if(names.Length > 1)
				this.FullNamespace = new DotNetQualifiedName(names.Take(names.Length - 1).ToArray());
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
		///     <item>All others are parsed as Member names.</item>
		///   </list>
		/// </param>
		/// <exception cref="XmlFormatException">Name does not start with /[TMFPE]:/</exception>
		public static DotNetQualifiedName FromVisualStudioXml(string name)
		{
			if(String.IsNullOrEmpty(name)) return new DotNetQualifiedName();

			if(name.Length < 2 || name[1] != ':') return MemberNameFromVisualStudioXml(name);

			switch(name[0])
			{
				case 'T': return DotNetQualifiedClassName.FromVisualStudioXml(name);
				case 'M': return DotNetQualifiedMethodName.FromVisualStudioXml(name);
				case 'F':
				case 'P':
				case 'E': return MemberNameFromVisualStudioXml(name);
				default: return MemberNameFromVisualStudioXml(name);
			}
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

			return new DotNetQualifiedName(localName, DotNetQualifiedClassName.FromVisualStudioXml(fullNamespace));
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
			return ToString(0);
		}

		/// <example>
		/// For "System.Collections.Generic.List", 
		/// Depth = 0 at "List"
		/// Depth = 1 at "Generic"
		/// Depth = 2 at "Collections"
		/// Depth = 3 at "System"
		/// </example>
		private string ToString(int depth)
		{
			string fullName = (FullNamespace == null) ? LocalName : Combine(FullNamespace.ToString(depth + 1), LocalName);
			return ApplyNameConverter(fullName, depth);
		}

		private string ApplyNameConverter(string fullName, int depth)
		{
			if(DotNetSettings.QualifiedNameConverter != null)
				fullName = DotNetSettings.QualifiedNameConverter(fullName, depth);
			if(DotNetSettings.AdditionalQualifiedNameConverter != null)
				fullName = DotNetSettings.AdditionalQualifiedNameConverter(fullName, depth);
			return fullName;
		}
		
		/// <summary>Names converted to strings must match exactly to be considered equal.</summary>
		public static bool operator ==(DotNetQualifiedName a, DotNetQualifiedName b)
		{
			if(object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
				return true;

			if(object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
				return false;

			return (a.LocalName == b.LocalName && a.FullNamespace == b.FullNamespace);
		}

		/// <duplicate cref="operator ==(DotNetQualifiedName,DotNetQualifiedName)" />
		public static bool operator !=(DotNetQualifiedName a, DotNetQualifiedName b)
		{
			return !(a == b);
		}

		/// <duplicate cref="operator ==(DotNetQualifiedName,DotNetQualifiedName)" />
		public override bool Equals(Object b)
		{
			if(b != null && b is DotNetQualifiedName)
			{
				return (this == (DotNetQualifiedName)b);
			}
			return false;
		}

		/// <duplicate cref="operator ==(DotNetQualifiedName,DotNetQualifiedName)" />
		public override int GetHashCode()
		{
			if(FullNamespace == null)
				return LocalName.GetHashCode();
			return LocalName.GetHashCode() ^ FullNamespace.GetHashCode();
		}

		#endregion
	}
}
