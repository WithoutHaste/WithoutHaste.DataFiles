using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.Markdown;

namespace DataFilesTest
{
	[TestClass]
	public class MarkdownListTests
	{
		[TestMethod]
		public void MarkdownList_Add_InLine()
		{
			//arrange
			MarkdownList list = new MarkdownList();
			MarkdownText text = new MarkdownText("abc");
			//act
			list.Add(text as IMarkdownInLine);
			//assert
			Assert.AreEqual(1, list.Length);
			Assert.IsNotNull(list[0]);
		}
	}
}
