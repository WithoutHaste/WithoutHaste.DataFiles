using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	class DotNetCommentParameterLinkTests
	{
		[TestMethod]
		public void DotNetCommentParameterLink_FromXml()
		{
			//arrange
			string name = "test";
			XElement element = XElement.Parse("<paramref name='" + name + "' />");
			//act
			DotNetCommentParameterLink result = DotNetCommentParameterLink.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(name, result.Name);
		}
	}
}
