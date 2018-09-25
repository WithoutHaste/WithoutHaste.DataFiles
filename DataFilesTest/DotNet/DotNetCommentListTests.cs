using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	class DotNetCommentListTests
	{
		[TestMethod]
		public void DotNetCommentList_FromXml_NoType_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<list/>");
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(false, list.IsNumbered);
			Assert.AreEqual(0, list.Length);
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_BulletType_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<list type='bullet'/>");
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(false, list.IsNumbered);
			Assert.AreEqual(0, list.Length);
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_NumberType_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<list type='number'/>");
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(true, list.IsNumbered);
			Assert.AreEqual(0, list.Length);
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_TableType_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<list type='table'/>");
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentTable);
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_BulletType_Full()
		{
			//arrange
			string items = "<listheader>H1</listheader><item>I1</item><item>I2</item><item>I3</item>";
			XElement element = XElement.Parse("<list type='bullet'>" + items + "</list>");
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(false, list.IsNumbered);
			Assert.AreEqual(4, list.Length);
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_NumberType_Full()
		{
			//arrange
			string items = "<listheader>H1</listheader><item>I1</item><item>I2</item><item>I3</item>";
			XElement element = XElement.Parse("<list type='number'>" + items + "</list>");
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(false, list.IsNumbered);
			Assert.AreEqual(4, list.Length);
		}
	}
}
