using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a normal-type parameter in a method signature.
	/// </summary>
	public class DotNetParameter
	{
		/// <summary>Fully qualified data type name object.</summary>
		public DotNetQualifiedTypeName TypeName { get; protected set; }

		/// <summary>Fully qualified data type name.</summary>
		public string FullTypeName { get { return TypeName?.FullName; } }

		/// <summary>Local data type name.</summary>
		public string LocalTypeName { get { return TypeName?.LocalName; } }

		/// <summary>Name of parametere. Null if not known.</summary>
		public string Name { get; protected set; }

		#region Constructors

		/// <summary>Empty constructor.</summary>
		public DotNetParameter()
		{
			TypeName = null;
		}

		/// <summary></summary>
		/// <param name="typeName">Fully qualified data type name.</param>
		public DotNetParameter(DotNetQualifiedTypeName typeName)
		{
			TypeName = typeName;
		}

		/// <summary>
		/// Parses a .Net XML documentation parameter type name.
		/// </summary>
		public static DotNetParameter FromVisualStudioXml(string typeName)
		{
			return new DotNetParameter(DotNetQualifiedTypeName.FromVisualStudioXml(typeName));
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(ParameterInfo parameterInfo)
		{
			Name = parameterInfo.Name;
		}
	}
}
