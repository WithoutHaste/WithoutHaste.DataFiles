# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetParameter

**Inheritance:** object  

Represents a parameter in a method signature.  

# Properties

## Category

**[ParameterCategory](WithoutHaste.DataFiles.DotNet.ParameterCategory.md) { public get; protected set; }**  

## DefaultValue

**object { public get; protected set; }**  

For optional parameters, the default value of the parameter. Null otherwise.  

## FullTypeName

**string { public get; }**  

Fully qualified data type name.  

## LocalTypeName

**string { public get; }**  

Local data type name.  

## Name

**string { public get; protected set; }**  

Name of parameter. Null if not known.  

**Example A:**  
In `MethodName(int a, string b)`, the first parameter name is `a`.  

## SignatureWithName

**string { public get; }**  

Returns formatted parameter with name.  

**Example A:**  
MyType myName  

**Example B:**  
out MyType myName  

**Example C:**  
ref MyType myName  

**Example D:**  
this MyType myName  

**Example E:**  
MyType myName = defaultValue  

## SignatureWithoutName

**string { public get; }**  

Returns formatted parameter without the name.  

**Example A:**  
MyType  

**Example B:**  
out MyType  

**Example C:**  
ref MyType  

**Example D:**  
this MyType  

**Example E:**  
MyType = defaultValue  

## TypeName

**[DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) { public get; protected set; }**  

Fully qualified data type name object.  

# Constructors

## DotNetParameter()

Empty constructor.  

**Remarks:**  
Category defaults to Unknown.  

## DotNetParameter([DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) typeName)

**Remarks:**  
Category defaults to Normal.  

**Parameters:**  
* **[DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) typeName**: Fully qualified data type name.  

## DotNetParameter([DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) typeName, [ParameterCategory](WithoutHaste.DataFiles.DotNet.ParameterCategory.md) category)

**Parameters:**  
* **[DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) typeName**: Fully qualified data type name.  
* **[ParameterCategory](WithoutHaste.DataFiles.DotNet.ParameterCategory.md) category**: Category of parameter.  

# Methods

## AddAssemblyInfo([System.Reflection.ParameterInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.parameterinfo) parameterInfo)

**virtual void**  

Load additional documentation information from the assembly itself.  

## Clone()

**DotNetParameter**  

Returns deep clone of parameter.  

## Equals(object b)

**virtual bool**  

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

## GetHashCode()

**virtual int**  

## MatchesSignature([DotNetParameter](WithoutHaste.DataFiles.DotNet.DotNetParameter.md))

Returns true if signatures match. Looks at types only, not at names.  

## MatchesSignature([System.Reflection.ParameterInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.parameterinfo))

Returns true if signatures match. Looks at types only, not at names.  

## PushClassGenericTypes([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array))

Update method/indexer parameters with the class's generic-type aliases.  

**Parameters:**  

## PushMethodGenericTypes([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array))

Update method parameters with the method's generic-type aliases.  

**Parameters:**  

## SetIsExtension()

**void**  

Set that this parameter is the first parameter in an extension method.  

## ToString()

**virtual string**  

Returns formatted parameter with name.  

**Example A:**  
MyType myName  

**Example B:**  
out MyType myName  

**Example C:**  
ref MyType myName  

**Example D:**  
this MyType myName  

**Example E:**  
MyType myName = defaultValue  

# Static Methods

## FromVisualStudioXml(string typeName)

**static DotNetParameter**  

Parses a .Net XML documentation parameter type name.  

# Operators

## bool = DotNetParameter a == DotNetParameter b

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

## bool = DotNetParameter a != DotNetParameter b

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

