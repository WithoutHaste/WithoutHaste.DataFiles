# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetParameter

**Inheritance:** object  

Represents a parameter in a method signature.  

# Properties

## [ParameterCategory](WithoutHaste.DataFiles.DotNet.ParameterCategory.md) Category { get; protected set; }

## object DefaultValue { get; protected set; }

For optional parameters, the default value of the parameter. Null otherwise.  

## string FullTypeName { get; }

Fully qualified data type name.  

## string LocalTypeName { get; }

Local data type name.  

## string Name { get; protected set; }

Name of parameter. Null if not known.  

**Example A:**  
In `MethodName(int a, string b)`, the first parameter name is `a`.  

## string SignatureWithName { get; }

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

## string SignatureWithoutName { get; }

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

## virtual void AddAssemblyInfo([System.Reflection.ParameterInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.parameterinfo) parameterInfo)

Load additional documentation information from the assembly itself.  

## DotNetParameter Clone()

Returns deep clone of parameter.  

## virtual bool Equals(object b)

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

## virtual int GetHashCode()

## void SetIsExtension()

Set that this parameter is the first parameter in an extension method.  

## virtual string ToString()

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

## static DotNetParameter FromVisualStudioXml(string typeName)

Parses a .Net XML documentation parameter type name.  

# Operators

## bool = DotNetParameter a == DotNetParameter b

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

## bool = DotNetParameter a != DotNetParameter b

For equality, parameter type and category must be equal. Parameter name and default value are irrelevant.  

