# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetSettings

**Static**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Global settings for the entire DotNet namespace.  

# Fields

## static [Func&lt;string,int,string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-3) AdditionalQualifiedNameConverter

A second level [QualifiedNameConverter](WithoutHaste.DataFiles.DotNet.DotNetSettings.md) to provide further processing.  
This method will be run after [QualifiedNameConverter](WithoutHaste.DataFiles.DotNet.DotNetSettings.md) for each [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md).  
  
Set to null to not use any converter.  

**Example A:**  

```
DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
DotNetSettings.AdditionalQualifiedNameConverter = myCustomConverter;
string displayString = myQualifiedTypeName.FullName;
```  

## static [Func&lt;string,int,string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-3) QualifiedNameConverter

When DotNetQualifiedNames are converted to strings, this converter will be automatically applied to each:  
* generic type parameter  
* method parameter  
* type name  
  
Set to null to not use any converter.  

**Remarks:**  
See [DefaultQualifiedNameConverter(string, int)](WithoutHaste.DataFiles.DotNet.DotNetSettings.md) for usage examples.  

**Example A:**  

```
DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
string displayString = myQualifiedTypeName.FullName;
```  

# Static Methods

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) DefaultQualifiedNameConverter([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) fullName, [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) depth)

Converts all standard .Net types to their common aliases.  

**Example A:**  

```
DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
string displayString = myQualifiedTypeName.FullName;
```  

**Example B:**  

```xml
"System.Int32" => "int"
"System.Collections.Generic.List<System.Int32> => "System.Collections.Generic.List<int>"
"MyType.MyMethod(System.Int32)" => "MyType.MyMethod(int)"
```  

**Parameters:**  
* **[string](https://docs.microsoft.com/en-us/dotnet/api/system.string) fullName**: When processing name "System.Collections.Generic.List", fullName will be "System" then "System.Collections" then "System.Collections.Generic" then "System.Collections.Generic.List".  
* **[int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) depth**: When processing name "System.Collections.Generic.List", depth will be 3 at "System", then 2 at "Collections", then 1 at "Generic", then 0 at "List".  

