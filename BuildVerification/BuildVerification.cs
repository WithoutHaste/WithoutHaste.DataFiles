using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

namespace BuildVerification
{
	class BuildVerification
	{
		static void Main(string[] args)
		{
			string dllPath = args[0];
			dllPath = Path.Combine(Environment.CurrentDirectory, dllPath);
			Assembly dll = Assembly.LoadFrom(dllPath);
			string imageRuntimeVersion = dll.ImageRuntimeVersion;
			Console.WriteLine("DLL {0} => {1}", dllPath, imageRuntimeVersion);

			/*
			string dataFilesBinDirectory = GetDataFilesBinDirectory();
			string binDebug = Path.Combine(dataFilesBinDirectory, "Debug");
			string binRelease = Path.Combine(dataFilesBinDirectory, "Release");
			CheckFrameworks(binDebug);
			CheckFrameworks(binRelease);
			*/

//			Console.WriteLine("Done");
//			Console.ReadLine();
		}

		private static void CheckFrameworks(string dir)
		{
			foreach(string folder in Directory.GetDirectories(dir))
			{
				/*
				AppDomainSetup ads = new AppDomainSetup();
				ads.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
				ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
				AppDomain ad2 = AppDomain.CreateDomain("AD #2", null, ads);
				*/

				string dllPath = Path.Combine(folder, "WithoutHaste.DataFiles.dll");
				Assembly dll = Assembly.LoadFrom(dllPath);
				string imageRuntimeVersion = dll.ImageRuntimeVersion;
				Console.WriteLine("DLL {0} => {1}", folder, imageRuntimeVersion);

				/*
				AppDomain.Unload(ad2);
				*/

				/*
				TargetFrameworkAttribute attr = dll.GetCustomAttributes(true).OfType<TargetFrameworkAttribute>().FirstOrDefault();

				if(attr == null)
					Console.WriteLine("DLL {0} => NULL", folder);
				else
					Console.WriteLine("DLL {0} => {1}", folder, attr.FrameworkDisplayName);
				*/
			}
		}

		private static string GetDataFilesBinDirectory()
		{
			string dir = Environment.CurrentDirectory;
			while(!dir.EndsWith("WithoutHaste.DataFiles"))
			{
				dir = Directory.GetParent(dir).FullName;
			}
			dir = Path.Combine(dir, "DataFiles", "bin");
			return dir;
		}
	}
}
