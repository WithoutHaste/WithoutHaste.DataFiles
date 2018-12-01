using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentListItemTests
	{
		[TestMethod]
		public void DotNetCommentListItem_FromXml_Header_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<listheader/>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(true, result.IsHeader);
			Assert.AreEqual(null, result.Term);
			Assert.AreEqual(null, result.Description);
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Header_PlainText()
		{
			//arrange
			string text = "Lorem ipsum dolor sit amet";
			XElement element = XElement.Parse("<listheader>" + text + "</listheader>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(true, result.IsHeader);
			Assert.AreEqual(text, result.Term[0].ToString());
			Assert.AreEqual(null, result.Description);
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Header_Term()
		{
			//arrange
			string text = "Lorem ipsum dolor sit amet";
			XElement element = XElement.Parse("<listheader><term>" + text + "</term></listheader>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(true, result.IsHeader);
			Assert.AreEqual(text, result.Term[0].ToString());
			Assert.AreEqual(null, result.Description);
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Header_TermDescription()
		{
			//arrange
			string text = "Lorem ipsum dolor sit amet";
			string description = "Vivamus odio justo, bibendum non rutrum ac.";
			XElement element = XElement.Parse("<listheader><term>" + text + "</term><description>" + description + "</description></listheader>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(true, result.IsHeader);
			Assert.AreEqual(text, result.Term[0].ToString());
			Assert.AreEqual(description, result.Description[0].ToString());
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Header_Description()
		{
			//arrange
			string description = "Vivamus odio justo, bibendum non rutrum ac.";
			XElement element = XElement.Parse("<listheader><description>" + description + "</description></listheader>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(true, result.IsHeader);
			Assert.AreEqual(null, result.Term);
			Assert.AreEqual(description, result.Description[0].ToString());
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Data_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<item/>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(false, result.IsHeader);
			Assert.AreEqual(null, result.Term);
			Assert.AreEqual(null, result.Description);
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Data_PlainText()
		{
			//arrange
			string text = "Lorem ipsum dolor sit amet";
			XElement element = XElement.Parse("<item>" + text + "</item>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(false, result.IsHeader);
			Assert.AreEqual(text, result.Term[0].ToString());
			Assert.AreEqual(null, result.Description);
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Data_Term()
		{
			//arrange
			string text = "Lorem ipsum dolor sit amet";
			XElement element = XElement.Parse("<item><term>" + text + "</term></item>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(false, result.IsHeader);
			Assert.AreEqual(text, result.Term[0].ToString());
			Assert.AreEqual(null, result.Description);
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Data_TermDescription()
		{
			//arrange
			string text = "Lorem ipsum dolor sit amet";
			string description = "Vivamus odio justo, bibendum non rutrum ac.";
			XElement element = XElement.Parse("<item><term>" + text + "</term><description>" + description + "</description></item>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(false, result.IsHeader);
			Assert.AreEqual(text, result.Term[0].ToString());
			Assert.AreEqual(description, result.Description[0].ToString());
		}

		[TestMethod]
		public void DotNetCommentListItem_FromXml_Data_Description()
		{
			//arrange
			string description = "Vivamus odio justo, bibendum non rutrum ac.";
			XElement element = XElement.Parse("<item><description>" + description + "</description></item>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentListItem result = DotNetCommentListItem.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(false, result.IsHeader);
			Assert.AreEqual(null, result.Term);
			Assert.AreEqual(description, result.Description[0].ToString());
		}
	}
}
