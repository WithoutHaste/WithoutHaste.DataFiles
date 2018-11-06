using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentTypeParameterTests
	{
		[TestMethod]
		public void DotNetCommentTypeParameter_FromXml_Empty()
		{
			//arrange
			string name = "test";
			XElement element = XElement.Parse("<typeparam name='" + name + "' />", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentTypeParameter result = DotNetCommentTypeParameter.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(name, result.Link.FullName);
			Assert.AreEqual(0, result.Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentTypeParameter_FromXml_Full()
		{
			//arrange
			string name = "test";
			string comments = Utilities.LoadText("data/DotNetCommentGroup_XmlCommentsNestedInTag.txt");
			XElement element = XElement.Parse("<typeparam name='" + name + "'>" + comments + "</typeparam>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentTypeParameter result = DotNetCommentTypeParameter.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(name, result.Link.FullName);
			Assert.AreEqual(14, result.Comments.Count);
		}
	}
}
