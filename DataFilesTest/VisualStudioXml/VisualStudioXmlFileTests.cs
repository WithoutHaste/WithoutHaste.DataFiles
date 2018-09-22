using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class VisualStudioXmlFileTests
	{
		private const string ASSEMBLY_NAME = "Test";

		[TestMethod]
		public void DotNetDocumentationFile_LoadFromFile()
		{
			//arrange
			string filename = "data/DotNetDocumentationFile_Assembly.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			//assert
			Assert.AreEqual(ASSEMBLY_NAME, file.AssemblyName);
		}

		[TestMethod]
		public void DotNetDocumentationFile_LoadFromXDocument()
		{
			//arrange
			XDocument document = XDocument.Load("data/DotNetDocumentationFile_Assembly.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(document);
			//assert
			Assert.AreEqual(ASSEMBLY_NAME, file.AssemblyName);
		}

		[TestMethod]
		public void DotNetDocumentationFile_QualifiedName_JustLocal()
		{
			//arrange
			string name = "Test";
			//act
			DotNetQualifiedName result = new DotNetQualifiedName(name);
			//assert
			Assert.AreEqual(name, result.LocalName);
			Assert.AreEqual(name, result.FullName);
			Assert.IsNull(result.FullNamespace);
		}

		[TestMethod]
		public void DotNetDocumentationFile_QualifiedName_OneNamespace()
		{
			//arrange
			string namespaceA = "A";
			string name = "Test";
			string qualifiedName = namespaceA + "." + name;
			//act
			DotNetQualifiedName result = new DotNetQualifiedName(qualifiedName);
			//assert
			Assert.AreEqual(name, result.LocalName);
			Assert.AreEqual(qualifiedName, result.FullName);
			Assert.AreEqual(namespaceA, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetDocumentationFile_QualifiedName_TwoNamespaces()
		{
			//arrange
			string namespaceA = "A";
			string namespaceB = "B";
			string name = "Test";
			string qualifiedNamespace = namespaceA + "." + namespaceB;
			string qualifiedName = qualifiedNamespace + "." + name;
			//act
			DotNetQualifiedName result = new DotNetQualifiedName(qualifiedName);
			//assert
			Assert.AreEqual(name, result.LocalName);
			Assert.AreEqual(qualifiedName, result.FullName);
			Assert.AreEqual(qualifiedNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetDocumentationFile_QualifiedNameFromXml_CategoryIndicator()
		{
			//arrange
			string categoryIndicator = "M";
			string namespaceA = "A";
			string namespaceB = "B";
			string name = "Test";
			string qualifiedNamespace = namespaceA + "." + namespaceB;
			string qualifiedName = qualifiedNamespace + "." + name;
			string xmlName = categoryIndicator + ":" + qualifiedName;
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(xmlName);
			//assert
			Assert.AreEqual(name, result.LocalName);
			Assert.AreEqual(qualifiedName, result.FullName);
			Assert.AreEqual(qualifiedNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetDocumentationFile_QualifiedNameFromXml_CategoryIndicator_OneGenericType()
		{
			//arrange
			string categoryIndicator = "M";
			string namespaceA = "A";
			string namespaceB = "B";
			string name = "Test";
			string qualifiedNamespace = namespaceA + "." + namespaceB;
			string qualifiedName = qualifiedNamespace + "." + name;
			string xmlName = categoryIndicator + ":" + qualifiedName + "`1";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(xmlName);
			//assert
			Assert.AreEqual(name + "<T>", result.LocalName);
			Assert.AreEqual(qualifiedName + "<T>", result.FullName);
			Assert.AreEqual(qualifiedNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetDocumentationFile_QualifiedNameFromXml_CategoryIndicator_TwoGenericTypes()
		{
			//arrange
			string categoryIndicator = "M";
			string namespaceA = "A";
			string namespaceB = "B";
			string name = "Test";
			string qualifiedNamespace = namespaceA + "." + namespaceB;
			string qualifiedName = qualifiedNamespace + "." + name;
			string xmlName = categoryIndicator + ":" + qualifiedName + "`2";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(xmlName);
			//assert
			Assert.AreEqual(name + "<T,U>", result.LocalName);
			Assert.AreEqual(qualifiedName + "<T,U>", result.FullName);
			Assert.AreEqual(qualifiedNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetDocumentationFile_QualifiedNameFromXml_CategoryIndicator_TwoDigitGenericTypes()
		{
			//arrange
			string categoryIndicator = "M";
			string namespaceA = "A";
			string namespaceB = "B";
			string name = "Test";
			string qualifiedNamespace = namespaceA + "." + namespaceB;
			string qualifiedName = qualifiedNamespace + "." + name;
			string xmlName = categoryIndicator + ":" + qualifiedName + "`10";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(xmlName);
			//assert
			Assert.AreEqual(name + "<T,U,V,W,T2,U2,V2,W2,T3,U3>", result.LocalName);
			Assert.AreEqual(qualifiedName + "<T,U,V,W,T2,U2,V2,W2,T3,U3>", result.FullName);
			Assert.AreEqual(qualifiedNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetDocumentationFile_CommentFromXml_temp()
		{
			DotNetType member = new DotNetType(new DotNetQualifiedName("Namespace.Name"));
			member.ParseVisualStudioXmlDocumentation(XElement.Parse("<member name='T:Demo.DoubleGenericClass`2'><summary>Generic class <see cref='Test'/> with two generic types.</summary><typeparam name = 'T'>The generic type.</typeparam><typeparam name = 'U'>The other generic type.</typeparam></member>"));
		}
	}
}
