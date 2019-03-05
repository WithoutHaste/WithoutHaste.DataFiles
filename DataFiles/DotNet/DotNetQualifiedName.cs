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
	public class DotNetQualifiedName : IComparable
	{
		/// <summary>Fully qualified namespace.</summary>
		/// <remarks>Null if there is no namespace.</remarks>
		public DotNetQualifiedName FullNamespace { get; protected set; }

		/// <summary>Fully qualified name.</summary>
		public string FullName { get { return ToString(); } }

		/// <summary>Local data type name, written in the c# style.</summary>
		/// <example><![CDATA[MyType<T> instead of MyType`1]]></example>
		public virtual string LocalName {
			get {
				return localName;
			}
		}

		/// <summary>Local data type name, written in the Xml style.</summary>
		/// <example><![CDATA[MyType`1 instead of MyType<T>]]></example>
		public virtual string LocalXmlName { get { return localName; } }

		/// <summary>Name without namespace or declaring type or generic type parameters.</summary>
		protected string localName;

		/// <summary>
		/// The interface being implemented, if this is a property or method with an explicit interface implementation.
		/// </summary>
		public DotNetQualifiedName ExplicitInterface { get; protected set; }

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedName()
		{
		}

		/// <summary></summary>
		public DotNetQualifiedName(string localName, DotNetQualifiedName explicitInterface = null)
		{
			this.localName = localName;
			ExplicitInterface = explicitInterface;
		}

		/// <summary></summary>
		public DotNetQualifiedName(string localName, DotNetQualifiedName fullNamespace, DotNetQualifiedName explicitInterface = null) : this(localName, explicitInterface)
		{
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
		public static DotNetQualifiedName FromVisualStudioXml(string name)
		{
			if(String.IsNullOrEmpty(name))
				return new DotNetQualifiedName();

			if(name.Length < 2 || name[1] != ':')
				return MemberNameFromVisualStudioXml(name);

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
		/// <remarks>
		/// Does not parse method names; use DotNetQualifiedMethodName.FromVisualStudioXml(string) instead.
		/// </remarks>
		/// <example>
		/// Expected formats:
		/// - "F:NamespaceA.NamespaceB.MemberC"
		/// - "P:NamespaceA.NamespaceB.MemberC"
		/// - "E:NamespaceA.NamespaceB.MemberC"
		/// - "NamespaceA.NamespaceB.MemberC"
		/// - "NamespaceA.NamespaceB.InterfaceNamespace#Interface#MemberC"
		/// </example>
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

			DotNetQualifiedName explicitInterface = null;
			if(localName.Contains("#"))
			{
				int lastIndex = localName.LastIndexOf("#");
				string interfaceName = localName.Substring(0, lastIndex).Replace("#", ".");
				explicitInterface = DotNetQualifiedName.FromVisualStudioXml(interfaceName);
				localName = localName.Substring(lastIndex + 1);
			}

			if(String.IsNullOrEmpty(fullNamespace))
				return new DotNetQualifiedName(localName, explicitInterface);

			return new DotNetQualifiedName(localName, DotNetQualifiedClassName.FromVisualStudioXml(fullNamespace), explicitInterface);
		}

		/// <summary>
		/// Parses a System.Reflection.AssemblyInfo full name.
		/// </summary>
		/// <list>
		///   <item>The escape character is backslash (\)</item>
		///   <item>Nested types are separated with '+' instead of '.'</item>
		///   <item>Class declaration of generic types are shown the same as .Net XML documentation: MyType`1 for one generic type</item>
		///   <item>When a generic type is defined: System.Collections.Generic.List`1[U], where U is the type alias from the class declaration</item>
		/// </list>
		public static DotNetQualifiedName FromAssemblyInfo(Type type)
		{
			return FromAssemblyInfo(type.FullName);
		}

		/// <summary>See <see cref="FromAssemblyInfo(Type)"/></summary>
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
		internal virtual List<string> GetFullListOfLocalNames()
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
			return String.Join(".", names.Where(n => !String.IsNullOrEmpty(n)));
		}

		/// <summary>Return the names combined with a '.' delimiter.</summary>
		public static string Combine(List<string> names)
		{
			return String.Join(".", names.Where(n => !String.IsNullOrEmpty(n)));
		}

		/// <summary>Returns true if this Name is nested inside the other Name.</summary>
		/// <example>"System.Text.RegularExpressions" is within "System.Text" and "System".</example>
		/// <example>"System" is not within null or empty Name.</example>
		public bool IsWithin(DotNetQualifiedName other)
		{
			if(this.FullNamespace == null || other == null)
				return false;
			if(this.FullNamespace == other)
				return true;
			return this.FullNamespace.IsWithin(other);
		}

		/// <summary>
		/// Returns a new name object which has been localized to the provided other name. The current object is not altered.
		/// </summary>
		public virtual DotNetQualifiedName GetLocalized(DotNetQualifiedName other)
		{
			DotNetQualifiedName clone = this.Clone();
			clone.Localize(other);
			return clone;
		}

		/// <summary>
		/// Simplifies this qualified name based on the <paramref name='other'/> name.
		/// In other words, removes the portion of the namespace that this and the <paramref name='other'/> have in common.
		/// Alters the current object.
		/// </summary>
		/// <remarks>Will always keep at least the LocalName.</remarks>
		/// <remarks>Preserves explicit interface implementations.</remarks>
		/// <example>"System.Collections.Generic.List".Localize("System.Collections") returns "Generic.List".</example>
		/// <example>"System.Collections.Generic.List".Localize("System.Collections.Standard.List") returns "Standard.List".</example>
		/// <example>"System.Collections.Generic.List".Localize("System.Collections.Generic.List") returns "List".</example>
		public virtual void Localize(DotNetQualifiedName other)
		{
			if(FullNamespace == null || other == null)
				return;

			if(FullNamespace == other || other.IsWithin(FullNamespace))
			{
				FullNamespace = null;
			}
			else
			{
				FullNamespace.Localize(other);
			}
		}

		/// <summary>
		/// Returns an array of the name segments that make up this qualified name.
		/// </summary>
		/// <remarks>Does not include explicit interface implementations.</remarks>
		/// <example>"System.Collections.Generic".Flatten() returns ["System", "Collections", "Generic"].</example>
		public string[] Flatten()
		{
			if(FullNamespace == null)
				return new string[] { LocalName };

			List<string> segments = new List<string>(FullNamespace.Flatten());
			segments.Add(LocalName);
			return segments.ToArray();
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

		/// <duplicate cref="Equals(object)" />
		public static bool operator ==(DotNetQualifiedName a, DotNetQualifiedName b)
		{
			if(object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
				return true;
			if(object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
				return false;
			return a.Equals(b);
		}

		/// <duplicate cref="Equals(object)" />
		public static bool operator !=(DotNetQualifiedName a, DotNetQualifiedName b)
		{
			if(object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
				return false;
			if(object.ReferenceEquals(a, null) || object.ReferenceEquals(b, null))
				return true;
			return !a.Equals(b);
		}

		/// <summary>Names converted to strings must match exactly to be considered equal.</summary>
		public override bool Equals(Object b)
		{
			if(!(b is DotNetQualifiedName))
				return false;
			if(object.ReferenceEquals(this, null) && object.ReferenceEquals(b, null))
				return true;
			if(object.ReferenceEquals(this, null) || object.ReferenceEquals(b, null))
				return false;

			DotNetQualifiedName other = (b as DotNetQualifiedName);
			return (this.LocalName == other.LocalName && this.FullNamespace == other.FullNamespace && this.ExplicitInterface == other.ExplicitInterface);
		}

		/// <summary></summary>
		public override int GetHashCode()
		{
			if(FullNamespace == null) return LocalName.GetHashCode();
			return LocalName.GetHashCode() ^ FullNamespace.GetHashCode();
		}

		/// <duplicate cref='CompareTo(object)' />
		public static bool operator <(DotNetQualifiedName a, DotNetQualifiedName b)
		{
			if(object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
				return false;
			if(object.ReferenceEquals(a, null))
				return true;
			return (a.CompareTo(b) == -1);
		}

		/// <duplicate cref='CompareTo(object)' />
		public static bool operator >(DotNetQualifiedName a, DotNetQualifiedName b)
		{
			if(object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null))
				return false;
			if(object.ReferenceEquals(a, null))
				return false;
			return (a.CompareTo(b) == 1);
		}

		/// <duplicate cref='CompareTo(object)' />
		/// With protection against either object being null.
		public static int Compare(DotNetQualifiedName a, DotNetQualifiedName b)
		{
			if(a > b) return 1;
			if(a < b) return -1;

			return 0;
		}

		/// <summary>
		/// Names are sorted alphabetically, per namespace, starting with the root.
		/// </summary>
		/// <remarks>Explicit interface implementations are considered only as a last resort.</remarks>
		public virtual int CompareTo(object b)
		{
			if(!(b is DotNetQualifiedName))
				return -1;
			if(this.Equals(b))
				return 0;
			if(object.ReferenceEquals(this, null))
				return -1;
			if(object.ReferenceEquals(b, null))
				return 1;

			DotNetQualifiedName other = (b as DotNetQualifiedName);
			string[] thisNames = this.ToString().SplitIgnoreNested('.');
			string[] otherNames = other.ToString().SplitIgnoreNested('.');
			for(int i = 0; i < thisNames.Length; i++)
			{
				if(i >= otherNames.Length)
					return 1;
				if(thisNames[i] == otherNames[i])
					continue;
				return thisNames[i].CompareTo(otherNames[i]);
			}
			if(thisNames.Length < otherNames.Length)
				return -1;

			if(this.ExplicitInterface != null)
				return this.ExplicitInterface.CompareTo(other.ExplicitInterface);
			if(other.ExplicitInterface != null)
				return -1;

			return 0;
		}

		/// <summary>
		/// Returns deep clone of qualified name.
		/// </summary>
		public virtual DotNetQualifiedName Clone()
		{
			DotNetQualifiedName clonedFullNamespace = null;
			if(FullNamespace != null)
				clonedFullNamespace = FullNamespace.Clone();

			DotNetQualifiedName clonedExplicitInterface = null;
			if(ExplicitInterface != null)
				clonedExplicitInterface = ExplicitInterface.Clone();

			return new DotNetQualifiedName(localName, clonedFullNamespace, clonedExplicitInterface);
		}

		/// <summary>
		/// Convert a base-type DotNetQualifiedName to a DotNetQualifiedTypeName.
		/// </summary>
		public DotNetQualifiedTypeName ToDotNetQualifiedTypeName()
		{
			if(FullNamespace == null)
				return new DotNetQualifiedTypeName(localName);
			return new DotNetQualifiedTypeName(localName, FullNamespace.ToDotNetQualifiedTypeName());
		}

		#endregion
	}
}
