using System;
using System.Collections.Generic;
using System.Linq;
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
	public class DotNetQualifiedMethodName : DotNetQualifiedName
	{
		/// <summary>Default names that will be given to generic-method-types, in order.</summary>
		public static string[] GenericTypeNames = new string[] { "A", "B", "C", "A2", "B2", "C2", "A3", "B3", "C3", "A4", "B4", "C4" };

		/// <inheritdoc/>
		public override string LocalName {
			get {
				if(genericTypeCount == 0)
					return localName;
				return String.Format("{0}<{1}>", localName, String.Join(",", GenericTypeNames.Take(genericTypeCount).ToArray()));
			}
		}

		/// <summary>The number of generic-types required by the method declaration.</summary>
		protected int genericTypeCount = 0;

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedMethodName() : base()
		{
		}

		/// <summary></summary>
		public DotNetQualifiedMethodName(string localName, int genericTypeCount = 0) : base(localName)
		{
			this.genericTypeCount = genericTypeCount;
		}

		/// <summary></summary>
		public DotNetQualifiedMethodName(string localName, DotNetQualifiedName fullNamespace, int genericTypeCount = 0) : base(localName, fullNamespace)
		{
			this.genericTypeCount = genericTypeCount;
		}
	}
}
