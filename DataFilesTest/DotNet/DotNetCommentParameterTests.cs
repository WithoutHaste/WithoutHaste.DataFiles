using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentParameterTests
	{
		[TestMethod]
		public void DotNetCommentParameter_FromXml_Empty()
		{
			//arrange
			string name = "test";
			XElement element = XElement.Parse("<param name='" + name + "' />");
			//act
			DotNetCommentParameter result = DotNetCommentParameter.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(name, result.Link.FullName);
			Assert.AreEqual(0, result.Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentParameter_FromXml_Full()
		{
			//arrange
			string name = "test";
			XElement element = XElement.Parse("<typeparam name='" + name + "'>" + DotNetCommentGroupTests.GetXmlCommentsNestedInTag() + "</typeparam>");
			//act
			DotNetCommentTypeParameter result = DotNetCommentTypeParameter.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(name, result.Link.FullName);
			DotNetCommentGroupTests.ValidateXmlCommentsNestedInTag(result.Comments);
		}
	}
}
