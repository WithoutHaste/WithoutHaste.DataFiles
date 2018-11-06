using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentTypeParameterLinkTests
	{
		[TestMethod]
		public void DotNetCommentTypeParameterLink_FromXml()
		{
			//arrange
			string name = "test";
			XElement element = XElement.Parse("<typeparamref name='" + name + "' />", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentTypeParameterLink result = DotNetCommentTypeParameterLink.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(name, result.Name);
		}
	}
}
