# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetDocumentationFile

**Inheritance:** object  

Represents a .Net XML documentation file, such as those produced by Visual Studio.  
Can add additional documentation derived from the assembly itself.  

# Fields

## Delegates

**[List&lt;DotNetDelegate&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Top-level delegates in assembly.  

## Extensions

**readonly [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array)**  

Accepted .Net XML documentation file extensions.  

## Types

**[List&lt;DotNetType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Top-level types in assembly.  

# Properties

## AssemblyName

**string { public get; protected set; }**  

## TypeCount

**int { public get; }**  

Returns the full count of types within assembly, including nested types and enums.  

# Constructors

## DotNetDocumentationFile()

Empty constructor.  

## DotNetDocumentationFile(string filename)

Loads .Net XML documentation from file.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Filename is null.  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Unexpected file extension of the *.XML documentation file.  

**Parameters:**  
* **string filename**: Full path, filename, and extension.  

## DotNetDocumentationFile([System.Xml.Linq.XDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument) document)

Loads .Net XML documentation from XDocument.  

# Methods

## AddAssemblyInfo(string assemblyFilename, [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) thirdPartyAssemblyFilenames)

**void**  

Load additional documentation information from the assembly itself.  

### Parameters

#### string assemblyFilename

Full path and filename of the *.dll library being documentated.  

#### [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) thirdPartyAssemblyFilenames

List of third-party libraries referenced by your library.  
These libraries will not be documented, but they must be loaded if you want to see the full type names for return types and parameter types from these libraries.  
Each item in the list should be the full path and filename of a library.  

**Example A:**  
To document the return type of `public Company.SomeType MyMethod() {}`, the library for `Company.SomeType` must be loaded.  

