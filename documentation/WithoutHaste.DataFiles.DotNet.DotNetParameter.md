# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetParameter

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Represents a parameter in a method signature.  

# Properties

## [ParameterCategory](WithoutHaste.DataFiles.DotNet.ParameterCategory.md) Category { get; protected set; }

## [object](https://docs.microsoft.com/en-us/dotnet/api/system.object) DefaultValue { get; protected set; }

For optional parameters, the default value of the parameter. Null otherwise.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) FullTypeName { get; }

Fully qualified data type name.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) LocalTypeName { get; }

Local data type name.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Name { get; protected set; }

Name of parameter. Null if not known.  

**Example A:**  
In `MethodName(int a, string b)`, the first parameter name is `a`.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) SignatureWithName { get; }

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

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) SignatureWithoutName { get; }

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

## [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) TypeName { get; protected set; }

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

## virtual [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) AddAssemblyInfo([System.Reflection.ParameterInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.parameterinfo) parameterInfo)

Load additional documentation information from the assembly itself.  

## DotNetParameter Clone()

Returns deep clone of parameter.  

## virtual [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Equals([object](https://docs.microsoft.com/en-us/dotnet/api/system.object) b)

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

## virtual [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) GetHashCode()

## [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) SetIsExtension()

Set that this parameter is the first parameter in an extension method.  

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToString()

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

## static DotNetParameter FromVisualStudioXml([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) typeName)

Parses a .Net XML documentation parameter type name.  

# Operators

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetParameter a == DotNetParameter b

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetParameter a != DotNetParameter b

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

