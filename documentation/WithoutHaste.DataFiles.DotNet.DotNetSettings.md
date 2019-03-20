# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetSettings

**Static**  
**Inheritance:** object  

Global settings for the entire DotNet namespace.  

# Fields

## AdditionalQualifiedNameConverter

**static [Func&lt;string,int,string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)**  

A second level [QualifiedNameConverter](WithoutHaste.DataFiles.DotNet.DotNetSettings.md) to provide further processing.  
This method will be run after [QualifiedNameConverter](WithoutHaste.DataFiles.DotNet.DotNetSettings.md) for each [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md).  
  
Set to null to not use any converter.  

**Remarks:**  
Setting always defaults to null.  
With target frameworks 3.5 or higher, you can change this setting.  

**Example A:**  

```
DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
DotNetSettings.AdditionalQualifiedNameConverter = myCustomConverter;
string displayString = myQualifiedTypeName.FullName;
```  

## QualifiedNameConverter

**static [Func&lt;string,int,string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)**  

When DotNetQualifiedNames are converted to strings, this converter will be automatically applied to each:  
* generic type parameter  
* method parameter  
* type name  
  
Set to null to not use any converter.  

**Remarks:**  
Setting always defaults to [DefaultQualifiedNameConverter(string, int)](WithoutHaste.DataFiles.DotNet.DotNetSettings.md).  
With target frameworks 3.5 or higher, you can change this setting.  
  
See [DefaultQualifiedNameConverter(string, int)](WithoutHaste.DataFiles.DotNet.DotNetSettings.md) for usage examples.  

**Example A:**  

```
DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
string displayString = myQualifiedTypeName.FullName;
```  

# Static Methods

## DefaultQualifiedNameConverter(string fullName, int depth)

**static string**  

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
* **string fullName**: When processing name "System.Collections.Generic.List", fullName will be "System" then "System.Collections" then "System.Collections.Generic" then "System.Collections.Generic.List".  
* **int depth**: When processing name "System.Collections.Generic.List", depth will be 3 at "System", then 2 at "Collections", then 1 at "Generic", then 0 at "List".  

