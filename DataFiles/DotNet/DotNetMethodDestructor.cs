using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a method that is a destructor.
	/// </summary>
	public class DotNetMethodDestructor : DotNetMethod
	{
		/// <summary></summary>
		public DotNetMethodDestructor(DotNetQualifiedMethodName name) : base(name)
		{
			Category = MethodCategory.Normal;
		}

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(ConstructorInfo constructorInfo)
		{
			//no action
		}
	}
}