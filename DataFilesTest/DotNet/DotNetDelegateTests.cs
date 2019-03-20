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
	public class DotNetDelegateTests
	{
		public delegate EventHandler GlobalDelegate(int a, string b);

		public class BasicClass
		{
			public delegate EventHandler ClassDelegate(double a, float b);

			public class NestedClass
			{
				public delegate EventHandler NestedDelegate(bool a, byte b);
			}
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
		public void DotNetDelegate_ConvertFromType_Global()
		{
			//arrange
			XElement xmlElement = XElement.Parse("<member name='T:DataFileTest.DotNetDelegateTests.GlobalDelegate' />", LoadOptions.PreserveWhitespace);
			Type type = typeof(GlobalDelegate);
			//act
			DotNetType typeResult = DotNetType.FromVisualStudioXml(xmlElement);
			DotNetDelegate delegateResult = typeResult.ToDelegate(typeResult.Name);
			delegateResult.AddAssemblyInfo(type);
			//assert
			Assert.AreEqual(2, delegateResult.MethodName.Parameters.Count);
			Assert.AreEqual("a", delegateResult.MethodName.Parameters[0].Name);
			Assert.AreEqual("b", delegateResult.MethodName.Parameters[1].Name);
			Assert.AreEqual("System.Int32", delegateResult.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("System.String", delegateResult.MethodName.Parameters[1].FullTypeName);
			Assert.AreEqual("System.EventHandler", delegateResult.MethodName.ReturnTypeName.FullName);
		}
	}
}
