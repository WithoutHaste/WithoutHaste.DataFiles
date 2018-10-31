using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentMethodLinkTests
	{
		[TestMethod]
		public void DotNetCommentMethodLink_FromXml_NoParameters()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string methodName = "MyMethod";
			string parameters = "()";
			string qualifiedName = fullNamespace + "." + typeName + "." + methodName;
			string signature = qualifiedName + parameters;
			//act
			DotNetCommentMethodLink result = DotNetCommentMethodLink.FromVisualStudioXml(signature);
			//assert
			Assert.AreEqual(qualifiedName, result.FullName);
			Assert.AreEqual(signature, result.FullSignature);
		}

		[TestMethod]
		public void DotNetCommentMethodLink_FromXml_OneParameter()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string methodName = "MyMethod";
			string parameters = "(System.Int32)";
			string qualifiedName = fullNamespace + "." + typeName + "." + methodName;
			string signature = qualifiedName + parameters;
			//act
			DotNetCommentMethodLink result = DotNetCommentMethodLink.FromVisualStudioXml(signature);
			//assert
			Assert.AreEqual(qualifiedName, result.FullName);
			Assert.AreEqual(signature, result.FullSignature);
			Assert.AreEqual(1, result.MethodName.Parameters.Count);
		}

		[TestMethod]
		public void DotNetCommentMethodLink_FromXml_TwoParameters()
		{
			//arrange
			string fullNamespace = "System.X";
			string typeName = "MyType";
			string methodName = "MyMethod";
			string parameters = "(System.Int32,System.String)";
			string qualifiedName = fullNamespace + "." + typeName + "." + methodName;
			string signature = qualifiedName + parameters;
			//act
			DotNetCommentMethodLink result = DotNetCommentMethodLink.FromVisualStudioXml(signature);
			//assert
			Assert.AreEqual(qualifiedName, result.FullName);
			Assert.AreEqual(signature, result.FullSignature);
			Assert.AreEqual(2, result.MethodName.Parameters.Count);
		}
	}
}
