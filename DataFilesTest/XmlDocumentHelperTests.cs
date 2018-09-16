using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles;

namespace DataFormatsTest
{
	[TestClass]
	public class XmlDocumentHelperTests
	{
		[TestMethod]
		[ExpectedException(typeof(XmlNodeException))]
		public void XmlNode_Validate_NullNode()
		{
			//arrange
			XmlNode xmlNode = null;
			//act
			xmlNode.Validate("");
			//assert exception
		}

		[TestMethod]
		[ExpectedException(typeof(XmlNodeException))]
		public void XmlNode_Validate_WrongNode()
		{
			//arrange
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("A");
			//act
			xmlNode.Validate("B");
			//assert exception
		}

		[TestMethod]
		public void XmlNode_Validate_RightNode()
		{
			//arrange
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("A");
			//act
			xmlNode.Validate("A");
			//assert no exception
		}
	}
}
