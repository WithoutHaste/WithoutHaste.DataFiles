# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).TextHelper

**Static**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Contains operations for parsing and editing text.  

# Static Methods

## static [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsAllWhitespace([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Returns true if text is empty or contains only whitespace characters.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) RemoveFromEnd([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) end)

Removes the  string from the end of , if it exists there.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) RemoveFromStart([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) start)

Removes the  string from the beginning of , if it exists there.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) RemoveOuterBraces([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Removes outer matched pairs of braces from string.  
Only changes string if first and last characters are a matched pair of braces.  
Supports {}, [], (), and   
`<>`  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ReplaceUnescapedCharacters([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [char](https://docs.microsoft.com/en-us/dotnet/api/system.char) escapeChar, [char](https://docs.microsoft.com/en-us/dotnet/api/system.char) searchChar, [char](https://docs.microsoft.com/en-us/dotnet/api/system.char) replacementChar)

Replaces all instances of the unescaped  in the .  

**Remarks:**  
The  can escape itself.  

**Example A:**  
string original = "A.B.C\.D\\.E";  
string result = original('\', '.', '_');  
//result = "A_B_C\.D\\_E"  

## static [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string[]) SplitIgnoreNested([string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [char](https://docs.microsoft.com/en-us/dotnet/api/system.char) delimiter)

Split  on the   
but do not split if   
is nested within matched braces.  
Support braces: {}, [], (), and   
`<>`.  

**Remarks:**  
Returns empty string for empty matches.  

**Example A:**  
input "A,B{c,d},E[f,g,h]" returns ["A", "B{c,d}", "E[f,g,h]"]  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Delimitor cannot be a supported brace character.  
* **[WithoutHaste.DataFiles.StringFormatException](WithoutHaste.DataFiles.StringFormatException.md)**: Mismatched open/close braces.  

