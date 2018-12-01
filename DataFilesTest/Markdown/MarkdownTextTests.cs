using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.Markdown;

namespace DataFilesTest
{
	[TestClass]
	public class MarkdownTextTests
	{
		[TestMethod]
		public void MarkdownText_EscapeBacktics()
		{
			//arrange
			MarkdownText text = new MarkdownText("a`a");
			//act
			string result = text.ToMarkdown(null);
			//assert
			Assert.AreEqual("a&#96;a", result);
		}

		[TestMethod]
		public void MarkdownText_EscapeOpenAngleBracket()
		{
			//arrange
			MarkdownText text = new MarkdownText("a<a");
			//act
			string result = text.ToMarkdown(null);
			//assert
			Assert.AreEqual("a&lt;a", result);
		}

		[TestMethod]
		public void MarkdownText_EscapeCloseAngleBracket()
		{
			//arrange
			MarkdownText text = new MarkdownText("a>a");
			//act
			string result = text.ToMarkdown(null);
			//assert
			Assert.AreEqual("a&gt;a", result);
		}

		[TestMethod]
	}
}
