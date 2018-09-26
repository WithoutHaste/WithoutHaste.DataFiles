using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a generic-type parameter in a method signature, or a member type, or a return type.
	/// </summary>
	public abstract class DotNetParameterGeneric : DotNetParameterBase
	{
		/// <summary>
		/// The generic-type alias specified in the assembly. Null if not known.
		/// Whether this refers to a class-generic or method-generic is determined by the subclass.
		/// </summary>
		public string Alias { get; protected set; }

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetParameterGeneric()
		{
		}

		/// <summary></summary>
		public DotNetParameterGeneric(string alias)
		{
			Alias = alias;
		}

		#endregion
	}
}
