# [WithoutHaste.DataFiles](TableOfContents.WithoutHaste.DataFiles.md).TextHelper

**Static**  
**Inheritance:** [object](https://docs.microsoft.com/en-us/dotnet/api/system.object)  

Contains operations for parsing and editing text.  

# Static Methods

## static [bool](https://docs.microsoft.com/en-us/dotnet/api/system.boolean) IsAllWhitespace(this [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Returns true if text is empty or contains only whitespace characters.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) RemoveFromEnd(this [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) end)

Removes the _end_ string from the end of _text_, if it exists there.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) RemoveFromStart(this [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) start)

Removes the _start_ string from the beginning of _text_, if it exists there.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) RemoveOuterBraces(this [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text)

Removes outer matched pairs of braces from string.  
Only changes string if first and last characters are a matched pair of braces.  
Supports {}, [], (), and `<>`.  

## static [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) ReplaceUnescapedCharacters(this [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [char](https://docs.microsoft.com/en-us/dotnet/api/system.char) escapeChar, [char](https://docs.microsoft.com/en-us/dotnet/api/system.char) searchChar, [char](https://docs.microsoft.com/en-us/dotnet/api/system.char) replacementChar)

Replaces all instances of the unescaped _searchChar_ in the _text_.  

**Remarks:**  
The _escapeChar_ can escape itself.  

**Example A:**  

```
string original = "A.B.C\.D\\.E";
string result = original.ReplaceUnescapedCharacters('\', '.', '_');
//result = "A_B_C\.D\\_E"
```  

## static [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string[]) SplitIgnoreNested(this [string](https://docs.microsoft.com/en-us/dotnet/api/system.string) text, [char](https://docs.microsoft.com/en-us/dotnet/api/system.char) delimiter)

Split _text_ on the _delimiter_  
but do not split if _delimiter_is nested within matched braces.  
Support braces: {}, [], (), and `<>`.  

**Remarks:**  
Returns empty string for empty matches.  

**Example A:**  
input "A,B{c,d},E[f,g,h]" returns ["A", "B{c,d}", "E[f,g,h]"]  

**Exceptions:**  
* **[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)**: Delimitor cannot be a supported brace character.  
* **[StringFormatException](WithoutHaste.DataFiles.StringFormatException.md)**: Mismatched open/close braces.  

