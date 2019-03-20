using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a generic-type parameter that is not in a class declaration or a method declaration.
	/// </summary>
	/// <example>The "U"s in MyMethod(<![CDATA[List<U>]]> list, U obj).</example>
	public abstract class DotNetReferenceGeneric : DotNetQualifiedTypeName
	{
		/// <summary></summary>
		public override string LocalName { get { return Alias; } }

		/// <summary>
		/// The generic-type alias specified in the assembly. Null if not known.
		/// Whether this refers to a class-generic or method-generic is determined by the subclass.
		/// </summary>
		public string Alias { get; protected set; }

		/// <summary>0-based index in class's generic type list corresponding to this parameter.</summary>
		protected int genericTypeIndex = 0;

		/// <summary>
		/// Set the generic-type alias of this type, based on this ordered list of aliases.
		/// </summary>
		/// <returns>Returns False if the index is out of bounds and the alias is not updated.</returns>
		internal bool SetAlias(string[] genericTypeAliases)
		{
			if(genericTypeIndex < 0 || genericTypeIndex >= genericTypeAliases.Length)
				return false;
			Alias = genericTypeAliases[genericTypeIndex];
			return true;
		}

		/// <summary>
		/// Generic type references cannot be localized.
		/// </summary>
		public override void Localize(DotNetQualifiedName other)
		{
			return;
		}
	}
}
