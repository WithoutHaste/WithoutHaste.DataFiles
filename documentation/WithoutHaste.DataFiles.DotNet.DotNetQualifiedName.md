# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetQualifiedName

**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  
**Implements:** [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable)  

Represents a fully qualified type name or member name.  

# Fields

## protected [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName

Name without namespace or declaring type or generic type parameters.  

# Properties

## DotNetQualifiedName ExplicitInterface { get; protected set; }

The interface being implemented, if this is a property or method with an explicit interface implementation.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) FullName { get; }

Fully qualified name.  

## DotNetQualifiedName FullNamespace { get; protected set; }

Fully qualified namespace.  

**Remarks:**  
Null if there is no namespace.  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) LocalName { get; }

Local data type name, written in the c# style.  

**Example A:**  
``MyType<T> instead of MyType`1``  

## [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) LocalXmlName { get; }

Local data type name, written in the Xml style.  

**Example A:**  
``MyType`1 instead of MyType<T>``  

# Constructors

## DotNetQualifiedName()

Empty constructor  

## DotNetQualifiedName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

## DotNetQualifiedName([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) localName, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) fullNamespace, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) explicitInterface = null)

## DotNetQualifiedName([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) names)

Builds the qualified name from each segment provided, with the first string being the root namespace and the last string being the local name.  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: List of names cannot be empty.  

# Methods

## virtual DotNetQualifiedName Clone()

Returns deep clone of qualified name.  

## virtual [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) CompareTo([object](https://docs.microsoft.com/en-us/dotnet/api/system.object) b)

Names are sorted alphabetically, per namespace, starting with the root.  

**Remarks:**  
Explicit interface implementations are considered only as a last resort.  

## virtual [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) Equals([object](https://docs.microsoft.com/en-us/dotnet/api/system.object) b)

Names converted to strings must match exactly to be considered equal.  

## [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) Flatten()

Returns an array of the name segments that make up this qualified name.  

**Remarks:**  
Does not include explicit interface implementations.  

**Example A:**  
"System.Collections.Generic".Flatten() returns ["System", "Collections", "Generic"].  

## virtual [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) GetHashCode()

## DotNetQualifiedName GetLocalized([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

Returns a new name object which has been localized to the provided other name. The current object is not altered.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsWithin([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

Returns true if this Name is nested inside the other Name.  

**Example A:**  
"System.Text.RegularExpressions" is within "System.Text" and "System".  

**Example B:**  
"System" is not within null or empty Name.  

## virtual [void](https://docs.microsoft.com/en-us/dotnet/api/system.void) Localize([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) other)

Simplifies this qualified name based on the _other_ name.  
In other words, removes the portion of the namespace that this and the _other_ have in common.  
Alters the current object.  

**Remarks:**  
Will always keep at least the LocalName.  

Preserves explicit interface implementations.  

**Example A:**  
"System.Collections.Generic.List".Localize("System.Collections") returns "Generic.List".  

**Example B:**  
"System.Collections.Generic.List".Localize("System.Collections.Standard.List") returns "Standard.List".  

**Example C:**  
"System.Collections.Generic.List".Localize("System.Collections.Generic.List") returns "List".  

## [DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md) ToDotNetQualifiedTypeName()

Convert a base-type DotNetQualifiedName to a DotNetQualifiedTypeName.  

## virtual [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ToString()

Returns dot notation of namespaces and local name.  

**Example A:**  
A.B.C.LocalName  

# Static Methods

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Combine([String[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) names)

Return the names combined with a '.' delimiter.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) Combine([List&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) names)

Return the names combined with a '.' delimiter.  

## static [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32) Compare([DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) a, [DotNetQualifiedName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md) b)

Names are sorted alphabetically, per namespace, starting with the root.  

**Remarks:**  
Explicit interface implementations are considered only as a last resort.  

## static DotNetQualifiedName FromAssemblyInfo([System.Reflection.TypeInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo) typeInfo)

Parses a System.Reflection.AssemblyInfo full name.  

**Misc:**  
* The escape character is backslash (\)  
* Nested types are separated with '+' instead of '.'  
* Class declaration of generic types are shown the same as .Net XML documentation: MyType&#96;1 for one generic type  
* When a generic type is defined: System.Collections.Generic.List&#96;1[U], where U is the type alias from the class declaration  

## static DotNetQualifiedName FromAssemblyInfo([Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type)

See [FromAssemblyInfo(System.Reflection.TypeInfo)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  

## static DotNetQualifiedName FromAssemblyInfo([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) typeName)

See [FromAssemblyInfo(System.Reflection.TypeInfo)](WithoutHaste.DataFiles.DotNet.DotNetQualifiedName.md)  

## static DotNetQualifiedName FromVisualStudioXml([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name)

Parses a .Net XML documentation type, method, or other member name.  

### Parameters

#### [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) name

* Names starting with "T:" are parsed as Type names.  
* Names starting with "M:" are parsed as Method names.  
* Names starting with "F:" are parsed as Member names.  
* Names starting with "P:" are parsed as Member names.  
* Names starting with "E:" are parsed as Member names.  
* All others are parsed as Member names.  

# Operators

## implicit [string](https://docs.microsoft.com/en-us/dotnet/api/system.string)(DotNetQualifiedName name)

Returns dot notation of namespaces and local name.  

**Example A:**  
A.B.C.LocalName  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetQualifiedName a == DotNetQualifiedName b

Names converted to strings must match exactly to be considered equal.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetQualifiedName a != DotNetQualifiedName b

Names converted to strings must match exactly to be considered equal.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetQualifiedName a > DotNetQualifiedName b

Names are sorted alphabetically, per namespace, starting with the root.  

**Remarks:**  
Explicit interface implementations are considered only as a last resort.  

## [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) = DotNetQualifiedName a < DotNetQualifiedName b

Names are sorted alphabetically, per namespace, starting with the root.  

**Remarks:**  
Explicit interface implementations are considered only as a last resort.  

# Derived By

[WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md)  
Represents a fully qualified class name, for class declarations.  

[WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md)  
Represents a fully qualified method name.  

[WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedTypeName.md)  
Represents a fully qualified type name, for return types / field types / property types / parameter types.  

