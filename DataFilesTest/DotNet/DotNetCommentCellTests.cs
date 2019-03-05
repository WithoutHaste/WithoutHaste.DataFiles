using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentCellTests
	{
		//todo: commented out over version issue with System.Xml.Linq NuGet library
		//[TestMethod]
		//public void DotNetCommentCell_FromXml_Empty()
		//{
		//	//arrange
		//	XElement element = XElement.Parse("<term/>", LoadOptions.PreserveWhitespace);
		//	//act
		//	DotNetCommentCell result = DotNetCommentCell.FromVisualStudioXml(element);
		//	//assert
		//	Assert.AreEqual(null, result.Text);
		//}

		//[TestMethod]
		//public void DotNetCommentCell_FromXml_Full()
		//{
		//	//arrange
		//	string text = "Lorem ipsum dolor sit amet";
		//	XElement element = XElement.Parse("<term>" + text + "</term>", LoadOptions.PreserveWhitespace);
		//	//act
		//	DotNetCommentCell result = DotNetCommentCell.FromVisualStudioXml(element);
		//	//assert
		//	Assert.AreEqual(text, result.Text);
		//}
	}
}
