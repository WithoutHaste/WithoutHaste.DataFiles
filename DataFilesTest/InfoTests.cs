using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles;

namespace DataFilesTest
{
	[TestClass]
	public class InfoTests
	{
		[TestMethod]
		public void Info_GetVersion()
		{
			//arrange
			//act
			Version version = Info.GetVersion();
			//assert
			ValidateVersion(version, 1, 0, 0);
		}

		[TestMethod]
		public void Info_GetDependencyVersions()
		{
			//arrange
			//act
			Dictionary<string, Version> versions = Info.GetDependencyVersions();
			//assert
			ValidateVersion(versions["EPPlus"], 4, 5, 2, 1);
		}

		private void ValidateVersion(Version version, int major, int minor, int revision)
		{
			Assert.AreEqual(major, version.Major);
			Assert.AreEqual(minor, version.Minor);
			Assert.AreEqual(revision, version.Revision);
		}

		private void ValidateVersion(Version version, int major, int minor, int build, int revision)
		{
			Assert.AreEqual(major, version.Major);
			Assert.AreEqual(minor, version.Minor);
			Assert.AreEqual(build, version.Build);
			Assert.AreEqual(revision, version.Revision);
		}
	}
}