# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetDocumentationFile

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents a .Net XML documentation file, such as those produced by Visual Studio.  

# Fields

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetDelegate&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Delegates

Top-level delegates in assembly.  

## readonly [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string[]) Extensions

Accepted .Net XML documentation file extensions.  

## [List&lt;WithoutHaste.DataFiles.DotNet.DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Types

Top-level types in assembly.  

# Properties

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) AssemblyName { get; protected set; }

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) TypeCount { get; }

Returns the full count of types within assembly, including nested types and enums.  

# Constructors

## DotNetDocumentationFile()

Empty constructor.  

## DotNetDocumentationFile([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) filename)

Loads .Net XML documentation from file.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Filename is null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Unexpected file extension.  

**Parameters:**  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) filename**: Full path, filename, and extension.  

## DotNetDocumentationFile([System.Xml.Linq.XDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument) document)

Loads .Net XML documentation from XDocument.  

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) assemblyFilename)

Load additional documentation information from the assembly itself.  

