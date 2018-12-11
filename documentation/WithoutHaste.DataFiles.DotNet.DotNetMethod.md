# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetMethod

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md)  

Represents a method.  

# Properties

## Category

**[MethodCategory](WithoutHaste.DataFiles.DotNet.MethodCategory.md) { public get; protected set; }**  

## MethodName

**[DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) { public get; }**  

Strongly-typed name.  

# Constructors

## DotNetMethod()

Empty constructor  

## DotNetMethod([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name)

# Methods

## AddAssemblyInfo([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

**virtual void**  

Load additional documentation information from the assembly itself.  

## Equals(object b)

**virtual bool**  

Equality is based on the full namespace/name/generic-type-parameters of the method, and on parameter-types.  

## GetHashCode()

**virtual int**  

## Is([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) name)

**bool**  

Returns true if this member's name matches the provided name.  

## MatchesArguments([System.Reflection.ParameterInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) parameters)

**bool**  

Returns true if this method's parameter list matches the reflected ParameterInfo. Checks parameter types, not names.  

## MatchesArguments([List&lt;DotNetParameter&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) parameters)

**bool**  

Returns true if this method's parameter list matches the provided parameter list. Checks parameter types, not names.  

## MatchesLocalSignature([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name)

**bool**  

Returns true if this method's signature matches the other method signature.  
Looks at local name instead of entire namespace.  

## MatchesSignature([System.Reflection.MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodinfo) methodInfo)

**bool**  

Returns true if this method's signature matches the reflected MethodInfo.  

## MatchesSignature([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name)

**bool**  

Returns true if this method's signature matches the other method signature.  

## MatchesSignature([DotNetCommentMethodLink](WithoutHaste.DataFiles.DotNet.DotNetCommentMethodLink.md) link)

**bool**  

Returns true if this method link and the method have matching signatures, based on the fully qualified name and the list of parameter types.  

# Static Methods

## FromVisualStudioXml([System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement)

**static DotNetMethod**  

Parse .Net XML documentation for method signature data.  

**Example A:**  
`<member name="M:Namespace.Type.MethodName(System.Int32,System.String)"></member>`  

**Parameters:**  
* **[System.Xml.Linq.XElement](https://docs.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement) memberElement**: Expects tag "member".  

# Operators

## bool = DotNetMethod a == DotNetMethod b

Equality is based on the full namespace/name/generic-type-parameters of the method, and on parameter-types.  

## bool = DotNetMethod a != DotNetMethod b

Equality is based on the full namespace/name/generic-type-parameters of the method, and on parameter-types.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetDelegate](WithoutHaste.DataFiles.DotNet.DotNetDelegate.md)  
Represents a delegate type, categorized as a method.  

[WithoutHaste.DataFiles.DotNet.DotNetMethodConstructor](WithoutHaste.DataFiles.DotNet.DotNetMethodConstructor.md)  
Represents a method that is a constructor.  

[WithoutHaste.DataFiles.DotNet.DotNetMethodDestructor](WithoutHaste.DataFiles.DotNet.DotNetMethodDestructor.md)  
Represents a method that is a destructor.  

[WithoutHaste.DataFiles.DotNet.DotNetMethodOperator](WithoutHaste.DataFiles.DotNet.DotNetMethodOperator.md)  
Represents a method that is an operator.  

