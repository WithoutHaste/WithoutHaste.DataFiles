# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetBaseType

**Inheritance:** object  

Represents a node in an inheriance hierarchy.  
Stub class: contains minimal information about the type.  

# Properties

## BaseType

**DotNetBaseType { public get; protected set; }**  

## Depth

**int { public get; }**  

Returns the inheritance distance from here to the bottom.  

**Example A:**  
Class "System.Reflection.TypeInfo" has Depth = 4 because its inheritance path is "TypeInfo -&gt; Type -&gt; MemberInfo -&gt; Object".  

**Example B:**  
Class "System.Object" has Depth = 1 because its inheritance path is just "Object".  

## Name

**[DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) { public get; protected set; }**  

# Constructors

## DotNetBaseType([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

