using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WithoutHaste.DataFiles
{
	/// <summary>
	/// Generic System.Xml.XmlDocument utilities.
	/// </summary>
    public static class XmlDocumentHelper
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

		/// <summary>
		/// Returns a string containing the entire contents of the XmlDocument.
		/// </summary>
		public static string XmlToString(XmlDocument xmlDocument)
		{
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlDocument.WriteTo(xmlTextWriter);
			return stringWriter.ToString();
		}

		/// <summary>
		/// Returns a string containing the entire contents of the XmlNode.
		/// </summary>
		public static string XmlToString(XmlNode xmlNode)
		{
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlNode.WriteTo(xmlTextWriter);
			return stringWriter.ToString();
		}
	}
}
