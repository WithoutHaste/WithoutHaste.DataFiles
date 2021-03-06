﻿using System;
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

			private void ClearCompilerWarnings()
			{
				EventA.Invoke(this, new EventArgs());
			}
		}

		[TestMethod]
		public void DotNetEvent_Assembly_Normal()
		{
			//arrange
			Type type = typeof(A);
			DotNetType dotNetType = DotNetType.FromVisualStudioXml(XElement.Parse("<member name='T:A'></member>", LoadOptions.PreserveWhitespace));
			dotNetType.AddMember(DotNetEvent.FromVisualStudioXml(XElement.Parse("<member name='E:A.EventA'></member>", LoadOptions.PreserveWhitespace)));
			//act
			dotNetType.AddAssemblyInfo(type, dotNetType.Name);
			//assert
			Assert.AreEqual(1, dotNetType.Events.Count);
			Assert.AreEqual("System.EventHandler", dotNetType.Events[0].FullTypeName);
		}
	}
}
