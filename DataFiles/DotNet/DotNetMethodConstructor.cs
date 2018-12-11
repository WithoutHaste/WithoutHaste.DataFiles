using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a method that is a constructor.
	/// </summary>
	public class DotNetMethodConstructor : DotNetMethod
	{
		/// <summary></summary>
		public DotNetMethodConstructor(DotNetQualifiedMethodName name, bool isStatic = false) : base(name)
		{
			Category = (isStatic) ? MethodCategory.Static : MethodCategory.Normal;
		}

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(ConstructorInfo constructorInfo)
		{
			int index = 0;
			foreach(ParameterInfo parameterInfo in constructorInfo.GetParameters())
			{
				MethodName.Parameters[index].AddAssemblyInfo(parameterInfo);
				index++;
			}
		}

		/// <summary>
		/// Constructors need to reference the actual name of their type so they display the right name with aliases.
		/// </summary>
		internal void SetClassName(DotNetQualifiedClassName className)
		{
			MethodName.SetClassName(className);
		}
	}
}
