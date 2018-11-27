# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).IDotNetCommentLink

**Interface**  

Represents anything in the comments that links to something in the assembly.  

# Implemented By

[WithoutHaste.DataFiles.DotNet.DotNetCommentDuplicate](WithoutHaste.DataFiles.DotNet.DotNetCommentDuplicate.md)  
Represents a the `<duplicate cref="" />` tag, which means that documentation should be copied from the specified (cref) class, interface, struct, or member.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md)  
Represents a link in the comments to an internal or extenal method.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentParameterLink.md)  
Represents a link in the comments to an internal parameter name.  

[WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink](WithoutHaste.DataFiles.DotNet.DotNetCommentQualifiedLink.md)  
Represents a link in the comments to an internal or extenal type or type.method().  

[WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink](WithoutHaste.DataFiles.DotNet.DotNetCommentTypeParameterLink.md)  
Represents a link in the comments to an internal generic-type parameter.  

# Properties

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) FullName { get; }

Return the fully qualified name of the referenced assembly element.  

