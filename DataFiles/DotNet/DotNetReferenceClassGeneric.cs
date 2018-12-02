using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a generic-type parameter that is in reference to a class's declared generic types.
	/// </summary>
	/// <example>
	/// The "T" and "U" in the constructor parameters.
	/// <![CDATA[
	/// class MyGeneric<T,U>
	/// {
	///     public MyGeneric(T t, U u) { }
	/// }
	/// ]]>
	/// </example>
	public class DotNetReferenceClassGeneric : DotNetReferenceGeneric
	{
		/// <summary></summary>
		public override string LocalName { get { return DotNetQualifiedClassName.GenericTypeNames[genericTypeIndex]; } }

		#region Constructors

		/// <param name="genericTypeIndex">
		///   0-based index of type in class declaration type parameter list.
		///   <example><![CDATA[Index 0 refers to "T" in "class MyGeneric<T,U> { }"]]></example>
		/// </param>
		/// <param name="alias">Alias of generic-type within assembly. Null if not known.</param>
		/// <exception cref="ArgumentException"><paramref name='genericTypeIndex'/> cannot be less than 0.</exception>
		public DotNetReferenceClassGeneric(int genericTypeIndex, string alias = null) : base(genericTypeIndex, alias)
		{
		}

		/// <summary>
		/// Parses a .Net XML documentation class-generic-type parameter.
		/// </summary>
		/// <example><![CDATA[Namespace.MyType<T>{ }]]> is formatted as <c>Namespace.MyType`1</c>.</example>
		/// <exception cref="XmlFormatException"><paramref name="name"/> is not in expected format: <c>`Index</c>.</exception>
		public static new DotNetReferenceClassGeneric FromVisualStudioXml(string name)
		{
			if(!HasExpectedVisualStudioXmlFormat(name))
				throw new XmlFormatException("Generic parameters that refer to class-generic-types should be in the format `Index, where Index is the 0-based index of the generic type in the class declaraction.");

			int genericTypeIndex = 0;
			Int32.TryParse(name.Substring(name.IndexOf("`") + 1), out genericTypeIndex);
			return new DotNetReferenceClassGeneric(genericTypeIndex);
		}

		#endregion

		/// <summary>
		/// Check if a string is properly formatted as a parameter referencing a class-generic-type.
		/// </summary>
		/// <example><c>`0</c>, <c>`1</c>, <c>`2</c>, etc.</example>
		public static bool HasExpectedVisualStudioXmlFormat(string name)
		{
			Regex regex = new Regex(@"^\`\d+$");
			Match match = regex.Match(name);
			return regex.Match(name).Success;
		}
	}
}
