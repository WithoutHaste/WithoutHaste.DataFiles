using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
			if(lines.All(l => (l.StartsWith(whitespace.Value) || l.IsAllWhitespace())))
			{
				lines = lines.Select(l => l.RemoveFromStart(whitespace.Value)).ToArray();
			}

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

		internal static bool IsEnum(this Type type)
		{
			return (type.BaseType != null && type.BaseType.Name == "Enum");
		}

		internal static bool IsException(this Type type)
		{
			if(type.FullName == "System.Exception")
				return true;
			if(type.BaseType == null)
				return false;
			return type.BaseType.IsException();
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

		internal static bool IsExtension(this MethodInfo methodInfo)
		{
			return methodInfo.CustomAttributes.Any(attr => attr.AttributeType == typeof(System.Runtime.CompilerServices.ExtensionAttribute));
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

		internal static FieldInfo[] GetDeclaredFields(this Type type)
		{
			List<FieldInfo> results = new List<FieldInfo>();
			//public instance fields
			results.AddRange(type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
			//public static fields
			results.AddRange(type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public));
			//protected/private instance fields
			results.AddRange(type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic)); //.Where(f => f.IsPrivate == false));
			//protected/private static fields
			results.AddRange(type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic)); //.Where(f => f.IsPrivate == false));
			return results.ToArray();
		}

		internal static PropertyInfo[] GetDeclaredProperties(this Type type)
		{
			List<PropertyInfo> results = new List<PropertyInfo>();
			//public instance properties
			results.AddRange(type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
			//public static properties
			results.AddRange(type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public));
			//protected/private instance properties
			results.AddRange(type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic)); //.Where(f => f.IsPrivate == false));
			//protected/private static properties
			results.AddRange(type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic)); //.Where(f => f.IsPrivate == false));
			return results.ToArray();
		}

		internal static MethodInfo[] GetDeclaredMethods(this Type type)
		{
			List<MethodInfo> results = new List<MethodInfo>();
			//public instance methods
			results.AddRange(type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
			//public static methods
			results.AddRange(type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public));
			//protected/private instance methods
			results.AddRange(type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic)); //.Where(f => f.IsPrivate == false));
			//protected/private static methods
			results.AddRange(type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic)); //.Where(f => f.IsPrivate == false));
			return results.ToArray();
		}

		internal static ConstructorInfo[] GetDeclaredConstructors(this Type type)
		{
			List<ConstructorInfo> results = new List<ConstructorInfo>();
			results.AddRange(type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
			results.AddRange(type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public));
			results.AddRange(type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic));
			results.AddRange(type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic));
			return results.ToArray();
		}

		internal static EventInfo[] GetDeclaredEvents(this Type type)
		{
			List<EventInfo> results = new List<EventInfo>();
			results.AddRange(type.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
			results.AddRange(type.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public));
			results.AddRange(type.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic));
			results.AddRange(type.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic));
			return results.ToArray();
		}

		internal static Type[] GetDeclaredNestedTypes(this Type type)
		{
			List<Type> results = new List<Type>();
			results.AddRange(type.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public));
			results.AddRange(type.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public));
			results.AddRange(type.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic));
			results.AddRange(type.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic));
			return results.ToArray();
		}
	}
}
