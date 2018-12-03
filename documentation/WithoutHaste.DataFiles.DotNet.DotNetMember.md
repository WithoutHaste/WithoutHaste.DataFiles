# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetMember

**Abstract**  
**Inheritance:** object  

Represents any .Net construct that can have comments on it:  
class, interface, struct, delegate, enum, method, field, property, event, etc.  

# Fields

## ExampleComments

**[List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Comments from "example" xml tags.  

## ExceptionComments

**[List&lt;DotNetCommentQualifiedLinkedGroup&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Comments from "exception" xml tags.  Only expected as top-level tags.  

## FloatingComments

**[DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md)**  

Any comments not within expected top-level tags.  

## ParameterComments

**[List&lt;DotNetCommentParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Comments from "param" xml tags. Only expected as top-level tags.  

## PermissionComments

**[List&lt;DotNetCommentQualifiedLinkedGroup&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Comments from "permission" xml tags. Only expected as top-level tags.  

## RemarksComments

**[DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md)**  

Comments from "remarks" xml tags. Only expected as a top-level tag.  

**Remarks:**  
If there are multiple "remarks" tags, their contents will be concatenated as if they were one tag.  

## ReturnsComments

**[DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md)**  

Comments from "returns" xml tags. Only expected as a top-level tag.  

**Remarks:**  
If there are multiple "returns" tags, their contents will be concatenated as if they were one tag.  

## SummaryComments

**[DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md)**  

Comments from "summary" xml tags. Only expected as a top-level tag.  

**Remarks:**  
If there are multiple "summary" tags, their contents will be concatenated as if they were one tag.  

## TypeParameterComments

**[List&lt;DotNetCommentParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Comments from "typeparam" xml tags. Only expected as top-level tags.  

## ValueComments

**[DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md)**  

Comments from "value" xml tags. Only expected as a top-level tag.  

**Remarks:**  
If there are multiple "value" tags, their contents will be concatenated as if they were one tag.  

# Properties

## HasComments

**bool { public get; }**  

True when there's at least one comment on this member.  

## HasNoComments

**bool { public get; }**  

True when there are no comments on this member.  

## Name

**[DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) { public get; protected set; }**  

# Constructors

## DotNetMember([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

# Methods

## ClearComments()

**void**  

Removes all comments from member.  

## CopyComments([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) original)

**void**  

Shallow-copies all comments from the _original_ member to this member.  

## Matches([DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md) link)

**bool**  

Returns true if member name matches the link name.  

## Matches([DotNetCommentQualifiedLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup.md) group)

**bool**  

Returns true if member name matches the link name.  

## ParseVisualStudioXmlDocumentation([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) parent)

**void**  

Parse .Net XML documentation about this member.  

**Remarks:**  
Clears any existing comments before parsing the new ones.  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) parent**: Expects the tag containing all documentation for this member.  

## ResolveDuplicatedComments([Func&lt;DotNetQualifiedName,DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember, [List&lt;DotNetQualifiedName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) pathToThisDuplicate = null)

**virtual bool**  

For all "duplicate" comments, replace the comment with the duplicated comments.  

**Returns:**  
Returns true if resolution is successful. Returns false if referenced member is not found, or if there is a reference loop.  

**Parameters:**  
* **[Func&lt;DotNetQualifiedName,DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember**: Function that returns the selected member from all known members in the assembly.  
* **[List&lt;DotNetQualifiedName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) pathToThisDuplicate**: List of named types/members that are duplicating each other, leading to this member. Used to avoid reference loops.  

## ToString()

**virtual string**  

Full name of member.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md)  
Represents a type's field.  

[WithoutHaste.DataFiles.DotNet.DotNetMethod](WithoutHaste.DataFiles.DotNet.DotNetMethod.md)  
Represents a method.  

[WithoutHaste.DataFiles.DotNet.DotNetType](WithoutHaste.DataFiles.DotNet.DotNetType.md)  
Represents a data type: a class, interface, struct, or enum.  

