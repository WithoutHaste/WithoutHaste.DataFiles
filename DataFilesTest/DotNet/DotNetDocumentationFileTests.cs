using System;
using System.Collections.Generic;
using System.Reflection;
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
		public void DotNetDocumentationFile_CommentFromXml_Full()
		{
			//arrange
			XDocument document = XDocument.Load("data/DotNetDocumentationFile_Full.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(document);
			//assert
			Assert.AreEqual(5, file.TypeCount);

			DotNetType typeA = file.Types[0];
			Assert.AreEqual(2, typeA.NestedTypeCount);
			Assert.AreEqual("Test.TypeA", typeA.Name.FullName);
			Assert.AreEqual(4, typeA.CommentCount);
			Assert.AreEqual(1, typeA.Methods.Count);
			Assert.AreEqual(2, typeA.Fields.Count);
			Assert.AreEqual(3, typeA.Properties.Count);
			Assert.AreEqual(1, typeA.Events.Count);

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

			DotNetType typeD = file.Types[1];
			Assert.AreEqual("Test.SingleGenericTypeD<T>", typeD.Name.FullName);
			Assert.AreEqual(0, typeD.CommentCount);
			Assert.AreEqual(1, typeD.Methods.Count);

			DotNetType typeE = file.Types[2];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>", typeE.Name.FullName);
			Assert.AreEqual(0, typeE.CommentCount);
			Assert.AreEqual(3, typeE.Methods.Count);

			DotNetMethod methodAA = typeA.Methods[0];
			Assert.AreEqual("Test.TypeA.MethodAA", methodAA.Name.FullName);
			Assert.AreEqual(8, methodAA.CommentCount);
			Assert.AreEqual(false, methodAA is DotNetMethodConstructor);
			Assert.AreEqual(false, methodAA is DotNetMethodOperator);
			Assert.AreEqual(0, methodAA.Parameters.Count);

			DotNetMethod methodBA = typeB.Methods[0];
			Assert.AreEqual("Test.TypeA.NestedTypeB.MethodBA", methodBA.Name.FullName);
			Assert.AreEqual(0, methodBA.CommentCount);
			Assert.AreEqual(false, methodBA is DotNetMethodConstructor);
			Assert.AreEqual(false, methodBA is DotNetMethodOperator);
			Assert.AreEqual(2, methodBA.Parameters.Count);

			DotNetMethod methodCA = typeC.Methods[0];
			Assert.AreEqual("Test.TypeA.NestedTypeB.SubNestedTypeC.MethodCA", methodCA.Name.FullName);
			Assert.AreEqual(0, methodCA.CommentCount);
			Assert.AreEqual(false, methodCA is DotNetMethodConstructor);
			Assert.AreEqual(false, methodCA is DotNetMethodOperator);
			Assert.AreEqual(1, methodCA.Parameters.Count);

			DotNetMethod methodCConstructor = typeC.Methods[1];
			Assert.AreEqual("Test.TypeA.NestedTypeB.SubNestedTypeC.SubNestedTypeC", methodCConstructor.Name.FullName);
			Assert.AreEqual(0, methodCConstructor.CommentCount);
			Assert.AreEqual(true, methodCConstructor is DotNetMethodConstructor);
			Assert.AreEqual(false, methodCConstructor is DotNetMethodOperator);
			Assert.AreEqual(0, methodCConstructor.Parameters.Count);

			DotNetMethod methodDAddition = typeD.Methods[0];
			Assert.AreEqual("Test.SingleGenericTypeD<T>.Addition", methodDAddition.Name.FullName);
			Assert.AreEqual(0, methodDAddition.CommentCount);
			Assert.AreEqual(false, methodDAddition is DotNetMethodConstructor);
			Assert.AreEqual(true, methodDAddition is DotNetMethodOperator);
			Assert.AreEqual(2, methodDAddition.Parameters.Count);
			Assert.AreEqual("T", methodDAddition.Parameters[0].FullTypeName);
			Assert.AreEqual("Test.SingleGenericTypeD<T>", methodDAddition.Parameters[1].FullTypeName);

			DotNetMethod methodEConstructor = typeE.Methods[0];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>.DoubleGenericTypeE<T,U>", methodEConstructor.Name.FullName);
			Assert.AreEqual(0, methodEConstructor.CommentCount);
			Assert.AreEqual(true, methodEConstructor is DotNetMethodConstructor);
			Assert.AreEqual(false, methodEConstructor is DotNetMethodOperator);
			Assert.AreEqual(2, methodEConstructor.Parameters.Count);
			Assert.AreEqual("U", methodEConstructor.Parameters[0].FullTypeName);
			Assert.AreEqual("T", methodEConstructor.Parameters[1].FullTypeName);

			DotNetMethod methodEA = typeE.Methods[1];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>.MethodEA<A>", methodEA.Name.FullName);
			Assert.AreEqual(0, methodEA.CommentCount);
			Assert.AreEqual(false, methodEA is DotNetMethodConstructor);
			Assert.AreEqual(false, methodEA is DotNetMethodOperator);
			Assert.AreEqual(3, methodEA.Parameters.Count);
			Assert.AreEqual("System.String", methodEA.Parameters[0].FullTypeName);
			Assert.AreEqual("A", methodEA.Parameters[1].FullTypeName);
			Assert.AreEqual("U", methodEA.Parameters[2].FullTypeName);

			DotNetMethod methodEB = typeE.Methods[2];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>.MethodEB<A>", methodEB.Name.FullName);
			Assert.AreEqual(0, methodEB.CommentCount);
			Assert.AreEqual(false, methodEB is DotNetMethodConstructor);
			Assert.AreEqual(false, methodEB is DotNetMethodOperator);
			Assert.AreEqual(1, methodEB.Parameters.Count);
			Assert.AreEqual("System.Collections.Generic.List<Test.SingleGenericTypeD<T>>", methodEB.Parameters[0].FullTypeName);
		}

		[TestMethod]
		public void DotNetDocumentationFile_CommentFromXml_NameConflict()
		{
			//arrange
			XDocument document = XDocument.Load("data/DotNetDocumentationFile_NameConflict.xml");
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(document);
			//assert
			Assert.AreEqual(7, file.TypeCount);

			DotNetType typeGenericClass = file.Types[1];
			Assert.AreEqual("A.GenericClass<TXXX,UX,V,W,T2X,U2X,V2X,W2,T3,U3,V3,W3>", typeGenericClass.Name.FullName);

			DotNetType typeB = file.Types[0];
			DotNetMethod methodBGenericMethod = typeB.Methods[2];
			Assert.AreEqual("A.B.GenericMethod<AXXX,BX,CX,A2,B2,C2,A3,B3,C3,A4,B4,C4>", methodBGenericMethod.Name.FullName);
		}

		[TestMethod]
		public void DotNetDocumentationFile_AddAssemblyInfo()
		{
			//DotNetDocumentationFile file = new DotNetDocumentationFile();
			//file.AddAssemblyInfo("E:/Github/EarlyDocs/Demo/bin/Debug/Demo.dll");

			//arrange
			//act
			//assert
		}

	}
}
