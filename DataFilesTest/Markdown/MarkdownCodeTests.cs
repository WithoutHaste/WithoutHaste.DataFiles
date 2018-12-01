using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.Markdown;

namespace DataFilesTest
{
	[TestClass]
	public class MarkdownCodeTests
	{
		[TestMethod]
		public void MarkdownCode_EscapeBacktics_OneMiddle()
		{
			//arrange
			MarkdownCode code = new MarkdownCode("a`a");
			//act
			string text = code.ToMarkdown(null);
			//assert
			Assert.AreEqual("``a`a``", text);
		}

		[TestMethod]
		public void MarkdownCode_EscapeBacktics_TwoMiddle()
		{
			//arrange
			MarkdownCode code = new MarkdownCode("a``a");
			//act
			string text = code.ToMarkdown(null);
			//assert
			Assert.AreEqual("```a``a```", text);
		}

		[TestMethod]
		public void MarkdownCode_EscapeBacktics_ThreeMiddle()
		{
			//arrange
			MarkdownCode code = new MarkdownCode("a```a");
			//act
			string text = code.ToMarkdown(null);
			//assert
			Assert.AreEqual("````a```a````", text);
		}

		[TestMethod]
		public void MarkdownCode_EscapeBacktics_VariousMiddle()
		{
			//arrange
			MarkdownCode code = new MarkdownCode("a``a```a`a");
			//act
			string text = code.ToMarkdown(null);
			//assert
			Assert.AreEqual("````a``a```a`a````", text);
		}

		[TestMethod]
		public void MarkdownCode_EscapeBacktics_OneStart()
		{
			//arrange
			MarkdownCode code = new MarkdownCode("`aa");
			//act
			string text = code.ToMarkdown(null);
			//assert
			Assert.AreEqual("`` `aa``", text);
		}

		[TestMethod]
		public void MarkdownCode_EscapeBacktics_TwoStart()
		{
			//arrange
			MarkdownCode code = new MarkdownCode("``aa");
			//act
			string text = code.ToMarkdown(null);
			//assert
			Assert.AreEqual("``` ``aa```", text);
		}

		[TestMethod]
		public void MarkdownCode_EscapeBacktics_OneEnd()
		{
			//arrange
			MarkdownCode code = new MarkdownCode("aa`");
			//act
			string text = code.ToMarkdown(null);
			//assert
			Assert.AreEqual("``aa` ``", text);
		}

		[TestMethod]
		public void MarkdownCode_EscapeBacktics_TwoEnd()
		{
			//arrange
			MarkdownCode code = new MarkdownCode("aa``");
			//act
			string text = code.ToMarkdown(null);
			//assert
			Assert.AreEqual("```aa`` ```", text);
		}
	}
}
