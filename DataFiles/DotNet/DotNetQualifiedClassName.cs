using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a fully qualified class name, for class declarations.
	/// </summary>
	/// <remarks>
	/// Cannot handle classes that declare more than 12 generic types,
	/// such as <![CDATA[MyType<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>]]>.
	/// </remarks>
	public class DotNetQualifiedClassName : DotNetQualifiedName
	{
		/// <summary>Default names that will be given to generic-types, in order.</summary>
		public static readonly string[] DefaultGenericTypeNames = new string[] { "T", "U", "V", "W", "T2", "U2", "V2", "W2", "T3", "U3", "V3", "W3" };

		/// <summary>
		/// Strongly-typed FullNamespace.
		/// </summary>
		public DotNetQualifiedClassName FullClassNamespace {
			get {
				if(FullNamespace == null)
					return null;
				return (FullNamespace as DotNetQualifiedClassName);
			}
		}

		/// <inheritdoc/>
		public override string LocalName {
			get {
				if(GenericTypeCount == 0)
					return localName;
				return String.Format("{0}<{1}>", localName, String.Join(",", genericTypeAliases.Take(GenericTypeCount).ToArray()));
			}
		}

		/// <inheritdoc/>
		public override string LocalXmlName {
			get {
				if(GenericTypeCount == 0)
					return localName;
				return String.Format("{0}`{1}", localName, GenericTypeCount);
			}
		}

		/// <summary>
		/// True if this is a generic class name.
		/// </summary>
		public bool IsGeneric { get { return (GenericTypeCount > 0); } }

		/// <summary>The number of generic-types required by the class declaration.</summary>
		public int GenericTypeCount { get; protected set; }

		/// <summary>
		/// A copy of the ordered list of generic-type aliases used by this class name.
		/// </summary>
		/// <remarks>Non-generic class names may still have default alias values.</remarks>
		public string[] GenericTypeAliases { get { return genericTypeAliases.Select(x => x).ToArray(); } }

		/// <summary>Specific generic type aliases for this method. Defaults to the <see cref='DefaultGenericTypeNames'/> values.</summary>
		protected string[] genericTypeAliases = DefaultGenericTypeNames.Select(x => x).ToArray();

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedClassName() : base()
		{
			GenericTypeCount = 0;
		}

		/// <summary></summary>
		public DotNetQualifiedClassName(string localName, int genericTypeCount = 0) : base(localName)
		{
			GenericTypeCount = genericTypeCount;
		}

		/// <summary></summary>
		public DotNetQualifiedClassName(string localName, DotNetQualifiedClassName fullNamespace, int genericTypeCount = 0) : base(localName, fullNamespace, null)
		{
			GenericTypeCount = genericTypeCount;
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
		///     For example, <![CDATA[MyGenericType<T,U,V>]]> is documented as <c>MyGenericType`3</c>.
		///     Anywhere else within this object's documentation that a single backtic appears, it indicates the zero-based index of the generic type in reference to the class declaration.
		///     For example, the constructor <![CDATA[MyGenericType(T,U,V)]]> is documented as <c>MyGenericType.#ctor(`0,`1,`2)</c>.
		///   </para>
		/// </example>
		/// <param name="name">Name may or may not start with "T:"</param>
		public new static DotNetQualifiedClassName FromVisualStudioXml(string name)
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

			return new DotNetQualifiedClassName(localName, DotNetQualifiedClassName.FromVisualStudioXml(fullNamespace), classGenericTypeCount);
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(Type type)
		{
			genericTypeAliases = type.GetGenericArguments().Select(p => p.Name).ToArray();
			if(type.DeclaringType != null && isNestedClass())
			{
				(FullNamespace as DotNetQualifiedClassName).AddAssemblyInfo(type.DeclaringType);
			}
		}

		private bool isNestedClass()
		{
			return (FullNamespace != null && FullNamespace is DotNetQualifiedClassName);
		}

		#region Low Level

		/// <summary>
		/// Returns deep clone of qualified name.
		/// </summary>
		public new DotNetQualifiedClassName Clone()
		{
			DotNetQualifiedClassName clonedFullNamespace = null;
			if(FullNamespace != null)
				clonedFullNamespace = FullClassNamespace.Clone();

			return new DotNetQualifiedClassName(localName, clonedFullNamespace, GenericTypeCount);
		}

		#endregion
	}
}
