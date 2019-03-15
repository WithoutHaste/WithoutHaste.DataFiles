using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetParameterTests
	{

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
		public void DotNetParameterName_Clone_NullType()
		{
			//arrange
			DotNetParameter a = new DotNetParameter();
			//act
			DotNetParameter result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
		}

		[TestMethod]
		public void DotNetParameterName_Clone_NullNamespaceType()
		{
			//arrange
			DotNetParameter a = DotNetParameter.FromVisualStudioXml("MyType");
			//act
			DotNetParameter result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
		}

		[TestMethod]
		public void DotNetParameter_Clone_ManySegmentsType()
		{
			//arrange
			DotNetParameter a = DotNetParameter.FromVisualStudioXml("A.B.C.MyType");
			//act
			DotNetParameter result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
		}

		[TestMethod]
		public void DotNetParameter_Clone_GenericType()
		{
			//arrange
			DotNetParameter a = DotNetParameter.FromVisualStudioXml("A.B.C.MyType{System.Int32}");
			//act
			DotNetParameter result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual("System.Int32", result.TypeName.GenericTypeParameters[0].ToString());
		}

		[TestMethod]
		public void DotNetParameter_Clone_NestedGenericType()
		{
			//arrange
			DotNetParameter a = DotNetParameter.FromVisualStudioXml("A.B.C{System.String}.MyType{System.Int32}");
			//act
			DotNetParameter result = a.Clone();
			//assert
			Assert.AreEqual(a, result);
			Assert.AreEqual("System.Int32", result.TypeName.GenericTypeParameters[0].ToString());
			Assert.AreEqual("System.String", result.TypeName.FullTypeNamespace.GenericTypeParameters[0].ToString());
		}
	}
}
