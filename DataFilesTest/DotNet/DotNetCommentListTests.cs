using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentListTests
	{
		[TestMethod]
		public void DotNetCommentList_FromXml_NoType_Empty()
		{
			//arrange
			XElement element = XElement.Parse("<list/>", LoadOptions.PreserveWhitespace);
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
			XElement element = XElement.Parse("<list type='bullet'/>", LoadOptions.PreserveWhitespace);
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
			XElement element = XElement.Parse("<list type='number'/>", LoadOptions.PreserveWhitespace);
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
			XElement element = XElement.Parse("<list type='table'/>", LoadOptions.PreserveWhitespace);
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
			XElement element = XElement.Parse("<list type='bullet'>" + items + "</list>", LoadOptions.PreserveWhitespace);
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
			XElement element = XElement.Parse("<list type='number'>" + items + "</list>", LoadOptions.PreserveWhitespace);
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(true, list.IsNumbered);
			Assert.AreEqual(4, list.Length);
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_NestedListInItem_Ignore()
		{
			//arrange
			string items = "<listheader>H1</listheader><item>I1</item><item><list type='number'><item>Nested 1</item><item>Nested 2</item></list></item><item>I3</item>";
			XElement element = XElement.Parse("<list type='bullet'>" + items + "</list>", LoadOptions.PreserveWhitespace);
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(4, list.Length);
			Assert.AreEqual(null, list[2].Term);
			Assert.AreEqual(null, list[2].Description);
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_NestedList_Ignore()
		{
			//arrange
			string items = "<listheader>H1</listheader><item>I1</item><list type='number'><item>Nested 1</item><item>Nested 2</item></list><item>I3</item>";
			XElement element = XElement.Parse("<list type='bullet'>" + items + "</list>", LoadOptions.PreserveWhitespace);
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(4, list.Length);
			Assert.AreEqual("I1", list[1].Term[0].ToString());
			Assert.AreEqual(null, list[2].Term);
			Assert.AreEqual("I3", list[3].Term[0].ToString());
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_InlineTagsInItem()
		{
			//arrange
			XElement element = XElement.Parse("<list type='number'><item>ABC <see cref='OtherClass'/> DEF</item></list>", LoadOptions.PreserveWhitespace);
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(1, list.Length);
			DotNetCommentListItem item = list[0];
			Assert.AreEqual(3, item.Term.Count);
			Assert.IsNull(item.Description);
		}

		[TestMethod]
		public void DotNetCommentList_FromXml_NumberType_WhitespaceInItem()
		{
			//arrange
			XElement element = XElement.Parse(@"
<list type='number'>
<listheader>
	<term>Header Term</term>
	<description>Header Description</description>
</listheader>
<item>
	<term>Term A</term>
	<description>Description A</description>
</item>
<item>
	<term>Term B</term>
	<description>Description B</description>
</item>
<item>
	<term>Term C</term>
	<description>Description C</description>
</item>
</list>
", LoadOptions.PreserveWhitespace);
			//act
			DotNetComment result = DotNetCommentList.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentList);
			DotNetCommentList list = result as DotNetCommentList;
			Assert.AreEqual(true, list.IsNumbered);
			Assert.AreEqual(4, list.Length);
			Assert.AreEqual("Term A", list[1].Term[0].ToString());
			Assert.AreEqual("Description A", list[1].Description[0].ToString());
		}
	}
}
