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
	/// <summary>
	/// Retrieves version information.
	/// </summary>
	public static class Info
	{
		/// <summary>
		/// Returns the version of this library.
		/// </summary>
		public static Version GetVersion()
		{
			return AssemblyInfo.GetVersion(Assembly.GetExecutingAssembly());
		}

		/// <summary>
		/// Returns the versions of any special libraries this one relies on.
		/// </summary>
		public static Dictionary<string, Version> GetDependencyVersions()
		{
			string[] dlls = new string[] {
				"EPPlus.dll"
			};
			return AssemblyInfo.GetDllVersions(dlls);
		}
	}
}
