[Home](README.md)

# Getting Started: WithoutHaste.DataFiles.DotNet

## Exporting Comments

To export Xml comments from your project:  
1. Open project properties
2. Go to the Build tab
3. Scroll to the bottom and check "XML documentation file"
4. Build the project

## Using This Library

```C#
using WithoutHaste.DataFiles.DotNet;

public class Sample
{
	public Sample(string pathToXmlComments, string pathToDll)
	{
		DotNetDocumentationFile doc = new DotNetDocumentationFile(pathToXmlComments);
		doc.AddAssemblyInfo(pathToDll);
		foreach(DotNetType t in doc.Types)
		{
			Console.WriteLine("Type {0}", t.ClassName);
		}
	}
}
```