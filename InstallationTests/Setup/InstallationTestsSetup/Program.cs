﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace InstallationTestsSetup
{
	class Program
	{
		static void Main(string[] args)
		{
			//delete any old auto-generated installation tests
			string dataFilesFolder = Path.Combine(GetInstallationTestsDirectory(), "..", "DataFiles");
			string templateFolder = Path.Combine(GetInstallationTestsDirectory(), "Template");
			string autoGeneratedFolder = Path.Combine(GetInstallationTestsDirectory(), "AutoGenerated");
			foreach(string oldFolder in Directory.EnumerateDirectories(autoGeneratedFolder))
			{
				Directory.Delete(oldFolder, recursive: true);
			}

			Dictionary<string, string> frameworks = new Dictionary<string, string>() {
				{ "v2.0",   "net20" },
				{ "v3.0",   "net30" },
				{ "v3.5",   "net35" },
				{ "v4.0",   "net40" },
				{ "v4.5",   "net45" },
				{ "v4.5.1", "net451" },
				{ "v4.5.2", "net452" },
				{ "v4.6",   "net46" },
				{ "v4.6.1", "net461" },
				{ "v4.6.2", "net462" },
				{ "v4.7",   "net47" },
				{ "v4.7.1", "net471" },
				{ "v4.7.2", "net472" },
			};

			foreach(KeyValuePair<string, string> pair in frameworks)
			{
				string version = pair.Key;
				string net = pair.Value;

				//create a folder
				string destination = Path.Combine(autoGeneratedFolder, net);
				Directory.CreateDirectory(destination);

				//copy Template project without any NuGet references
				//do not copy any NuGet information, so those files can be installed in the test
				File.Copy(Path.Combine(templateFolder, "Template.sln"), Path.Combine(destination, "Template.sln"));
				File.Copy(Path.Combine(dataFilesFolder, "nuget.exe"), Path.Combine(destination, "nuget.exe"));

				destination = Path.Combine(destination, "Template");
				Directory.CreateDirectory(destination);
				File.Copy(Path.Combine(templateFolder, "Template", "Template.csproj"), Path.Combine(destination, "Template.csproj"));
				File.Copy(Path.Combine(templateFolder, "Template", "Program.cs"), Path.Combine(destination, "Program.cs"));
				File.Copy(Path.Combine(templateFolder, "Template", "app.config"), Path.Combine(destination, "app.config"));
				destination = Path.Combine(destination, "Properties");
				Directory.CreateDirectory(destination);
				File.Copy(Path.Combine(templateFolder, "Template", "Properties", "AssemblyInfo.cs"), Path.Combine(destination, "AssemblyInfo.cs"));
				File.Copy(Path.Combine(templateFolder, "Template", "Properties", "Settings.Designer.cs"), Path.Combine(destination, "Settings.Designer.cs"));
				File.Copy(Path.Combine(templateFolder, "Template", "Properties", "Settings.settings"), Path.Combine(destination, "Settings.settings"));

				//update project to current target framework
				string csprojPath = Path.Combine(autoGeneratedFolder, net, "Template", "Template.csproj");
				XmlDocument doc = new XmlDocument();
				doc.Load(csprojPath);
				//replace target framework
				XmlNode frameworkNode = doc.GetElementsByTagName("TargetFrameworkVersion")[0];
				frameworkNode.InnerText = version;
				//remove NuGet references
				List<XmlNode> referenceNodes = new List<XmlNode>(doc.GetElementsByTagName("Reference").Cast<XmlNode>());
				foreach(XmlNode referenceNode in referenceNodes)
				{
					string include = (referenceNode as XmlElement).GetAttribute("Include");
					if(include.StartsWith("LINQlone") || include.StartsWith("WithoutHaste.DataFiles"))
					{
						referenceNode.ParentNode.RemoveChild(referenceNode);
					}
					if(include.StartsWith("System.Xml.Linq")) //remove hint path attribute
					{
						foreach(XmlNode referenceChildNode in referenceNode.ChildNodes)
						{
							referenceNode.RemoveChild(referenceChildNode);
						}
					}
				}
				//save changes
				doc.Save(csprojPath);

				//config file too
				string configPath = Path.Combine(autoGeneratedFolder, net, "Template", "app.config");
				doc = new XmlDocument();
				doc.Load(configPath);
				XmlNode valueNode = doc.GetElementsByTagName("value")[0];
				valueNode.InnerText = net;
				doc.Save(configPath);
			}
		}

		private static string GetInstallationTestsDirectory()
		{
			string dir = Environment.CurrentDirectory;
			while(!dir.EndsWith("InstallationTests"))
			{
				dir = Directory.GetParent(dir).FullName;
			}
			return dir;
		}
	}
}
