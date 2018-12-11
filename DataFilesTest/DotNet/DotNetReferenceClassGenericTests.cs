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
	public class DotNetReferenceClassGenericTests
	{
		protected class ClassGeneric<Apple>
		{
			public Apple FieldA;

			public Apple PropertyA { get; set; }

			public Apple this[Apple a] { get { return default(Apple); } }

			public ClassGeneric(Apple a) { }

			public Apple MethodA() { return default(Apple); }
		}
		
		[TestMethod]
		public void DotNetReferenceClassGeneric_FromAssembly_GenericConstructor()
		{
			//arrange
			XElement typeXmlElement = XElement.Parse("<member name='T:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1' />", LoadOptions.PreserveWhitespace);
			XElement methodXmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1.#ctor(`0)' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(ClassGeneric<>);
			TypeInfo typeInfo = type.GetTypeInfo();
			//act
			DotNetType typeResult = DotNetType.FromVisualStudioXml(typeXmlElement);
			DotNetMethod methodResult = DotNetMethod.FromVisualStudioXml(methodXmlElement);
			typeResult.AddMember(methodResult);
			typeResult.AddAssemblyInfo(typeInfo, typeResult.Name);
			//assert
			Assert.AreEqual(MethodCategory.Normal, methodResult.Category);
			Assert.AreEqual("Apple", methodResult.MethodName.Parameters[0].TypeName);
		}

		[TestMethod]
		public void DotNetReferenceClassGeneric_FromAssembly_ReturnType()
		{
			//arrange
			XElement typeXmlElement = XElement.Parse("<member name='T:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1' />", LoadOptions.PreserveWhitespace);
			XElement methodXmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1.MethodA' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(ClassGeneric<>);
			TypeInfo typeInfo = type.GetTypeInfo();
			//act
			DotNetType typeResult = DotNetType.FromVisualStudioXml(typeXmlElement);
			DotNetMethod methodResult = DotNetMethod.FromVisualStudioXml(methodXmlElement);
			typeResult.AddMember(methodResult);
			typeResult.AddAssemblyInfo(typeInfo, typeResult.Name);
			//assert
			Assert.AreEqual(MethodCategory.Normal, methodResult.Category);
			Assert.AreEqual("Apple", methodResult.MethodName.ReturnTypeName);
		}
		
		[TestMethod]
		public void DotNetReferenceClassGeneric_FromAssembly_Field()
		{
			//arrange
			Type type = typeof(ClassGeneric<>);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetField.FromVisualStudioXml(XElement.Parse("<member name='F:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1.FieldA'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.Fields[0].TypeName);
			Assert.AreEqual("Apple", dotNetType.Fields[0].TypeName.FullName);
		}

		[TestMethod]
		public void DotNetReferenceClassGeneric_FromAssembly_Property()
		{
			//arrange
			Type type = typeof(ClassGeneric<>);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1.PropertyA'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.Properties[0].TypeName);
			Assert.AreEqual("Apple", dotNetType.Properties[0].TypeName.FullName);
		}

		[TestMethod]
		public void DotNetReferenceClassGeneric_FromAssembly_Indexer()
		{
			//arrange
			Type type = typeof(ClassGeneric<>);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetProperty.FromVisualStudioXml(XElement.Parse("<member name='P:DataFilesTest.DotNetReferenceClassGenericTests.ClassGeneric`1.Item(`0)'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsTrue(dotNetType.Properties[0] is DotNetIndexer);
			DotNetIndexer indexer = (dotNetType.Properties[0] as DotNetIndexer);
			Assert.AreEqual(1, indexer.Parameters.Count());
			Assert.AreEqual("Apple", indexer.Parameters[0].TypeName);
		}

		[TestMethod]
		public void DotNetReferenceClassGeneric_GetLocalized_GenericType_NotLocal()
		{
			//arrange
			DotNetReferenceClassGeneric a = DotNetReferenceClassGeneric.FromVisualStudioXml("`0");
			DotNetQualifiedTypeName b = DotNetQualifiedTypeName.FromVisualStudioXml("A.B.C");
			//act
			DotNetReferenceClassGeneric result = a.GetLocalized(b);
			//assert
			Assert.AreEqual(a, result);
		}
	}
}
