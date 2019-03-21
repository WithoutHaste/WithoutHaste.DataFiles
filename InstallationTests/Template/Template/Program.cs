using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WithoutHaste.DataFiles.DotNet;
using WithoutHaste.DataFiles.Markdown;

namespace Template
{
    public class Program
    {
		public static void Main()
		{
			new Program();
		}

		public Program()
		{
			//Test DotNet namespace
			string outputFolder = Path.Combine(GetMainDirectory(), "DataFiles");
			outputFolder = Path.Combine(outputFolder, "bin");
			outputFolder = Path.Combine(outputFolder, "Release");
			string xmlFilename = Path.Combine(outputFolder, "WithoutHaste.DataFiles.XML");
			string dllFilename = Path.Combine(outputFolder, "WithoutHaste.DataFiles.net20.dll");
			DotNetDocumentationFile docFile = new DotNetDocumentationFile(xmlFilename);
			docFile.AddAssemblyInfo(dllFilename);

			//Test Markdown namespace
			MarkdownFile mdFile = new MarkdownFile();
			mdFile.AddSection("A Header");
			string mdText = mdFile.ToMarkdownString();
		}

		private string GetMainDirectory()
		{
			string dir = Environment.CurrentDirectory;
			while(!dir.EndsWith("WithoutHaste.DataFiles"))
			{
				dir = Directory.GetParent(dir).FullName;
			}
			return dir;
		}
	}
}
