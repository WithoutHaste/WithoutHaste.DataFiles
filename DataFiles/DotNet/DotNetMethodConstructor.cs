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
		public DotNetMethodConstructor(DotNetQualifiedMethodName name) : base(name)
		{
			Category = MethodCategory.Normal;
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
	}
}
