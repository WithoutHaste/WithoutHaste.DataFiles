# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetMethod

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md)  

Represents a method.  

# Properties

## [WithoutHaste.DataFiles.DotNet.MethodCategory](WithoutHaste.DataFiles.DotNet.MethodCategory.md) Category { get; protected set; }

## [WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) MethodName { get; }

Strongly typed name.  

# Constructors

## DotNetMethod()

Empty constructor  

## DotNetMethod([WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name)

# Methods

## virtual [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

Load additional documentation information from the assembly itself.  

## virtual [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Equals([object](https://docs.microsoft.com/en-us/dotnet/api/system.object) b)

Equality is based on the full namespace/name/generic-type-parameters of the method, and on parameter-types.  

## virtual [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) GetHashCode()

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Is([WithoutHaste.DataFiles.DotNet.DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

Returns true if this member's name matches the provided name.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesArguments([System.Reflection.ParameterInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.parameterinfo[]) parameters)

Returns true if this method's parameter list matches the reflected ParameterInfo. Checks parameter types, not names.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesArguments([List&lt;WithoutHaste.DataFiles.DotNet.DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters)

Returns true if this method's parameter list matches the provided parameter list. Checks parameter types, not names.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesLocalSignature([WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name)

Returns true if this method's signature matches the other method signature.  
Looks at local name instead of entire namespace.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesSignature([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

Returns true if this method's signature matches the reflected MethodInfo.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesSignature([WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name)

Returns true if this method's signature matches the other method signature.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) MatchesSignature([WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) link)

Returns true if this method link and the method have matching signatures, based on the fully qualified name and the list of parameter types.  

# Static Methods

## static DotNetMethod FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

Parse .Net XML documentation for method signature data.  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag "member".  

# Operators

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetMethod a == DotNetMethod b

Equality is based on the full namespace/name/generic-type-parameters of the method, and on parameter-types.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetMethod a != DotNetMethod b

Equality is based on the full namespace/name/generic-type-parameters of the method, and on parameter-types.  

