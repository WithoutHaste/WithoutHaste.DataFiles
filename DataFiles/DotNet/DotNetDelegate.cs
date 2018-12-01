using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a delegate type, categorized as a method.
	/// </summary>
	public class DotNetDelegate : DotNetMethod
	{
		/// <summary></summary>
		public DotNetDelegate(DotNetQualifiedMethodName name) : base(name)
		{
			Category = MethodCategory.Delegate;
		}

		/// <summary></summary>
		public DotNetDelegate(DotNetQualifiedName name) : base(new DotNetQualifiedMethodName(name))
		{
			Category = MethodCategory.Delegate;
		}

		/// <summary>Add additional documentation information from the assembly itself.</summary>
		public void AddAssemblyInfo(TypeInfo typeInfo)
		{
			AddAssemblyInfo(typeInfo.DeclaredMethods.First(m => m.Name == "Invoke"));
		}
	}
}
