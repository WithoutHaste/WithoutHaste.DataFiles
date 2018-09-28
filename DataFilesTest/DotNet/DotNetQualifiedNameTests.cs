using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetQualifiedNameTests
	{
		[TestMethod]
		public void DotNetQualifiedName_FromXml_TypeName_Simple()
		{
			//arrange
			string input = "T:A.B.MyType";
			string expectedLocalName = "MyType";
			string expectedQualifiedName = "A.B.MyType";
			string expectedFullNamespace = "A.B";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_TypeName_OneGeneric()
		{
			//arrange
			string input = "T:A.B.MyType`1";
			string expectedLocalName = "MyType<T>";
			string expectedQualifiedName = "A.B.MyType<T>";
			string expectedFullNamespace = "A.B";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_TypeName_TwoGeneric()
		{
			//arrange
			string input = "T:A.B.MyType`2";
			string expectedLocalName = "MyType<T,U>";
			string expectedQualifiedName = "A.B.MyType<T,U>";
			string expectedFullNamespace = "A.B";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_TypeName_NestedInOneGeneric()
		{
			//arrange
			string input = "T:A.B.MyType`1.MyNestedType";
			string expectedLocalName = "MyNestedType";
			string expectedQualifiedName = "A.B.MyType<T>.MyNestedType";
			string expectedFullNamespace = "A.B.MyType<T>";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_MethodName_SimpleZeroParameters()
		{
			//arrange
			string input = "M:A.B.MyType.MyMethod";
			string expectedLocalName = "MyMethod";
			string expectedQualifiedName = "A.B.MyType.MyMethod";
			string expectedFullNamespace = "A.B.MyType";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_MethodName_SimpleOneParameter()
		{
			//arrange
			string input = "M:A.B.MyType.MyMethod(System.String)";
			string expectedLocalName = "MyMethod";
			string expectedQualifiedName = "A.B.MyType.MyMethod";
			string expectedFullNamespace = "A.B.MyType";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_MethodName_SimpleTwoParameters()
		{
			//arrange
			string input = "M:A.B.MyType.MyMethod(System.String,System.Integer)";
			string expectedLocalName = "MyMethod";
			string expectedQualifiedName = "A.B.MyType.MyMethod";
			string expectedFullNamespace = "A.B.MyType";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_MethodName_OneGeneric()
		{
			//arrange
			string input = "M:A.B.MyType.MyMethod``1";
			string expectedLocalName = "MyMethod<A>";
			string expectedQualifiedName = "A.B.MyType.MyMethod<A>";
			string expectedFullNamespace = "A.B.MyType";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_MethodName_TwoGenerics()
		{
			//arrange
			string input = "M:A.B.MyType.MyMethod``2";
			string expectedLocalName = "MyMethod<A,B>";
			string expectedQualifiedName = "A.B.MyType.MyMethod<A,B>";
			string expectedFullNamespace = "A.B.MyType";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_FieldName_Simple()
		{
			//arrange
			string input = "F:A.B.MyType.MyField";
			string expectedLocalName = "MyField";
			string expectedQualifiedName = "A.B.MyType.MyField";
			string expectedFullNamespace = "A.B.MyType";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_FieldName_InOneGeneric()
		{
			//arrange
			string input = "F:A.B.MyType`1.MyField";
			string expectedLocalName = "MyField";
			string expectedQualifiedName = "A.B.MyType<T>.MyField";
			string expectedFullNamespace = "A.B.MyType<T>";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_PropertyName_Simple()
		{
			//arrange
			string input = "P:A.B.MyType.MyProperty";
			string expectedLocalName = "MyProperty";
			string expectedQualifiedName = "A.B.MyType.MyProperty";
			string expectedFullNamespace = "A.B.MyType";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_PropertyName_InOneGeneric()
		{
			//arrange
			string input = "P:A.B.MyType`1.MyProperty";
			string expectedLocalName = "MyProperty";
			string expectedQualifiedName = "A.B.MyType<T>.MyProperty";
			string expectedFullNamespace = "A.B.MyType<T>";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_EventName_Simple()
		{
			//arrange
			string input = "E:A.B.MyType.MyEvent";
			string expectedLocalName = "MyEvent";
			string expectedQualifiedName = "A.B.MyType.MyEvent";
			string expectedFullNamespace = "A.B.MyType";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_EventName_InOneGeneric()
		{
			//arrange
			string input = "E:A.B.MyType`1.MyEvent";
			string expectedLocalName = "MyEvent";
			string expectedQualifiedName = "A.B.MyType<T>.MyEvent";
			string expectedFullNamespace = "A.B.MyType<T>";
			//act
			DotNetQualifiedName result = DotNetQualifiedName.FromVisualStudioXml(input);
			//assert
			Assert.AreEqual(expectedLocalName, result.LocalName);
			Assert.AreEqual(expectedQualifiedName, result.FullName);
			Assert.AreEqual(expectedFullNamespace, result.FullNamespace);
		}
	}
}
