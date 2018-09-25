using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentTableTests
	{
		[TestMethod]
		public void DotNetCommentTable_FromXml_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<list type='table' />");
			//act
			DotNetCommentTable result = DotNetCommentTable.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(0, result.RowCount);
			Assert.AreEqual(0, result.HeaderRowCount);
			Assert.AreEqual(0, result.DataRowCount);
			Assert.AreEqual(0, result.ColumnCount);
		}

		[TestMethod]
		public void DotNetCommentTable_FromXml_Full()
		{
			//arrange
			XElement element = XElement.Load("data/DotNetCommentTable_Full.xml");
			//act
			DotNetCommentTable result = DotNetCommentTable.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(4, result.RowCount);
			Assert.AreEqual(1, result.HeaderRowCount);
			Assert.AreEqual(3, result.DataRowCount);
			Assert.AreEqual(3, result.ColumnCount);
			Assert.AreEqual("Header 1", result[0, 0].Text);
			Assert.AreEqual("Header 2", result[0, 1].Text);
			Assert.AreEqual("Header 3", result[0, 2].Text);
			Assert.AreEqual("R1 C1", result[1, 0].Text);
			Assert.AreEqual("R1 C2", result[1, 1].Text);
			Assert.AreEqual(null, result[1, 2].Text);
			Assert.AreEqual("R2 C1", result[2, 0].Text);
			Assert.AreEqual("R2 C2", result[2, 1].Text);
			Assert.AreEqual("R2 C3", result[2, 2].Text);
			Assert.AreEqual("R3 C1", result[3, 0].Text);
			Assert.AreEqual(null, result[3, 1].Text);
			Assert.AreEqual(null, result[3, 2].Text);
		}
	}
}
