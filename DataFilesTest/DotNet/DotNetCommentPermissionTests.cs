using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentPermissionTests
	{
		[TestMethod]
		public void DotNetCommentPermission_FromXml_FullyQualifiedLink()
		{
			//arrange
			string _namespace = "A";
			string typeName = "B";
			string fieldName = "C";
			string fullName = _namespace + "." + typeName + "." + fieldName;
			XElement element = XElement.Parse("<permission cref='F:" + fullName + "'>Comments</permission>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(fullName, result.QualifiedLink.Name.FullName);
			Assert.AreEqual(fieldName, result.QualifiedLink.Name.LocalName);
			Assert.AreEqual(_namespace + "." + typeName, result.QualifiedLink.Name.FullNamespace);
		}
		[TestMethod]
		public void DotNetCommentPermission_FromXml_Indexer()
		{
			//arrange
			XElement element = XElement.Parse("<permission cref='P:A.B.Item(System.Int32)'>Comments</permission>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.IsTrue(result is DotNetCommentMethodLinkedGroup);
		}


		[TestMethod]
		public void DotNetCommentPermission_FromXml_FullyQualifiedMethodLink()
		{
			//arrange
			string _namespace = "A";
			string typeName = "B";
			string methodName = "C";
			string parameters = "(int,string)";
			string fullName = _namespace + "." + typeName + "." + methodName;
			XElement element = XElement.Parse("<permission cref='M:" + fullName + parameters + "'>Comments</permission>", LoadOptions.PreserveWhitespace);
			//act
			DotNetCommentQualifiedLinkedGroup result = DotNetCommentQualifiedLinkedGroup.FromVisualStudioXml(element);
			//assert
			Assert.AreEqual(fullName, result.QualifiedLink.Name.FullName);
			Assert.AreEqual(methodName, result.QualifiedLink.Name.LocalName);
			Assert.AreEqual(_namespace + "." + typeName, result.QualifiedLink.Name.FullNamespace);
		}
	}
}
