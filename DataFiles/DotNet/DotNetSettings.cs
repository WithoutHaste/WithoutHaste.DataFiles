using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		/// <remarks>
		/// Setting always defaults to <see cref="DefaultQualifiedNameConverter"/>.
		/// With target frameworks less than 3.5, you can turn this on or off with <see cref='UseDefaultQualifiedNameConverter(bool)'/>.
		/// With target frameworks 3.5 or higher, you can set this to a custom function.
		/// 
		/// See <see cref="DefaultQualifiedNameConverter"/> for usage examples.
		/// </remarks>
		/// <example>
		/// <code>
		///		DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
		///		string displayString = myQualifiedTypeName.FullName;
		///	</code>	
		/// </example>
#if FUNCS
		public static Func<string, int, string> QualifiedNameConverter = DefaultQualifiedNameConverter;
#else
		internal static Func<string, int, string> QualifiedNameConverter = DefaultQualifiedNameConverter;

		/// <summary>
		/// Toggle the <see cref="DefaultQualifiedNameConverter"/> on and off.
		/// </summary>
		/// <remarks>
		/// Only available in target frameworks less than 3.5.
		/// </remarks>
		/// <param name="useDefault">True to use the default converter, false to not use a converter.</param>
		public static void UseDefaultQualifiedNameConverter(bool useDefault)
		{
			if(useDefault)
				QualifiedNameConverter = DefaultQualifiedNameConverter;
			else
				QualifiedNameConverter = null;
		}
#endif

		/// <summary>
		/// A second level <see cref="QualifiedNameConverter"/> to provide further processing.
		/// This method will be run after <see cref="QualifiedNameConverter"/> for each <see cref="DotNetQualifiedName"/>.
		/// 
		/// Set to null to not use any converter.
		/// </summary>
		/// <remarks>
		/// Setting always defaults to null.
		/// With target frameworks 3.5 or higher, you can change this setting.
		/// </remarks>
		/// <example>
		/// <code>
		///		DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
		///		DotNetSettings.AdditionalQualifiedNameConverter = myCustomConverter;
		///		string displayString = myQualifiedTypeName.FullName;
		///	</code>	
		/// </example>
#if FUNCS
		public static Func<string, int, string> AdditionalQualifiedNameConverter = null;
#else
		internal static Func<string, int, string> AdditionalQualifiedNameConverter = null;
#endif

		/// <summary>
		/// Converts all standard .Net types to their common aliases.
		/// </summary>
		/// <example>
		/// <code>
		///		DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
		///		string displayString = myQualifiedTypeName.FullName;
		///	</code>
		/// </example>
		/// <example>
		/// <![CDATA[
		/// "System.Int32" => "int"
		/// "System.Collections.Generic.List<System.Int32> => "System.Collections.Generic.List<int>"
		/// "MyType.MyMethod(System.Int32)" => "MyType.MyMethod(int)"
		/// ]]>
		/// </example>
		/// <param name="fullName">
		///	When processing name "System.Collections.Generic.List", fullName will be "System" then "System.Collections" then "System.Collections.Generic" then "System.Collections.Generic.List".
		/// </param>
		/// <param name="depth">
		///	When processing name "System.Collections.Generic.List", depth will be 3 at "System", then 2 at "Collections", then 1 at "Generic", then 0 at "List".
		/// </param>
		public static string DefaultQualifiedNameConverter(string fullName, int depth)
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
				case "System.Object": return "object";
				case "System.SByte": return "sbyte";
				case "System.Single": return "float";
				case "System.String": return "string";
				case "System.UInt16": return "ushort";
				case "System.UInt32": return "uint";
				case "System.UInt64": return "ulong";
				case "System.Void": return "void";
			}
			return fullName;
		}
	}
}
