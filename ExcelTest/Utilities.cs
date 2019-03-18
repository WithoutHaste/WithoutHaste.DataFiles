using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataFilesTest
{
	public static class Utilities
	{
		public static string LoadText(string fullPath)
		{
			using(StreamReader reader = new StreamReader(fullPath))
			{
				return reader.ReadToEnd().Replace("\r", "");
			}
		}

		public static string GetProjectDirectory()
		{
			string workingDirectory = Environment.CurrentDirectory; //should be in a bin folder, but may be different during tests
			while(!workingDirectory.EndsWith("ExcelTest"))
			{
				workingDirectory = Directory.GetParent(workingDirectory).FullName;
			}
			return workingDirectory;
		}

		public static string GetPathTo(string relativeToProjectDirectory)
		{
			return Path.Combine(Utilities.GetProjectDirectory(), relativeToProjectDirectory);
		}
	}
}
