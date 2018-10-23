using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles
{
	/// <summary>
	/// Contains operations for parsing and editing text.
	/// </summary>
	public static class TextHelper
	{
		/// <summary>
		/// Removes the <paramref name="end"/> string from the end of <paramref name="text"/>, if it exists there.
		/// </summary>
		public static string RemoveFromEnd(this string text, string end)
		{
			if(text.EndsWith(end))
				return text.Substring(0, text.Length - end.Length);
			return text;
		}

		/// <summary>
		/// Removes the <paramref name="start"/> string from the beginning of <paramref name="text"/>, if it exists there.
		/// </summary>
		public static string RemoveFromStart(this string text, string start)
		{
			if(text.StartsWith(start))
				return text.Substring(start.Length);
			return text;
		}

		/// <summary>
		/// Replaces all instances of the unescaped <paramref name="searchChar"/> in the <paramref name="text"/>.
		/// </summary>
		/// <remarks>
		/// The <paramref name="escapeChar"/> can escape itself.
		/// </remarks>
		/// <example>
		/// string original = "A.B.C\.D\\.E";
		/// string result = original('\', '.', '_');
		/// //result = "A_B_C\.D\\_E"
		/// </example>
		public static string ReplaceUnescapedCharacters(this string text, char escapeChar, char searchChar, char replacementChar)
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
		public static string RemoveOuterBraces(this string text)
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
		/// <exception cref="ArgumentException">Delimitor cannot be a supported brace character.</exception>
		/// <exception cref="StringFormatException">Mismatched open/close braces.</exception>
		public static string[] SplitIgnoreNested(this string text, char delimiter)
		{
			char[] supportedBraces = new char[] { '{', '}', '[', ']', '(', ')', '<', '>' };
			if(supportedBraces.Contains(delimiter))
				throw new ArgumentException("Delimitor cannot be a supported brace character: " + String.Join(",", supportedBraces) + ".");

			if(text == null) return new string[] { };

			List<string> results = new List<string>();
			int openCurlyCount = 0;
			int openSquareCount = 0;
			int openAngleCount = 0;
			int openParenCount = 0;
			int i = text.Length - 1;

			while(i >= 0)
			{
				if(text[i] == delimiter)
				{
					if(openCurlyCount == 0 && openSquareCount == 0 && openAngleCount == 0 && openParenCount == 0)
					{
						results.Add(text.Substring(i + 1));
						text = text.Substring(0, i);
					}
				}
				else
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
					}
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

		internal static bool IsEven(this int num)
		{
			return (num % 2 == 0);
		}

		internal static bool IsOdd(this int num)
		{
			return (!num.IsEven());
		}

	}
}
