using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a generic-type parameter that is not in a class declaration or a method declaration.
	/// </summary>
	/// <example><![CDATA[The "U"s in MyMethod(List<U> list, U obj).]]></example>
	public abstract class DotNetReferenceGeneric : DotNetQualifiedTypeName
	{
		/// <summary>
		/// The generic-type alias specified in the assembly. Null if not known.
		/// Whether this refers to a class-generic or method-generic is determined by the subclass.
		/// </summary>
		public string Alias { get; protected set; }

		/// <summary>0-based index in class's generic type list corresponding to this parameter.</summary>
		protected int genericTypeIndex = 0;

		#region Constructors

		/// <param name="genericTypeIndex">
		///   0-based index of type in class or method declaration type parameter list.
		///   <example><![CDATA[Index 0 refers to "T" in "class MyGeneric<T,U> { }"]]></example>
		///   <example><![CDATA[Index 0 refers to "A" in "void MyMethod<A,B>() { }"]]></example>
		/// </param>
		/// <param name="alias">Alias of generic-type within assembly. Null if not known.</param>
		/// <exception cref="ArgumentException">GenericTypeIndex cannot be less than 0.</exception>
		public DotNetReferenceGeneric(int genericTypeIndex, string alias = null)
		{
			if(genericTypeIndex < 0)
				throw new ArgumentException("GenericTypeIndex cannot be less than 0.", "genericTypeIndex");

			FullNamespace = null;
			Alias = alias;
			this.localName = null;
			this.genericTypeIndex = genericTypeIndex;
		}

		#endregion

		/// <summary></summary>
		public void SetAlias(string alias)
		{
			this.Alias = alias;
		}
	}
}
