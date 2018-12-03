# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).ArbitraryCounter

**Abstract**  
**Inheritance:** object  

Counts using an arbitrary set of digits.  

**Remarks:**  
Using "integers" as an analogy, Counter values cannot be negative.  

# Fields

## VALID_CHARACTERS

**protected readonly [Char[]](https://docs.microsoft.com/en-us/dotnet/api/system.array)**  

All valid characters, in order from smallest to largest.  

## value

**protected [List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Current internal value.  

# Properties

## MINIMUM_VALUE

**string { public get; }**  

Counter can never decrement below the minimum value.  

## Value

**string { public get; }**  

Current display value.  

# Constructors

## ArbitraryCounter([Char[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) validCharacters)

Initialize at MINIMUM_VALUE.  

# Methods

## CopyValue()

**protected [List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)**  

Returns an independent copy of the value.  

## Decrement([List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) value, int delta)

**protected void**  

Increment a value.  

## Increment([List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) value, int delta)

**protected void**  

Increment a value.  

## SetValue(string start)

**void**  

## SetValue([List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) start)

**protected void**  

# Derived By

[WithoutHaste.DataFiles.AlphabetCounter](WithoutHaste.DataFiles.AlphabetCounter.md)  
Counts "A", "B", "C", ..., "Z", "AA", "AB", ..., "AZ", "BA", "BB", ...  

