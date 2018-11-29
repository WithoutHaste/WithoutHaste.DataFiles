# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetIndexer

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md) → [DotNetProperty](WithoutHaste.DataFiles.DotNet.DotNetProperty.md)  

Represents an indexer property.  

# Fields

## [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Parameters

# Properties

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ParametersSignature { get; }

Returns indexer parameters format as "[TypeA a, TypeB b]".  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ParameterTypesSignature { get; }

Returns indexer parameters format as "[TypeA,TypeB]".  

# Constructors

## DotNetIndexer([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name, [List](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters)

# Methods

## virtual [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) propertyInfo)

Load additional documentation information from the assembly itself.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Matches([DotNetCommentQualifiedLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup.md) linkedGroup)

Returns true if this indexer's signature matches the link.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Matches([DotNetCommentMethodLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLinkedGroup.md) linkedGroup)

Returns true if this indexer's signature matches the link.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Matches([DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) methodLink)

Returns true if this indexer's signature matches the method signature.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Matches([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName)

Returns true if this indexer's signature matches the method signature.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesArguments([System.Reflection.ParameterInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.parameterinfo[]) otherParameters)

Returns true if this method's parameter list matches the reflected ParameterInfo.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesSignature([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

Returns true if this method's signature matches the reflected MethodInfo.  

**Parameters:**  
* **[System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo**: Expects a method with name "get_Item".  

# Static Methods

## static DotNetIndexer FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

Parse .Net XML documentation for Indexer data.  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

