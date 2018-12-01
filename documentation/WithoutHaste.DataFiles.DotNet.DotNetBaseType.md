# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetBaseType

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents a node in an inheriance hierarchy.  
Stub class: contains minimal information about the type.  

# Properties

## DotNetBaseType BaseType { get; protected set; }

## [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) Depth { get; }

Returns the inheritance distance from here to the bottom.  

**Example A:**  
Class "System.Reflection.TypeInfo" has Depth = 4 because its inheritance path is "TypeInfo -&gt; Type -&gt; MemberInfo -&gt; Object".  

**Example B:**  
Class "System.Object" has Depth = 1 because its inheritance path is just "Object".  

## [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) Name { get; protected set; }

# Constructors

## DotNetBaseType([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

