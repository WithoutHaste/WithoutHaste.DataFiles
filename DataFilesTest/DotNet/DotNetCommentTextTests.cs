using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentTextTests
	{
		[TestMethod]
		public void DotNetCommentText_FromXml_Empty()
		{
			//arrange
			string text = "";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(text);
			//assert
			Assert.AreEqual(null, result.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_Short()
		{
			//arrange
			string text = "Lorem ipsum dolor sit amet";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(text);
			//assert
			Assert.AreEqual(text, result.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_Formatted()
		{
			//arrange
			string originalText = @"
	  Lorem ipsum dolor sit amet. 
	  ultricies hendrerit vehicula. 

	  In tempus id diam eu mollis. Donec 
	  vulputate, nunc massa bibendum purus.";
			string expectedResult = "Lorem ipsum dolor sit amet. \nultricies hendrerit vehicula. \n\nIn tempus id diam eu mollis. Donec \nvulputate, nunc massa bibendum purus.";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(originalText);
			//assert
			Assert.AreEqual(expectedResult, result.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_InLine()
		{
			//arrange
			string originalText = " or ";
			string expectedResult = " or ";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(originalText);
			//assert
			Assert.AreEqual(expectedResult, result.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_InLine_RealExample_A()
		{
			//arrange
			XElement element = XElement.Parse(@"<summary><see cref='P:Test.ClassSeeAlso.PropertyA'/> or <see cref='P:Test.ClassSeeAlso.PropertyA'>Local property</see></summary>", LoadOptions.PreserveWhitespace);
			//act
			DotNetComment result = DotNetComment.FromVisualStudioXml(element);
			DotNetCommentGroup groupResult = result as DotNetCommentGroup;
			//assert
			Assert.AreEqual(3, groupResult.Comments.Count);
			Assert.AreEqual(" or ", (groupResult.Comments[1] as DotNetCommentText).Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_InLine_RealExample_B()
		{
			//arrange
			XDocument document = XDocument.Load(Utilities.GetPathTo("data/DotNetCommentText_Inline_RealExample_B.xml"), LoadOptions.PreserveWhitespace);
			XElement element = document.Elements().First();
			string inner = String.Concat(element.Nodes().Select(node => Utilities.XNodeToString(node)));
			//act
			DotNetComment result = DotNetComment.FromVisualStudioXml(element);
			DotNetCommentGroup groupResult = result as DotNetCommentGroup;
			//assert
			Assert.AreEqual(7, groupResult.Comments.Count);
			Assert.AreEqual(" or ", (groupResult.Comments[1] as DotNetCommentText).Text);
			Assert.AreEqual("\n", (groupResult.Comments[3] as DotNetCommentText).Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_RealExample_A()
		{
			//arrange
			string originalText = @"
            Tests the display of common data types that have recognized aliases in .Net.
            Also common data types that have long fully-qualified names.
            ";
			string expectedResult = "Tests the display of common data types that have recognized aliases in .Net.\nAlso common data types that have long fully-qualified names.";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(originalText);
			//assert
			Assert.AreEqual(expectedResult, result.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_RealExample_A2()
		{
			//arrange
			XElement element = XElement.Parse(@"<summary>
            Tests the display of common data types that have recognized aliases in .Net.
            Also common data types that have long fully-qualified names.
            </summary>", LoadOptions.PreserveWhitespace);
			string originalText = Utilities.XNodeToString(element.Nodes().First());
			string expectedResult = "Tests the display of common data types that have recognized aliases in .Net.\nAlso common data types that have long fully-qualified names.";
			//act
			DotNetCommentText result = DotNetCommentText.FromVisualStudioXml(originalText);
			//assert
			Assert.AreEqual(expectedResult, result.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_RealExample_A3()
		{
			//arrange
			XElement element = XElement.Parse(@"<summary>
            Tests the display of common data types that have recognized aliases in .Net.
            Also common data types that have long fully-qualified names.
            </summary>", LoadOptions.PreserveWhitespace);
			string expectedResult = "Tests the display of common data types that have recognized aliases in .Net.\nAlso common data types that have long fully-qualified names.";
			//act
			DotNetComment result = DotNetComment.FromVisualStudioXml(element);
			DotNetCommentGroup groupResult = result as DotNetCommentGroup;
			result = groupResult.Comments.First();
			DotNetCommentText textResult = result as DotNetCommentText;
			//assert
			Assert.AreEqual(expectedResult, textResult.Text);
		}

		[TestMethod]
		public void DotNetCommentText_FromXml_SpaceAfterParamRef()
		{
			//arrange
			XElement element = XElement.Parse(@"<summary>
Split <paramref name='text'/> on the <paramref name='delimiter'/> 
but do not split if <paramref name='delimiter'/> is nested within matched braces.
Supports braces: {}, [], (), and <![CDATA[<>]]>.
</summary>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentGroup result = DotNetCommentGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual("Split ", (result[0] as DotNetCommentText).Text);
			Assert.AreEqual(" on the ", (result[2] as DotNetCommentText).Text);
			Assert.AreEqual(" \nbut do not split if ", (result[4] as DotNetCommentText).Text);
			Assert.AreEqual(" is nested within matched braces.\nSupports braces: {}, [], (), and ", (result[6] as DotNetCommentText).Text);
		}
	}
}
