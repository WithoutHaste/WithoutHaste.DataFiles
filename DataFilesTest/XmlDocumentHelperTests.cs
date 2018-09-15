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
	}
}
