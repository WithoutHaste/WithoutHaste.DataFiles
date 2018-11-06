using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentQualifiedLinkedGroupTests
	{
		[TestMethod]
		public void DotNetCommentQualifiedLinkedGroup_FromXml_TypeLink_Empty()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "ArgumentException";
			string qualifiedName = fullNamespace + "." + typeName;
			XElement element = XElement.Parse("<exception cref='" + qualifiedName + "' />", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			Assert.AreEqual(0, result.Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentQualifiedLinkedGroup_FromXml_TypeLink_Full()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "ArgumentException";
			string qualifiedName = fullNamespace + "." + typeName;
			string comments = Utilities.LoadText("data/DotNetCommentGroup_XmlCommentsNestedInTag.txt");
			XElement element = XElement.Parse("<exception cref='" + qualifiedName + "'>" + comments + "</exception>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			Assert.AreEqual(14, result.Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentQualifiedLinkedGroup_FromXml_MethodLink_Empty()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string methodName = "MyMethod";
			string parameters = "(int, string)";
			string qualifiedName = fullNamespace + "." + typeName + "." + methodName;
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + parameters + "' />", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			Assert.AreEqual(0, result.Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentQualifiedLinkedGroup_FromXml_MethodLink_Full()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string methodName = "MyMethod";
			string parameters = "(int, string)";
			string qualifiedName = fullNamespace + "." + typeName + "." + methodName;
			string comments = Utilities.LoadText("data/DotNetCommentGroup_XmlCommentsNestedInTag.txt");
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + parameters + "'>" + comments + "</permission>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			Assert.AreEqual(14, result.Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentQualifiedLinkedGroup_FromXml_MemberLink_Empty()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string memberName = "MyMember";
			string qualifiedName = fullNamespace + "." + typeName + "." + memberName;
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + "' />", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			Assert.AreEqual(0, result.Comments.Count);
		}

		[TestMethod]
		public void DotNetCommentQualifiedLinkedGroup_FromXml_MemberLink_Full()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string memberName = "MyMember";
			string qualifiedName = fullNamespace + "." + typeName + "." + memberName;
			string comments = Utilities.LoadText("data/DotNetCommentGroup_XmlCommentsNestedInTag.txt");
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + "'>" + comments + "</permission>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			Assert.AreEqual(14, result.Comments.Count);
		}
	}
}