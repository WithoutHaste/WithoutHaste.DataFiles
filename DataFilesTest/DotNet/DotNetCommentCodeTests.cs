using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentCodeTests
	{
		[TestMethod]
		public void DotNetCommentCode_FromXml_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<c/>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentCode result = DotNetCommentCode.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(null, result.Text);
		}

		[TestMethod]
		public void DotNetCommentCode_FromXml_Full()
		{
			//arrange
			string code = "int x = 5;\nint y = 6;";
			XElement element = XElement.Parse("<c>" + code + "</c>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentCode result = DotNetCommentCode.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(code, result.Text);
		}
	}
}
