using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles;

namespace DataFilesTest
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
			XmlDocumentHelper.Validate(xmlNode, "");
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
			XmlDocumentHelper.Validate(xmlNode, "B");
			//assert exception
		}

		[TestMethod]
		public void XmlNode_Validate_RightNode()
		{
			//arrange
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("A");
			//act
			XmlDocumentHelper.Validate(xmlNode, "A");
			//assert no exception
		}
	}
}
