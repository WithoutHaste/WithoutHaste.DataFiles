# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedClassNameTreeNode

**Inheritance:** object  

A node in a tree data structure made up of DotNetQualifiedClassNames organized by their namespaces.  

# Properties

## [List&lt;DotNetQualifiedClassNameTreeNode&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) Children { get; protected set; }

## DotNetQualifiedClassNameTreeNode Parent { get; protected set; }

## [DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) Value { get; protected set; }

# Methods

## void Insert([DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md) name)

Insert a new namespace into its proper position, based on the current node as the root.  

# Static Methods

## static DotNetQualifiedClassNameTreeNode Generate([List&lt;DotNetQualifiedClassName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) names)

Organize a list of namespaces into a tree, based on which namespaces are within other namespaces.  

**Returns:**  
Returns the root of the new tree.  
If there is one top-level namespace, root.Value will be that namespace.  
If there are more than one top-level namespaces, root.Value will be null and root.Children will contain the top-level namespaces.  

