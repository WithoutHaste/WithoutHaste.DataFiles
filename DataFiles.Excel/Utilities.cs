using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WithoutHaste.DataFiles.Excel
{
	internal static class Utilities
	{
		/// <summary>
		/// Throw exception if XmlNode does not exist, or does not have the expected LocalName.
		/// </summary>
		/// <exception cref="XmlNodeException"></exception>
		public static void Validate(XmlNode node, string localName)
		{
			if(node == null || node.LocalName != localName)
			{
				throw new XmlNodeException(localName + " node expected.");
			}
		}
	}
}
