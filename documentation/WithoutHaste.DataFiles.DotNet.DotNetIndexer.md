# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetIndexer

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md) → [DotNetProperty](WithoutHaste.DataFiles.DotNet.DotNetProperty.md)  

Represents an indexer property.  

# Fields

## [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Parameters

The list of indexer keys.  

**Example A:**  
Indexer `int this[string key]` has one parameter named "key".  

# Properties

## string ParametersSignature { get; }

Returns indexer parameters formatted as "[TypeA a, TypeB b]".  

## string ParameterTypesSignature { get; }

Returns indexer parameters formatted as "[TypeA,TypeB]".  

# Constructors

## DotNetIndexer([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name, [List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters)

# Methods

## virtual void AddAssemblyInfo([System.Reflection.PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) propertyInfo)

Load additional documentation information from the assembly itself.  

## bool Matches([DotNetCommentQualifiedLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup.md) linkedGroup)

Returns true if this indexer's signature matches the link.  

## bool Matches([DotNetCommentMethodLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLinkedGroup.md) linkedGroup)

Returns true if this indexer's signature matches the link.  

## bool Matches([DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) methodLink)

Returns true if this indexer's signature matches the method signature.  

## bool Matches([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) methodName)

Returns true if this indexer's signature matches the method signature.  

## bool MatchesArguments([System.Reflection.ParameterInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) otherParameters)

Returns true if this method's parameter list matches the reflected ParameterInfo.  

## bool MatchesSignature([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

Returns true if this method's signature matches the reflected MethodInfo.  

**Parameters:**  
* **[System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo**: Expects a method with name "get_Item".  

# Static Methods

## static DotNetIndexer FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

Parse .Net XML documentation for Indexer data.  

**Example A:**  
`<member name="P:Namespace.Type.Item(System.Int32)"></member>`  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag name "member".  

