using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a normal-type parameter in a method signature.
	/// </summary>
	public class DotNetParameter : DotNetBaseParameter
	{
		/// <summary>Fully qualified data type name object.</summary>
		public DotNetQualifiedName TypeName { get; protected set; }

		/// <summary>Fully qualified data type name.</summary>
		public override string FullName { get { return TypeName?.FullName; } }

		/// <summary>Local data type name.</summary>
		public override string LocalName { get { return TypeName?.LocalName; } }

		#region Constructors

		/// <summary>Empty constructor.</summary>
		public DotNetParameter()
		{
			TypeName = null;
		}

		/// <summary></summary>
		/// <param name="typeName">Fully qualified data type name.</param>
		public DotNetParameter(DotNetQualifiedName typeName)
		{
			TypeName = typeName;
		}

		#endregion
	}
}
