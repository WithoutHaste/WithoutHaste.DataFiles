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
	public class DotNetCommentRowTests
	{
		[TestMethod]
		public void DotNetCommentRow_FromXml_EmptyHeader()
		{
			//arrange
			XElement element = XElement.Parse("<listheader />", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentRow result = DotNetCommentRow.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(true, result.IsHeader);
			Assert.AreEqual(0, result.ColumnCount);
			Assert.AreEqual(null, result[0].Text);
		}

		[TestMethod]
		public void DotNetCommentRow_FromXml_EmptyData()
		{
			//arrange
			XElement element = XElement.Parse("<item />", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentRow result = DotNetCommentRow.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(false, result.IsHeader);
			Assert.AreEqual(0, result.ColumnCount);
			Assert.AreEqual(null, result[0].Text);
		}

		[TestMethod]
		public void DotNetCommentRow_FromXml_Header()
		{
			//arrange
			XElement table = XElement.Load(Utilities.GetPathTo("data/DotNetCommentTable_Full.xml"));
			XElement element = table.Elements("listheader").First();
			//act
			DotNetCommentRow result = DotNetCommentRow.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(true, result.IsHeader);
			Assert.AreEqual(3, result.ColumnCount);
			Assert.AreEqual("Header 1", result[0].Text);
			Assert.AreEqual("Header 2", result[1].Text);
			Assert.AreEqual("Header 3", result[2].Text);
			Assert.AreEqual(null, result[3].Text);
		}

		[TestMethod]
		public void DotNetCommentRow_FromXml_Data()
		{
			//arrange
			XElement table = XElement.Load(Utilities.GetPathTo("data/DotNetCommentTable_Full.xml"));
			XElement element = table.Elements("item").First();
			//act
			DotNetCommentRow result = DotNetCommentRow.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(false, result.IsHeader);
			Assert.AreEqual(2, result.ColumnCount);
			Assert.AreEqual("R1 C1", result[0].Text);
			Assert.AreEqual("R1 C2", result[1].Text);
			Assert.AreEqual(null, result[2].Text);
		}
	}
}
