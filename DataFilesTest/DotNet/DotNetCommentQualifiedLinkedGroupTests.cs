using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	class DotNetCommentQualifiedLinkedGroupTests
	{
		[TestMethod]
		public void DotNetCommentQualifiedLinkedGroup_FromXml_TypeLink_Empty()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "ArgumentException";
			string qualifiedName = fullNamespace + "." + typeName;
			XElement element = XElement.Parse("<exception cref='" + qualifiedName + "' />");
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
			XElement element = XElement.Parse("<exception cref='" + qualifiedName + "'>" + DotNetCommentGroupTests.GetXmlCommentsNestedInTag() + "</exception>");
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			DotNetCommentGroupTests.ValidateXmlCommentsNestedInTag(result.Comments);
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
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + parameters + "' />");
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
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + parameters + "'>" + DotNetCommentGroupTests.GetXmlCommentsNestedInTag() + "</permission>");
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			DotNetCommentGroupTests.ValidateXmlCommentsNestedInTag(result.Comments);
		}

		[TestMethod]
		public void DotNetCommentQualifiedLinkedGroup_FromXml_MemberLink_Empty()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string memberName = "MyMember";
			string qualifiedName = fullNamespace + "." + typeName + "." + memberName;
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + "' />");
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
			XElement element = XElement.Parse("<permission cref='" + qualifiedName + "'>" + DotNetCommentGroupTests.GetXmlCommentsNestedInTag() + "</permission>");
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(qualifiedName, result.Link.FullName);
			DotNetCommentGroupTests.ValidateXmlCommentsNestedInTag(result.Comments);
		}
	}
}