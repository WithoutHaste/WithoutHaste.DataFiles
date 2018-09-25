using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentQualifiedLinkTests
	{
		[TestMethod]
		public void DotNetCommentQualifiedLink_FromXml_TypeLink()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "ArgumentException";
			string qualifiedName = fullNamespace + "." + typeName;
			//act
			DotNetCommentQualifiedLink result = DotNetCommentQualifiedLink.FromVisualStudioXml(qualifiedName);
			//assert
			Assert.AreEqual(qualifiedName, result.FullName);
		}
	}
}
