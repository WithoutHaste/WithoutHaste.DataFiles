# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetMember

**Abstract**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents any .Net construct that can have comments on it:  
class, interface, struct, delegate, enum, method, field, property, event, etc.  

# Fields

## [List&lt;DotNetComment&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ExampleComments

Comments from "example" xml tags.  

## [List&lt;DotNetCommentQualifiedLinkedGroup&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ExceptionComments

Comments from "exception" xml tags.  Only expected as top-level tags.  

## [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) FloatingComments

Any comments not within expected top-level tags.  

## [List&lt;DotNetCommentParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) ParameterComments

Comments from "param" xml tags. Only expected as top-level tags.  

## [List&lt;DotNetCommentQualifiedLinkedGroup&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) PermissionComments

Comments from "permission" xml tags. Only expected as top-level tags.  

## [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) RemarksComments

Comments from "remarks" xml tags. Only expected as a top-level tag.  

**Remarks:**  
If there are multiple "remarks" tags, their contents will be concatenated as if they were one tag.  

## [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) ReturnsComments

Comments from "returns" xml tags. Only expected as a top-level tag.  

**Remarks:**  
If there are multiple "returns" tags, their contents will be concatenated as if they were one tag.  

## [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) SummaryComments

Comments from "summary" xml tags. Only expected as a top-level tag.  

**Remarks:**  
If there are multiple "summary" tags, their contents will be concatenated as if they were one tag.  

## [List&lt;DotNetCommentParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) TypeParameterComments

Comments from "typeparam" xml tags. Only expected as top-level tags.  

## [DotNetCommentGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentGroup.md) ValueComments

Comments from "value" xml tags. Only expected as a top-level tag.  

**Remarks:**  
If there are multiple "value" tags, their contents will be concatenated as if they were one tag.  

# Properties

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) HasComments { get; }

True when there&#96;s at least one comment on this member.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) HasNoComments { get; }

True when there are no comments on this member.  

## [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) Name { get; protected set; }

# Constructors

## DotNetMember([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

# Methods

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) ClearComments()

Removes all comments from member.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) CopyComments([DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) original)

Shallow-copies all comments from the _original_ member to this member.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Matches([DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md) link)

Returns true if member name matches the link name.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Matches([DotNetCommentQualifiedLinkedGroup](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLinkedGroup.md) group)

Returns true if member name matches the link name.  

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) ParseVisualStudioXmlDocumentation([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) parent)

Parse .Net XML documentation about this member.  

**Remarks:**  
Clears any existing comments before parsing the new ones.  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) parent**: Expects the tag containing all documentation for this member.  

## virtual [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) ResolveDuplicatedComments([Func&lt;DotNetQualifiedName,DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember, [List&lt;DotNetQualifiedName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) pathToThisDuplicate = null)

For all "duplicate" comments, replace the comment with the duplicated comments.  

**Returns:**  
Returns true if resolution is successful. Returns false if referenced member is not found, or if there is a reference loop.  

**Parameters:**  
* **[Func&lt;DotNetQualifiedName,DotNetMember&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) FindMember**: Function that returns the selected member from all known members in the assembly.  
* **[List&lt;DotNetQualifiedName&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) pathToThisDuplicate**: List of named types/members that are duplicating each other, leading to this member. Used to avoid reference loops.  

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToString()

Full name of member.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetField](WithoutHaste.DataFiles.DotNet.DotNetField.md)  
Represents a type&#96;s field.  

[WithoutHaste.DataFiles.DotNet.DotNetMethod](WithoutHaste.DataFiles.DotNet.DotNetMethod.md)  
Represents a method.  

[WithoutHaste.DataFiles.DotNet.DotNetType](WithoutHaste.DataFiles.DotNet.DotNetType.md)  
Represents a data type.  

