using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentTypeParameterTests
	{
		[TestMethod]
		public void DotNetCommentTypeParameter_FromXml_Empty()
		{
			//arrange
			string name = "test";
			XElement element = XElement.Parse("<typeparam name='" + name + "' />");
			//act
			DotNetComment result = DotNetCommentTypeParameter.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentGroup);
			Assert.AreEqual(name, (result as DotNetCommentGroup).Link.FullName);
			Assert.AreEqual(0, (result as DotNetCommentGroup).Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentTypeParameter_Constructor()
		{
			//arrange
			string name = "test";
			//act
			DotNetCommentTypeParameter result = new DotNetCommentTypeParameter(name);
			//assert
			Assert.AreEqual(name, result.FullName);
		}
	}
}
