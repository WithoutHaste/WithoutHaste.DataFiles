using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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
		/// Clean the formatting of all inner xml of an XElement. Returns the cleaned element.
		/// </summary>
		/// <remarks>Does not preserve attributes of the root element.</remarks>
		internal static XElement CleanWhitespaces(this XElement parent)
		{
			string innerText = String.Concat(parent.Nodes());
			innerText = innerText.TrimFromStartAsBlock();
			innerText = innerText.Trim();
			XElement cleanParent = XElement.Parse("<" + parent.Name + ">" + innerText + "</" + parent.Name + ">", LoadOptions.PreserveWhitespace);
			return cleanParent;
		}

		/// <summary>
		/// Trims an equal amount of leading white-space from each line, delimited by \n character.
		/// Callibrated for formatting blocks of text nested in XML.
		/// </summary>
		internal static string TrimFromStartAsBlock(this string text)
		{
			if(String.IsNullOrEmpty(text))
				return text;

			if(!text.Contains("\n"))
			{
				return TrimFromStartAsLine(text);
			}

			text = text.Replace("\r", "");
			if(text.IsAllWhitespace())
			{
				if(text.Contains('\n'))
					return "\n";
				return "";
			}

			while(text.StartsWith("\n"))
			{
				text = text.RemoveFromStart("\n");
			}

			string[] lines = text.Split('\n');
			Match whitespace = (new Regex(@"^\s+", RegexOptions.IgnoreCase)).Match(lines[0]);
			lines = lines.Select(l => l.RemoveFromStart(whitespace.Value)).ToArray();

			string result = String.Join("\n", lines);
			if(result.EndsWith("\n")) //trailing empty line is an artifact of XML
				result = result.RemoveFromEnd("\n");
			return result;
		}

		/// <summary>
		/// Trims extra whitespace from beginning and end of string. Allows at most one space on each end.
		/// Callibrated for formatting inline text nested in XML.
		/// </summary>
		internal static string TrimFromStartAsLine(this string text)
		{
			Match whitespace = (new Regex(@"^\s+", RegexOptions.IgnoreCase)).Match(text);
			if(!String.IsNullOrEmpty(whitespace.Value))
				text = " " + text.RemoveFromStart(whitespace.Value);

			whitespace = (new Regex(@"\s+$", RegexOptions.IgnoreCase)).Match(text);
			if(!String.IsNullOrEmpty(whitespace.Value))
				text = text.RemoveFromEnd(whitespace.Value) + " ";

			return text;
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
			if(typeInfo.FullName == "System.Delegate")
				return true;
			if(typeInfo.BaseType == null)
				return false;
			return typeInfo.BaseType.IsDelegate();
		}

		internal static bool IsDelegate(this Type type)
		{
			if(type.FullName == "System.Delegate")
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

		internal static bool IsNumeric(this object _object)
		{
			return (_object is sbyte
				|| _object is byte
				|| _object is short
				|| _object is ushort
				|| _object is int
				|| _object is uint
				|| _object is long
				|| _object is ulong
				|| _object is float
				|| _object is double
				|| _object is decimal
			);
		}
	}
}
