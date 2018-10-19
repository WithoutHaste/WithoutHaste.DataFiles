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
	public class DotNetEventTests
	{
		protected class A
		{
			public event EventHandler EventA;
		}

		[TestMethod]
		public void DotNetEvent_Assembly_Normal()
		{
			//arrange
			Type type = typeof(A);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:A'></member>"));
			dotNetType.AddMember(DotNetEvent.FromVisualStudioXml(XElement.Parse("<member name='E:A.EventA'></member>")));
			//act
			dotNetType.AddAssemblyInfo(type.GetTypeInfo(), dotNetType.Name);
			//assert
			Assert.AreEqual(1, dotNetType.Events.Count);
			Assert.AreEqual("System.EventHandler", dotNetType.Events[0].FullTypeName);
		}
	}
}
