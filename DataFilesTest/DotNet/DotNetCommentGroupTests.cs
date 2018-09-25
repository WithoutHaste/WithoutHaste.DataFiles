using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentGroupTests
	{
		public static string GetXmlCommentsNestedInTag()
		{
			return Utilities.LoadText("data/DotNetCommentGroup_XmlCommentsNestedInTag.txt");
		}

		public static void ValidateXmlCommentsNestedInTag(List<DotNetComment> comments)
		{
			Assert.AreEqual(13, comments.Count);
		}
	}
}
