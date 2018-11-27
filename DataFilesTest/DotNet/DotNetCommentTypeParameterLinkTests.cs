using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentTypeParameterLinkTests
	{
		[TestMethod]
		public void DotNetCommentTypeParameterLink_FromXml()
		{
			//arrange
			string name = "test";
			XElement element = XElement.Parse("<typeparamref name='" + name + "' />", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentTypeParameterLink result = DotNetCommentTypeParameterLink.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(name, result.Name);
		}

		[TestMethod]
		public void DotNetCommentTypeParameterLink_FromXml_AfterText()
		{
			//arrange
			string xml = "<summary>The type-parameter names are: <typeparamref name='A'/>, <typeparamref name='B'/>, and <typeparamref name='C'/>.</summary>";
			XElement element = XElement.Parse(xml, LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentGroup result = DotNetCommentGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(7, result.Comments.Count);
			Assert.AreEqual("The type-parameter names are: ", (result[0] as DotNetCommentText).Text);
			Assert.AreEqual("A", (result[1] as DotNetCommentTypeParameterLink).Name);
			Assert.AreEqual(", ", (result[2] as DotNetCommentText).Text);
			Assert.AreEqual("B", (result[3] as DotNetCommentTypeParameterLink).Name);
			Assert.AreEqual(", and ", (result[4] as DotNetCommentText).Text);
			Assert.AreEqual("C", (result[5] as DotNetCommentTypeParameterLink).Name);
			Assert.AreEqual(".", (result[6] as DotNetCommentText).Text);
		}
	}
}
