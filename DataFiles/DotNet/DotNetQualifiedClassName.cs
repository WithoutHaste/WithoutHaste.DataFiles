using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
		public static string[] GenericTypeNames = new string[] { "T", "U", "V", "W", "T2", "U2", "V2", "W2", "T3", "U3", "V3", "W3" };

		/// <summary>Local data type name with generic type parameters (if applicable).</summary>
		public override string LocalName {
			get {
				if(genericTypeCount == 0)
					return localName;
				if(genericTypeAliases != null)
					return String.Format("{0}<{1}>", localName, String.Join(",", genericTypeAliases));
				else
					return String.Format("{0}<{1}>", localName, String.Join(",", GenericTypeNames.Take(genericTypeCount).ToArray()));
			}
		}

		/// <summary>The number of generic-types required by the class declaration.</summary>
		protected int genericTypeCount = 0;

		/// <summary>Specific generic type aliases for this type. If null, the shared <see cref="GenericTypeNames"/> will be used.</summary>
		protected string[] genericTypeAliases;

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedClassName() : base()
		{
		}

		/// <summary></summary>
		public DotNetQualifiedClassName(string localName, int genericTypeCount = 0) : base(localName)
		{
			this.genericTypeCount = genericTypeCount;
		}

		/// <summary></summary>
		public DotNetQualifiedClassName(string localName, DotNetQualifiedName fullNamespace, int genericTypeCount = 0) : base(localName, fullNamespace)
		{
			this.genericTypeCount = genericTypeCount;
		}

		#endregion
		
		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(TypeInfo typeInfo)
		{
			genericTypeAliases = typeInfo.GenericTypeParameters.Select(p => p.Name).ToArray();
			if(typeInfo.DeclaringType != null && isNestedClass())
			{
				(FullNamespace as DotNetQualifiedClassName).AddAssemblyInfo(typeInfo.DeclaringType.GetTypeInfo());
			}
		}

		private bool isNestedClass()
		{
			return (FullNamespace != null && FullNamespace is DotNetQualifiedClassName);
		}

	}
}
