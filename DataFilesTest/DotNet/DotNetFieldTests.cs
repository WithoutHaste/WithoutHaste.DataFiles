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

		[TestMethod]
		public void DotNetField_Assembly_GetType()
		{
			//arrange
			Type type = typeof(A);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:A'></member>"));
			dotNetType.AddMember(DotNetField.FromVisualStudioXml(XElement.Parse("<member name='F:A.IntField'></member>")));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.Fields[0].TypeName);
			Assert.AreEqual("System.Int32", dotNetType.Fields[0].TypeName.FullName);
		}

		[TestMethod]
		public void DotNetField_Assembly_GenericTypeField()
		{
			//arrange
			Type type = typeof(B<,>);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:DataFilesTest.DotNetFieldTests.B'></member>"));
			dotNetType.AddMember(DotNetField.FromVisualStudioXml(XElement.Parse("<member name='F:DataFilesTest.DotNetFieldTests.B.TField'></member>")));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.IsNotNull(dotNetType.Fields[0].TypeName);
			Assert.AreEqual("T", dotNetType.Fields[0].TypeName.FullName);
		}
	}
}
