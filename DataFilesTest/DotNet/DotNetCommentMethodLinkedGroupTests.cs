using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentMethodLinkedGroupTests
	{
		[TestMethod]
		public void DotNetCommentMethodLinkedGroup_FromXml_MethodLinkWithParameters()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string methodName = "MyMethod";
			string parameters = "(int, string)";
			string qualifiedName = fullNamespace + "." + typeName + "." + methodName;
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + parameters + "' />");
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentMethodLinkedGroup);
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			Assert.AreEqual(0, result.Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentMethodLinkedGroup_FromXml_MethodLinkWithoutParameters()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string methodName = "MyMethod";
			string parameters = "";
			string qualifiedName = fullNamespace + "." + typeName + "." + methodName;
			XElement element = XElement.Parse("<permission cref='M:" + qualifiedName + parameters + "' />");
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentMethodLinkedGroup);
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			Assert.AreEqual(0, result.Comments.Count);
		}
	}
}
