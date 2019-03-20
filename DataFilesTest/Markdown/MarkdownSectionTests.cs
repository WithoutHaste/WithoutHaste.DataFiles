using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.Markdown;

namespace DataFilesTest
{
	[TestClass]
	public class MarkdownSectionTests
	{
		[TestMethod]
		public void MarkdownSection_ToMarkdown_SectionAfterNull()
		{
			//arrange
			MarkdownSection section = new MarkdownSection("Header");
			string previousText = null;
			//act
			string text = section.ToMarkdownString(previousText);
			//assert
			Assert.AreEqual("# Header\n\n", text);
		}

		[TestMethod]
		public void MarkdownSection_ToMarkdown_SectionAfterJustOneChar()
		{
			//arrange
			MarkdownSection section = new MarkdownSection("Header");
			string previousText = "A";
			//act
			string text = section.ToMarkdownString(previousText);
			//assert
			Assert.AreEqual("\n\n# Header\n\n", text);
		}

		[TestMethod]
		public void MarkdownSection_ToMarkdown_SectionAfterJustOneEndline()
		{
			//arrange
			MarkdownSection section = new MarkdownSection("Header");
			string previousText = "\n";
			//act
			string text = section.ToMarkdownString(previousText);
			//assert
			Assert.AreEqual("# Header\n\n", text);
		}

		[TestMethod]
		public void MarkdownSection_ToMarkdown_SectionAfterTwoEndlines()
		{
			//arrange
			MarkdownSection section = new MarkdownSection("Header");
			string previousText = "The quick brown fox\n\n";
			//act
			string text = section.ToMarkdownString(previousText);
			//assert
			Assert.AreEqual("# Header\n\n", text);
		}

		[TestMethod]
		public void MarkdownSection_ToMarkdown_SectionAfterOneEndline()
		{
			//arrange
			MarkdownSection section = new MarkdownSection("Header");
			string previousText = "The quick brown fox\n";
			//act
			string text = section.ToMarkdownString(previousText);
			//assert
			Assert.AreEqual("\n# Header\n\n", text);
		}

		[TestMethod]
		public void MarkdownSection_ToMarkdown_SectionAfterZeroEndlines()
		{
			//arrange
			MarkdownSection section = new MarkdownSection("Header");
			string previousText = "The quick brown fox";
			//act
			string text = section.ToMarkdownString(previousText);
			//assert
			Assert.AreEqual("\n\n# Header\n\n", text);
		}

		[TestMethod]
		public void MarkdownSection_ToMarkdown_TextThenSection()
		{
			//arrange
			MarkdownSection sectionA = new MarkdownSection("A");
			MarkdownText text = new MarkdownText("Text");
			sectionA.Add(text);
			MarkdownSection sectionB = sectionA.AddSection("B");
			//act
			string result = sectionA.ToMarkdownString();
			//assert
			Assert.AreEqual("# A\n\nText\n\n## B\n\n", result);
		}
	}
}
