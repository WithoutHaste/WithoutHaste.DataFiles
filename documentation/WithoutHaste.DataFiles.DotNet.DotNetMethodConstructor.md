# [WithoutHaste.DataFiles.DotNet](TableOfContents.WithoutHaste.DataFiles.DotNet.md).DotNetMethodConstructor

**Inheritance:** object → [DotNetMember](WithoutHaste.DataFiles.DotNet.DotNetMember.md) → [DotNetMethod](WithoutHaste.DataFiles.DotNet.DotNetMethod.md)  

Represents a method that is a constructor.  

# Constructors

## DotNetMethodConstructor([DotNetQualifiedMethodName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedMethodName.md) name, bool isStatic = False)

# Methods

## AddAssemblyInfo([System.Reflection.ConstructorInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.constructorinfo) constructorInfo)

**void**  

Load additional documentation information from the assembly itself.  

## SetClassName([DotNetQualifiedClassName](WithoutHaste.DataFiles.DotNet.DotNetQualifiedClassName.md))

Constructors need to reference the actual name of their type so they display the right name with aliases.  

