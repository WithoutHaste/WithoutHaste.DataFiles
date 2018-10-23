using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	internal static class Utilities
	{
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

		/// <summary>
		/// Trims an equal amount of leading white-space from each line, delimited by \n character.
		/// Callibrated for formatting blocks of text nested in XML.
		/// </summary>
		internal static string TrimFromStartAsBlock(this string text)
		{
			if(String.IsNullOrEmpty(text))
				return text;

			text = text.Replace("\r", "");
			while(text.StartsWith("\n"))
			{
				text = text.RemoveFromStart("\n");
			}

			if(String.IsNullOrEmpty(text))
				return text;

			string[] lines = text.Split('\n');
			Match whitespace = (new Regex(@"^\s+", RegexOptions.IgnoreCase)).Match(lines[0]);
			lines = lines.Select(l => l.RemoveFromStart(whitespace.Value)).ToArray();

			string result = String.Join("\n", lines);
			if(result.EndsWith("\n")) //trailing empty line is an artifact of XML
				result = result.RemoveFromEnd("\n");
			return result;
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

		internal static bool IsDelegate(this TypeInfo typeInfo)
		{
			if(typeInfo.FullName == "System.MulticastDelegate")
				return true;
			if(typeInfo.BaseType == null)
				return false;
			return typeInfo.BaseType.IsDelegate();
		}

		internal static bool IsDelegate(this Type type)
		{
			if(type.FullName == "System.MulticastDelegate")
				return true;
			if(type.BaseType == null)
				return false;
			return type.BaseType.IsDelegate();
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
