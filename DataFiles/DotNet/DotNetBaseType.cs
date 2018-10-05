using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a node in an inheriance hierarchy.
	/// Stub class: contains minimal information about the type.
	/// </summary>
	public class DotNetBaseType
	{
		/// <summary></summary>
		public DotNetQualifiedName Name { get; protected set; }

		/// <summary></summary>
		public DotNetBaseType BaseType { get; protected set; }

		/// <summary>Returns the inheritance distance from here to the bottom.</summary>
		/// <example>Class "System.Reflection.TypeInfo" has Depth = 4 because its inheritance path is "TypeInfo" -> "Type" -> "MemberInfo" -> "Object".</example>
		/// <example>Class "System.Object" has Depth = 1 because its inheritance path is just "Object".</example>
		public int Depth {
			get {
				return 1 + ((BaseType == null) ? 0 : BaseType.Depth);
			}
		}

		/// <summary></summary>
		public DotNetBaseType(Type type)
		{
			Name = DotNetQualifiedName.FromAssemblyInfo(type);
			if(type.BaseType != null)
			{
				BaseType = new DotNetBaseType(type.BaseType);
			}
		}
	}
}
