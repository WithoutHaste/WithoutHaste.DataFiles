# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).ArbitraryCounter

**Abstract**  
**Inheritance:** object  

Counts using an arbitrary set of digits.  

**Remarks:**  
Using "integers" as an analogy, Counter values cannot be negative.  

# Fields

## protected readonly [Char[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) VALID_CHARACTERS

All valid characters, in order from smallest to largest.  

## protected [List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) value

Current internal value.  

# Properties

## string MINIMUM_VALUE { get; }

Counter can never decrement below the minimum value.  

## string Value { get; }

Current display value.  

# Constructors

## ArbitraryCounter([Char[]](https://docs.microsoft.com/en-us/dotnet/api/system.array) validCharacters)

Initialize at MINIMUM_VALUE.  

# Methods

## protected [List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) CopyValue()

Returns an independent copy of the value.  

## protected void Decrement([List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) value, int delta)

Increment a value.  

## protected void Increment([List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) value, int delta)

Increment a value.  

## void SetValue(string start)

## protected void SetValue([List&lt;char&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1) start)

# Derived By

[WithoutHaste.DataFiles.AlphabetCounter](WithoutHaste.DataFiles.AlphabetCounter.md)  
Counts "A", "B", "C", ..., "Z", "AA", "AB", ..., "AZ", "BA", "BB", ...  

