using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.Excel;

namespace DataFilesTest
{
	[TestClass]
	class UtilityTests
	{
		[TestMethod]
		[ExpectedException(typeof(XmlNodeException))]
		public void XmlNode_Validate_NullNode()
		{
			//arrange
			XmlNode xmlNode = null;
			//act
			WithoutHaste.DataFiles.Excel.Utilities.Validate(xmlNode, "");
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
			WithoutHaste.DataFiles.Excel.Utilities.Validate(xmlNode, "B");
			//assert exception
		}

		[TestMethod]
		public void XmlNode_Validate_RightNode()
		{
			//arrange
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.CreateElement("A");
			//act
			WithoutHaste.DataFiles.Excel.Utilities.Validate(xmlNode, "A");
			//assert no exception
		}
	}
}
