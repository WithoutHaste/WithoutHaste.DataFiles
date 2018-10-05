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
	}
}
