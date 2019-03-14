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

		public static string XNodeToString(XNode node)
		{
			return WithoutHaste.DataFiles.DotNet.Utilities.XNodeToString(node);
		}
	}
}
