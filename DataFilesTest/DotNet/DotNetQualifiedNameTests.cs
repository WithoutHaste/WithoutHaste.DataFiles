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

		[TestMethod]
		public void DotNetQualifiedName_FromXml_Equality_Different()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("A.B.C");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("D.E.F");
			//act
			bool result = (a == b);
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_Equality_SameLocalOnly()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("A.B.C");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("D.E.C");
			//act
			bool result = (a == b);
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_Equality_SameNamespaceOnly()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("A.B.C");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("A.B.F");
			//act
			bool result = (a == b);
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_Equality_OneNull()
		{
			//arrange
			DotNetQualifiedName a = null;
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("D.E.F");
			//act
			bool result = (a == b);
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_Equality_OneNamespaceNull()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("F");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("D.E.F");
			//act
			bool result = (a == b);
			//assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_Equality_Same()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("A.B.C");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("A.B.C");
			//act
			bool result = (a == b);
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_FromXml_Equality_BothNull()
		{
			//arrange
			DotNetQualifiedName a = null;
			DotNetQualifiedName b = null;
			//act
			bool result = (a == b);
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_Equality_ListContains()
		{
			//arrange
			List<DotNetQualifiedName> list = new List<DotNetQualifiedName>() {
				DotNetQualifiedName.FromVisualStudioXml("System"),
				DotNetQualifiedName.FromVisualStudioXml("System.Collections.Generic"),
				DotNetQualifiedName.FromVisualStudioXml("System.Linq"),
				DotNetQualifiedName.FromVisualStudioXml("System.Text"),
				DotNetQualifiedName.FromVisualStudioXml("System.Threading.Tasks")
			};
			DotNetQualifiedName target = DotNetQualifiedName.FromVisualStudioXml("System.Text");
			//act
			bool result = list.Contains(target);
			//assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_CompareTo_DifferentDepths_AShorterThanB()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("System.Collections");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("System.Collections.Generic");
			//act
			int result = a.CompareTo(b);
			//assert
			Assert.AreEqual(-1, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_CompareTo_DifferentDepths_ALongerThanB()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("System.Collections.Generic");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("System.Collections");
			//act
			int result = a.CompareTo(b);
			//assert
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_CompareTo_SameDepths_ALessThanB()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("System.Collections.A");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("System.Collections.B");
			//act
			int result = a.CompareTo(b);
			//assert
			Assert.AreEqual(-1, result);
		}

		[TestMethod]
		public void DotNetQualifiedName_CompareTo_SameDepths_AGreaterThanB()
		{
			//arrange
			DotNetQualifiedName a = DotNetQualifiedName.FromVisualStudioXml("System.Collections.C");
			DotNetQualifiedName b = DotNetQualifiedName.FromVisualStudioXml("System.Collections.B");
			//act
			int result = a.CompareTo(b);
			//assert
			Assert.AreEqual(1, result);
		}
	}
}
