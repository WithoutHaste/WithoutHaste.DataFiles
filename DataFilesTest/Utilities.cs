using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

	}
}
