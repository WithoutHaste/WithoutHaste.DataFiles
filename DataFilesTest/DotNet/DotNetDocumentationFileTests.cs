using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetDocumentationFileTests
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
		public void DotNetDocumentationFile_CommentFromXml_Full()
		{
			//arrange
			XDocument document = XDocument.Load("data/DotNetDocumentationFile_Full.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(document);
			//assert
			Assert.AreEqual(5, file.TypeCount);

			DotNetType typeA = file[0];
			Assert.AreEqual(2, typeA.NestedTypeCount);
			Assert.AreEqual("Test.TypeA", typeA.Name.FullName);
			Assert.AreEqual(4, typeA.CommentCount);
			Assert.AreEqual(1, typeA.Methods.Count);

			DotNetType typeB = typeA.NestedTypes[0];
			Assert.AreEqual(1, typeB.NestedTypeCount);
			Assert.AreEqual("Test.TypeA.NestedTypeB", typeB.Name.FullName);
			Assert.AreEqual(1, typeB.CommentCount);
			Assert.AreEqual(1, typeB.Methods.Count);

			DotNetType typeC = typeB.NestedTypes[0];
			Assert.AreEqual(0, typeC.NestedTypeCount);
			Assert.AreEqual("Test.TypeA.NestedTypeB.SubNestedTypeC", typeC.Name.FullName);
			Assert.AreEqual(6, typeC.CommentCount);
			Assert.AreEqual(2, typeC.Methods.Count);

			DotNetType typeD = file[1];
			Assert.AreEqual("Test.SingleGenericTypeD<T>", typeD.Name.FullName);
			Assert.AreEqual(0, typeD.CommentCount);
			Assert.AreEqual(1, typeD.Methods.Count);

			DotNetType typeE = file[2];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>", typeE.Name.FullName);
			Assert.AreEqual(0, typeE.CommentCount);
			Assert.AreEqual(2, typeE.Methods.Count);

			DotNetMethod methodAA = typeA.Methods[0];
			Assert.AreEqual("Test.TypeA.MethodAA", methodAA.Name.FullName);
			Assert.AreEqual(8, methodAA.CommentCount);
			Assert.AreEqual(false, methodAA.IsConstructor);
			Assert.AreEqual(false, methodAA.IsOperator);
			Assert.AreEqual(0, methodAA.Parameters.Count);

			DotNetMethod methodBA = typeB.Methods[0];
			Assert.AreEqual("Test.TypeA.NestedTypeB.MethodBA", methodBA.Name.FullName);
			Assert.AreEqual(0, methodBA.CommentCount);
			Assert.AreEqual(false, methodBA.IsConstructor);
			Assert.AreEqual(false, methodBA.IsOperator);
			Assert.AreEqual(2, methodBA.Parameters.Count);

			DotNetMethod methodCA = typeC.Methods[0];
			Assert.AreEqual("Test.TypeA.NestedTypeB.SubNestedTypeC.MethodCA", methodCA.Name.FullName);
			Assert.AreEqual(0, methodCA.CommentCount);
			Assert.AreEqual(false, methodCA.IsConstructor);
			Assert.AreEqual(false, methodCA.IsOperator);
			Assert.AreEqual(1, methodCA.Parameters.Count);

			DotNetMethod methodCConstructor = typeC.Methods[1];
			Assert.AreEqual("Test.TypeA.NestedTypeB.SubNestedTypeC.SubNestedTypeC", methodCConstructor.Name.FullName);
			Assert.AreEqual(0, methodCConstructor.CommentCount);
			Assert.AreEqual(true, methodCConstructor.IsConstructor);
			Assert.AreEqual(false, methodCConstructor.IsOperator);
			Assert.AreEqual(0, methodCConstructor.Parameters.Count);

			DotNetMethod methodDAddition = typeD.Methods[0];
			Assert.AreEqual("Test.SingleGenericTypeD<T>.Addition", methodDAddition.Name.FullName);
			Assert.AreEqual(0, methodDAddition.CommentCount);
			Assert.AreEqual(false, methodDAddition.IsConstructor);
			Assert.AreEqual(true, methodDAddition.IsOperator);
			Assert.AreEqual(2, methodDAddition.Parameters.Count);
			Assert.AreEqual("T", methodDAddition.Parameters[0].FullName);
			Assert.AreEqual("Test.SingleGenericTypeD<T>", methodDAddition.Parameters[1].FullName);

			DotNetMethod methodEConstructor = typeE.Methods[0];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>.DoubleGenericTypeE<T,U>", methodEConstructor.Name.FullName);
			Assert.AreEqual(0, methodEConstructor.CommentCount);
			Assert.AreEqual(true, methodEConstructor.IsConstructor);
			Assert.AreEqual(false, methodEConstructor.IsOperator);
			Assert.AreEqual(2, methodEConstructor.Parameters.Count);
			Assert.AreEqual("U", methodEConstructor.Parameters[0].FullName);
			Assert.AreEqual("T", methodEConstructor.Parameters[1].FullName);

			DotNetMethod methodEA = typeE.Methods[1];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>.MethodEA<A>", methodEA.Name.FullName);
			Assert.AreEqual(0, methodEA.CommentCount);
			Assert.AreEqual(false, methodEA.IsConstructor);
			Assert.AreEqual(false, methodEA.IsOperator);
			Assert.AreEqual(3, methodEA.Parameters.Count);
			Assert.AreEqual("System.String", methodEA.Parameters[0].FullName);
			Assert.AreEqual("A", methodEA.Parameters[1].FullName);
			Assert.AreEqual("U", methodEA.Parameters[2].FullName);
		}
	}
}
