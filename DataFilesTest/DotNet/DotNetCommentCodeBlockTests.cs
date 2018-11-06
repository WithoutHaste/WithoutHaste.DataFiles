using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentCodeBlockTests
	{
		[TestMethod]
		public void DotNetCommentCodeBlock_FromXml_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<code/>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentCode result = DotNetCommentCodeBlock.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentCodeBlock);
			Assert.AreEqual(null, result.Text);
		}

		[TestMethod]
		public void DotNetCommentCodeBlock_FromXml_Full()
		{
			//arrange
			string code = "int x = 5;\nint y = 6;";
			XElement element = XElement.Parse("<code>" + code + "</code>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentCode result = DotNetCommentCodeBlock.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentCodeBlock);
			Assert.AreEqual(code, result.Text);
		}
	}
}
