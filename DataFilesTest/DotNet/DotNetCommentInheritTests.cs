using System;
using System.Collections.Generic;
using System.IO;
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
			string filename = Utilities.GetPathTo("data/DotNetCommentInherit_TypeFromBaseType.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].SummaryComments.Comments[0], file.Types[1].SummaryComments.Comments[0]);
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_FieldFromBaseType()
		{
			//arrange
			string filename = Utilities.GetPathTo("data/DotNetCommentInherit_TypeFromBaseType.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].Fields.First(f => f.Name.LocalName == "FieldA").SummaryComments.Comments[0], file.Types[1].Fields.First(f => f.Name.LocalName == "FieldA").SummaryComments.Comments[0]);
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_PropertyFromBaseType()
		{
			//arrange
			string filename = Utilities.GetPathTo("data/DotNetCommentInherit_TypeFromBaseType.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].Properties.First(p => p.Name.LocalName == "PropertyA").SummaryComments.Comments[0], file.Types[1].Properties.First(p => p.Name.LocalName == "PropertyA").SummaryComments.Comments[0]);
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_EventFromBaseType()
		{
			//arrange
			string filename = Utilities.GetPathTo("data/DotNetCommentInherit_TypeFromBaseType.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].Events.First(e => e.Name.LocalName == "EventA").SummaryComments.Comments[0], file.Types[1].Events.First(e => e.Name.LocalName == "EventA").SummaryComments.Comments[0]);
		}

		[TestMethod]
		public void DotNetCommentInherit_FromAssembly_MethodFromBaseType()
		{
			//arrange
			string filename = Utilities.GetPathTo("data/DotNetCommentInherit_TypeFromBaseType.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			file.Types.First(t => t.Name.LocalName == "DerivedType").AddAssemblyInfo(typeof(DerivedType), new DotNetQualifiedName("DataFilesTest", "DotNetCommentInheritTests", "DerivedType"));
			file.ResolveInheritedComments();
			//assert
			Assert.AreEqual(2, file.Types.Count);
			Assert.AreEqual(file.Types[0].Methods.First(m => m.Name.LocalName == "MethodA" && m.MethodName.Parameters.Count == 0).SummaryComments.Comments[0], file.Types[1].Methods.First(m => m.Name.LocalName == "MethodA" && m.MethodName.Parameters.Count == 0).SummaryComments.Comments[0]);
		}
	}
}
