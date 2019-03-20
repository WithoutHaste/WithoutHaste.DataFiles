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
	public class DotNetFieldTests
	{
		protected class A
		{
			public int IntField = 0;

			public int IntProperty { get; set; }
		}

		protected class B<T,U>
		{
			public T TField;
		}

		protected class C
		{
			public A FieldType;

			public List<A> FieldList;

			public A[] FieldArray;
		}

		[TestInitialize]
		public void Initialize()
		{
#if DATAFILES_TARGET_20 || DATAFILES_TARGET_30
			DotNetSettings.UseDefaultQualifiedNameConverter(false);
#else
			DotNetSettings.QualifiedNameConverter = null;
#endif
		}

		[TestCleanup]
		public void Cleanup()
		{
#if DATAFILES_TARGET_20 || DATAFILES_TARGET_30
			DotNetSettings.UseDefaultQualifiedNameConverter(true);
#else
			DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
#endif
		}

		[TestMethod]
		public void DotNetField_Assembly_GetType()
		{
			//arrange
			Type type = typeof(A);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:A'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetField.FromVisualStudioXml(XElement.Parse("<member name='F:A.IntField'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.Fields[0].TypeName);
			Assert.AreEqual("System.Int32", dotNetType.Fields[0].TypeName.FullName);
		}

		[TestMethod]
		public void DotNetField_Assembly_GenericTypeField()
		{
			//arrange
			Type type = typeof(B<,>);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetFieldTests.B'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetField.FromVisualStudioXml(XElement.Parse("<member name='F:DataFilesTest.DotNetFieldTests.B.TField'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.Fields[0].TypeName);
			Assert.AreEqual("T", dotNetType.Fields[0].TypeName.FullName);
		}

		[TestMethod]
		public void DotNetField_Assembly_ArrayTypeField()
		{
			//arrange
			Type type = typeof(C);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetFieldTests.C'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetField.FromVisualStudioXml(XElement.Parse("<member name='F:DataFilesTest.DotNetFieldTests.C.FieldArray'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.Fields[0].TypeName);
			Assert.AreEqual("DataFilesTest.DotNetFieldTests.A[]", dotNetType.Fields[0].TypeName.FullName);
		}

		[TestMethod]
		public void DotNetField_Assembly_ListTypeField()
		{
			//arrange
			Type type = typeof(C);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetFieldTests.C'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetField.FromVisualStudioXml(XElement.Parse("<member name='F:DataFilesTest.DotNetFieldTests.C.FieldList'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.Fields[0].TypeName);
			Assert.AreEqual("System.Collections.Generic.List<DataFilesTest.DotNetFieldTests.A>", dotNetType.Fields[0].TypeName.FullName);
		}
	}
}
