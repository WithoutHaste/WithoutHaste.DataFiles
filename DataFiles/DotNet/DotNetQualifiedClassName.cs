using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a fully qualified class name.
	/// </summary>
	/// <remarks>
	/// Cannot handle classes that declare more than 12 generic types,
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
	/// </example>
	public class DotNetQualifiedClassName : DotNetQualifiedName
	{
		/// <summary>Default names that will be given to generic-types, in order.</summary>
		public static string[] GenericTypeNames = new string[] { "T", "U", "V", "W", "T2", "U2", "V2", "W2", "T3", "U3", "V3", "W3" };

		/// <inheritdoc/>
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

		/// See <see cref="DotNetQualifiedName.SetMisplacedGenericParameters(List{DotNetParameterBase})"/>
		public override void SetMisplacedGenericParameters(List<DotNetParameterBase> misplacedGenericParameters)
		{
			//todo: reveals problem in class design
			//this should only be called on something that accepts full data types, rather than generic type aliases
			//something concrete
			int outerIndex = misplacedGenericParameters.Count - 1;
			if(genericTypeCount > 0)
			{
				int keep = Math.Min(misplacedGenericParameters.Count, genericTypeCount);
				genericTypeAliases = misplacedGenericParameters.Skip(misplacedGenericParameters.Count - keep).Take(keep).Select(x => x.FullTypeName).ToArray();
				outerIndex -= keep;
			}
			if(outerIndex < 0 || FullNamespace == null)
				return;
			FullNamespace.SetMisplacedGenericParameters(misplacedGenericParameters.Take(outerIndex + 1).ToList());
		}

	}
}
