using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.Markdown;

namespace DataFilesTest
{
	[TestClass]
	public class MarkdownParagraphTests
	{
		[TestMethod]
		public void MarkdownParagraph_Nested()
		{
			//arrange
			MarkdownParagraph p1 = new MarkdownParagraph("The Quick Brown Fox");
			MarkdownParagraph p2 = new MarkdownParagraph(new MarkdownLine("Jumped Over"));
			MarkdownParagraph p3 = new MarkdownParagraph("The Lazy Dog    \n\t  \n\n");
			MarkdownParagraph paragraph = new MarkdownParagraph(p1, p2, p3);
			//act
			string result = paragraph.ToMarkdownString(null);
			//assert
			Assert.AreEqual("The Quick Brown Fox  \n\nJumped Over  \n\nThe Lazy Dog  \n\n", result);
		}
	}
}
