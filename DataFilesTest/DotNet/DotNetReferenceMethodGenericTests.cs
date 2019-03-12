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
	public class DotNetReferenceMethodGenericTests
	{
		protected class ClassGeneric<Apple>
		{
			public Pear MethodB<Pear>(Apple a, Pear e) { return default(Pear); }
		}
		
		[TestMethod]
		public void DotNetReferenceMethodGeneric_FromAssembly_GenericMethod()
		{
			//arrange
			XElement typeXmlElement = XElement.Parse("<member name='T:DataFilesTest.DotNetReferenceMethodGenericTests.ClassGeneric`1' />", LoadOptions.PreserveWhitespace);
			XElement methodXmlElement = XElement.Parse("<member name='M:DataFilesTest.DotNetReferenceMethodGenericTests.ClassGeneric`1.MethodB``1(`0,``0)' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(ClassGeneric<>);
			//act
			DotNetType typeResult = DotNetType.FromVisualStudioXml(typeXmlElement);
			DotNetMethod methodResult = DotNetMethod.FromVisualStudioXml(methodXmlElement);
			typeResult.AddMember(methodResult);
			typeResult.AddAssemblyInfo(type, typeResult.Name);
			//assert
			Assert.AreEqual(MethodCategory.Normal, methodResult.Category);
			Assert.AreEqual("Apple", methodResult.MethodName.Parameters[0].TypeName);
			Assert.AreEqual("Pear", methodResult.MethodName.Parameters[1].TypeName);
			Assert.AreEqual("Pear", methodResult.MethodName.ReturnTypeName);
		}

		[TestMethod]
		public void DotNetReferenceMethodGeneric_GetLocalized_GenericType_NotLocal()
		{
			//arrange
			DotNetReferenceMethodGeneric a = DotNetReferenceMethodGeneric.FromVisualStudioXml("``0");
			DotNetQualifiedTypeName b = DotNetQualifiedTypeName.FromVisualStudioXml("A.B.C");
			//act
			DotNetReferenceMethodGeneric result = a.GetLocalized(b);
			//assert
			Assert.AreEqual(a, result);
		}
	}
}
