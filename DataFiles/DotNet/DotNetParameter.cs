using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a parameter in a method signature.
	/// </summary>
	public class DotNetParameter
	{
		/// <summary>Fully qualified data type name.</summary>
		public DotNetQualifiedName TypeName { get; protected set; }

		#region Constructors

		/// <summary></summary>
		/// <param name="typeName">Fully qualified data type name.</param>
		public DotNetParameter(DotNetQualifiedName typeName)
		{
			TypeName = typeName;
		}

		#endregion
	}
}
