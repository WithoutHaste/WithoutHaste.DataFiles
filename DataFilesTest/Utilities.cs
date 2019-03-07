using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	public static class Utilities
	{
		public static string LoadText(string fullPath)
		{
			using(StreamReader reader = new StreamReader(fullPath))
			{
				return reader.ReadToEnd().Replace("\r", "");
			}
		}

		//TODO remove
		private static string _dataFilesTargetFramework = null;
		public static string DataFilesTargetFramework {
			get {
				if(_dataFilesTargetFramework == null)
				{
					Assembly assembly = typeof(DotNetSettings).Assembly;
					_dataFilesTargetFramework = assembly.ImageRuntimeVersion;
				}
				return _dataFilesTargetFramework;
			}
		}
	}
}
