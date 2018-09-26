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
			return ((methodAttributes & MethodAttributes.Static) == MethodAttributes.Static);
		}
	}
}
