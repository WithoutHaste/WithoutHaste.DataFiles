using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	internal static class Utilities
	{
		internal static string RemoveFromEnd(this string text, string end)
		{
			if(text.EndsWith(end))
				return text.Substring(0, text.Length - end.Length);
			return text;
		}

		internal static string RemoveFromStart(this string text, string start)
		{
			if(text.StartsWith(start))
				return text.Substring(start.Length);
			return text;
		}

		internal static string ReplaceUnescapedCharacters(this string text, char escapeChar, char searchChar, char replacementChar)
		{
			if(String.IsNullOrEmpty(text))
				return text;

			for(int i = text.Length - 1; i >= 0; i--)
			{
				if(text[i] != searchChar)
					continue;
				if(i > 0 && text[i - 1] == escapeChar)
				{
					int countEscapeChars = CountPreceedingRepeatedChars(text, i - 1, escapeChar);
					if(countEscapeChars.IsOdd())
						continue; //searchChar is escaped
				}
				//replace searchChar
				text = text.Substring(0, i) + replacementChar + text.Substring(i + 1);
			}
			return text;
		}

		/// <summary>
		/// Returns the number of sequential characters that all match the selected character, working backwards from the starting index.
		/// </summary>
		/// <example>
		/// ("abcDDDefg", 5, 'D') returns 3 
		/// ("abcDDDefg", 4, 'D') returns 2 
		/// </example>
		/// <param name="text"></param>
		/// <param name="startIndex">First index checked for <paramref name="searchChar"/>.</param>
		/// <param name="searchChar"></param>
		internal static int CountPreceedingRepeatedChars(string text, int startIndex, char searchChar)
		{
			int count = 0;
			for(int i = startIndex; i >= 0 && i < text.Length; i--)
			{
				if(text[i] != searchChar) break;
				count++;
			}
			return count;
		}

		/// <summary>
		/// Removes outer matched pairs of braces from string.
		/// Only changes string if first and last characters are a matched pair of braces.
		/// Supports {}, [], (), and <![CDATA[<>]]>
		/// </summary>
		internal static string RemoveOuterBraces(this string text)
		{
			if(text == null)
				return null;

			if(
				(text.StartsWith("{") && text.EndsWith("}")) ||
				(text.StartsWith("[") && text.EndsWith("]")) ||
				(text.StartsWith("(") && text.EndsWith(")")) ||
				(text.StartsWith("<") && text.EndsWith(">"))
			)
				return text.Substring(1, text.Length - 2);

			return text;
		}

		/// <summary>
		/// Split <paramref name="text"/> on the <paramref name="delimiter"/> 
		/// but do not split if <paramref name="delimiter"/> is nested within matched braces.
		/// Support braces: {}, [], (), and <![CDATA[<>]]>.
		/// </summary>
		/// <remarks>
		/// Returns empty string for empty matches.
		/// </remarks>
		/// <example>
		/// input "A,B{c,d},E[f,g,h]" returns ["A", "B{c,d}", "E[f,g,h]"]
		/// </example>
		/// <exception cref="StringFormatException">Mismatched open/close braces.</exception>
		internal static string[] SplitIgnoreNested(this string text, char delimiter)
		{
			if(text == null) return new string[] { };

			List<string> results = new List<string>();
			int openCurlyCount = 0;
			int openSquareCount = 0;
			int openAngleCount = 0;
			int openParenCount = 0;
			int i = text.Length - 1;

			while(i >= 0)
			{
				switch(text[i])
				{
					case '}': openCurlyCount++; break;
					case ']': openSquareCount++; break;
					case '>': openAngleCount++; break;
					case ')': openParenCount++; break;

					case '{': openCurlyCount--; break;
					case '[': openSquareCount--; break;
					case '<': openAngleCount--; break;
					case '(': openParenCount--; break;

					case ',':
						if(openCurlyCount > 0 || openSquareCount > 0 || openAngleCount > 0 || openParenCount > 0)
							break;
						results.Add(text.Substring(i + 1));
						text = text.Substring(0, i);
						break;
				}
				if(openCurlyCount < 0 || openSquareCount < 0 || openAngleCount < 0 || openParenCount < 0)
					throw new StringFormatException("Mismatched open/close braces.");
				i--;
			}
			if(openCurlyCount > 0 || openSquareCount > 0 || openAngleCount > 0 || openParenCount > 0)
				throw new StringFormatException("Mismatched open/close braces.");
			results.Add(text);

			results.Reverse();

			return results.ToArray();
		}

		/// <summary>
		/// Trims leading and trailing whitespaces. Will leave one leading and one trailing space, but won't add them.
		/// </summary>
		internal static string TrimAllowOneSpace(this string text)
		{
			string trimmedEnd = text.TrimEnd();
			if(trimmedEnd.Length < text.Length && text[trimmedEnd.Length] == ' ')
			{
				trimmedEnd += " ";
			}

			string trimmedBoth = trimmedEnd.TrimStart();
			if(trimmedBoth.Length < trimmedEnd.Length && trimmedEnd[trimmedEnd.Length - trimmedBoth.Length - 1] == ' ')
			{
				trimmedBoth = " " + trimmedBoth;
			}

			return trimmedBoth;
		}

		internal static bool IsEven(this int num)
		{
			return (num % 2 == 0);
		}

		internal static bool IsOdd(this int num)
		{
			return (!num.IsEven());
		}

		internal static bool IsAbstract(this TypeAttributes typeAttributes)
		{
			if((typeAttributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				return false;
			if((typeAttributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
				return false;
			return ((typeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract);
		}

		internal static bool IsStatic(this TypeAttributes typeAttributes)
		{
			if((typeAttributes & TypeAttributes.Sealed) != TypeAttributes.Sealed)
				return false;
			return ((typeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract);
		}

		internal static bool IsInterface(this TypeAttributes typeAttributes)
		{
			if((typeAttributes & TypeAttributes.ClassSemanticsMask) != TypeAttributes.ClassSemanticsMask)
				return false;
			return ((typeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract);
		}

		internal static bool IsEnum(this TypeInfo typeInfo)
		{
			return (typeInfo.BaseType != null && typeInfo.BaseType.Name == "Enum");
		}

		internal static bool IsEnum(this Type type)
		{
			return (type.BaseType != null && type.BaseType.Name == "Enum");
		}

		internal static bool IsException(this TypeInfo typeInfo)
		{
			if(typeInfo.FullName == "System.Exception")
				return true;
			if(typeInfo.BaseType == null)
				return false;
			return typeInfo.BaseType.IsException();
		}

		internal static bool IsException(this Type type)
		{
			if(type.FullName == "System.Exception")
				return true;
			if(type.BaseType == null)
				return false;
			return type.BaseType.IsException();
		}

		internal static bool IsConstant(this FieldAttributes fieldAttributes)
		{
			FieldAttributes READONLY_FIELDATTRIBUTES = FieldAttributes.Static | FieldAttributes.InitOnly;
			FieldAttributes CONSTANT_FIELDATTRIBUTES = FieldAttributes.Static | FieldAttributes.Literal;

			if((fieldAttributes & READONLY_FIELDATTRIBUTES) == READONLY_FIELDATTRIBUTES)
				return true;
			return ((fieldAttributes & CONSTANT_FIELDATTRIBUTES) == CONSTANT_FIELDATTRIBUTES);
		}

		internal static bool IsPrivate(this MethodAttributes methodAttributes)
		{
			return ((methodAttributes & MethodAttributes.Private) == MethodAttributes.Private);
		}

		internal static bool IsStatic(this MethodAttributes methodAttributes)
		{
			return ((methodAttributes & MethodAttributes.Static) == MethodAttributes.Static && !IsAbstract(methodAttributes));
		}

		internal static bool IsAbstract(this MethodAttributes methodAttributes)
		{
			return ((methodAttributes & MethodAttributes.Abstract) == MethodAttributes.Abstract);
		}
	}
}
