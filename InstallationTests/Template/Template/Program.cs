using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
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
			outputFolder = Path.Combine(outputFolder, Properties.Settings.Default.NetVersion);
			string xmlFilename = Path.Combine(outputFolder, "WithoutHaste.DataFiles.XML");
			string dllFilename = Path.Combine(outputFolder, "WithoutHaste.DataFiles.dll");
			DotNetDocumentationFile docFile = new DotNetDocumentationFile(xmlFilename);
			docFile.AddAssemblyInfo(dllFilename);
			Console.WriteLine("Found {0} types in assembly", docFile.Types.Count);

			//Test Markdown namespace
			MarkdownFile mdFile = new MarkdownFile();
			mdFile.AddSection("A Header");
			string mdText = mdFile.ToMarkdownString();
			Console.WriteLine("Some markdown: {0}", mdText);

			Console.WriteLine("Done");
			Console.ReadLine();
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
