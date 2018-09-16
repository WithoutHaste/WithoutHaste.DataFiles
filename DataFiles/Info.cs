using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WithoutHaste.Libraries;

namespace WithoutHaste.DataFiles
{
	public static class Info
	{
		public static Version GetVersion()
		{
			return WithoutHaste.Libraries.AssemblyInfo.GetVersion();
		}

		public static Dictionary<string, Version> GetDependencyVersions()
		{
			string[] dlls = new string[] {
				"EPPlus.dll"
			};
			return WithoutHaste.Libraries.AssemblyInfo.GetAssemblyVersions(dlls);
		}
	}
}
