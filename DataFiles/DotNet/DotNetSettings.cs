using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Global settings for the entire DotNet namespace.
	/// </summary>
	public static class DotNetSettings
	{
		/// <summary>
		/// When DotNetQualifiedNames are converted to strings, this converter will be automatically applied to each:
		/// * generic type parameter
		/// * method parameter
		/// * type name
		/// 
		/// Set to null to not use any converter.
		/// </summary>
		public static Func<string, string> QualifiedNameConverter = null;

		/// <summary>
		/// Converts all standard .Net types to their common aliases.
		/// </summary>
		/// <example>
		///		DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
		///		string displayString = myQualifiedTypeName.FullName;
		/// </example>
		/// <example>
		/// <![CDATA[
		/// "System.Int32" => "int"
		/// "System.Collections.Generic.List<System.Int32> => "System.Collections.Generic.List<int>"
		/// "MyType.MyMethod(System.Int32)" => "MyType.MyMethod(int)"
		/// ]]>
		/// </example>
		public static string DefaultQualifiedNameConverter(string fullName)
		{
			switch(fullName)
			{
				case "System.Boolean": return "bool";
				case "System.Byte": return "byte";
				case "System.Char": return "char";
				case "System.Decimal": return "decimal";
				case "System.Double": return "double";
				case "System.Int16": return "short";
				case "System.Int32": return "int";
				case "System.Int64": return "long";
				case "System.SByte": return "sbyte";
				case "System.Single": return "float";
				case "System.String": return "string";
				case "System.UInt16": return "ushort";
				case "System.UInt32": return "uint";
				case "System.UInt64": return "ulong";
			}
			return fullName;
		}
	}
}
