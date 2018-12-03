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
