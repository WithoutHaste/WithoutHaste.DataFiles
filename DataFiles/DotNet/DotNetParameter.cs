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
		/// <summary>Fully qualified data type name object.</summary>
		public DotNetQualifiedName TypeName { get; protected set; }

		/// <summary>Fully qualified data type name.</summary>
		public string FullName { get { return TypeName.FullName; } }

		/// <summary>Local data type name.</summary>
		public string LocalName { get { return TypeName.LocalName; } }

		#region Constructors

		/// <summary></summary>
		/// <param name="typeName">Fully qualified data type name.</param>
		public DotNetParameter(DotNetQualifiedName typeName)
		{
			TypeName = typeName;
		}

		/// <summary>
		/// Parses a .Net XML documentation parameter type.
		/// </summary>
		public static DotNetParameter FromVisualStudioXml(string typeName)
		{
			return new DotNetParameter(DotNetQualifiedName.ParameterTypeFromVisualStudioXml(typeName));
		}

		#endregion
	}
}
