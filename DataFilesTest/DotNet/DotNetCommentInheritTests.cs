using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetCommentInheritTests
	{
		public class BaseType
		{
		}

		public class DerivedType : BaseType
		{
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_TypeFromBaseType()
		{
			//arrange
			string filename = "data/DotNetCommentInherit_TypeFromBaseType.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType).GetTypeInfo(), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].SummaryComments.Comments[0], file.Types[1].SummaryComments.Comments[0]);
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_FieldFromBaseType()
		{
			//arrange
			string filename = "data/DotNetCommentInherit_TypeFromBaseType.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType).GetTypeInfo(), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].FindField("FieldA").SummaryComments.Comments[0], file.Types[1].FindField("FieldA").SummaryComments.Comments[0]);
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_PropertyFromBaseType()
		{
			//arrange
			string filename = "data/DotNetCommentInherit_TypeFromBaseType.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType).GetTypeInfo(), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].FindProperty("PropertyA").SummaryComments.Comments[0], file.Types[1].FindProperty("PropertyA").SummaryComments.Comments[0]);
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_EventFromBaseType()
		{
			//arrange
			string filename = "data/DotNetCommentInherit_TypeFromBaseType.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType).GetTypeInfo(), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].FindEvent("EventA").SummaryComments.Comments[0], file.Types[1].FindEvent("EventA").SummaryComments.Comments[0]);
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_MethodFromBaseType()
		{
			//arrange
			string filename = "data/DotNetCommentInherit_TypeFromBaseType.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType).GetTypeInfo(), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].FindMethod("MethodA", new List<DotNetParameter>()).SummaryComments.Comments[0], file.Types[1].FindMethod("MethodA", new List<DotNetParameter>()).SummaryComments.Comments[0]);
		}
	}
}
