# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetIndexer

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md) → [DotNetProperty](WithoutHaste.DataFiles.DotNet.DotNetProperty.md)  

Represents an indexer property.  

# Fields

## Parameters

**[List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

The list of indexer keys.  

**Example A:**  
Indexer `int this[string key]` has one parameter named "key".  

# Properties

## ParametersSignature

**string { public get; }**  

Returns indexer parameters formatted as "[TypeA a, TypeB b]".  

## ParameterTypesSignature

**string { public get; }**  

Returns indexer parameters formatted as "[TypeA,TypeB]".  

# Constructors

## DotNetIndexer([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name, [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters)

# Methods

## AddAssemblyInfo([System.Reflection.PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) propertyInfo)

**virtual void**  

Load additional documentation information from the assembly itself.  

## Matches([DotNetCommentQualifiedLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup.md) linkedGroup)

**bool**  

Returns true if this indexer's signature matches the link.  

## Matches([DotNetCommentMethodLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLinkedGroup.md) linkedGroup)

**bool**  

Returns true if this indexer's signature matches the link.  

## Matches([DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) methodLink)

**bool**  

Returns true if this indexer's signature matches the method signature.  

## Matches([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName)

**bool**  

Returns true if this indexer's signature matches the method signature.  

## MatchesArguments([System.Reflection.ParameterInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) otherParameters)

**bool**  

Returns true if this method's parameter list matches the reflected ParameterInfo.  

## MatchesSignature([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

**bool**  

Returns true if this method's signature matches the reflected MethodInfo.  

**Parameters:**  
* **[System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo**: Expects a method with name "get_Item".  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

**static DotNetIndexer**  

Parse .Net XML documentation for Indexer data.  

**Example A:**  
`<member name="P:Namespace.Type.Item(System.Int32)"></member>`  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

