using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
		#region Constructors

		/// <summary>
		/// Creates a generic-type using the <see cref='DotNetQualifiedClassName.DefaultGenericTypeNames'/>.
		/// </summary>
		/// <remarks>
		/// Index value will be set to 0 if it is less than 0.
		/// Alias will be set to "?" if the index value is out of range.
		/// </remarks>
		/// <param name="genericTypeIndex">0-based index of generic-type in relation to the class's declaration.</param>
		public DotNetReferenceClassGeneric(int genericTypeIndex)
		{
			this.genericTypeIndex = (genericTypeIndex < 0) ? 0 : genericTypeIndex;
			this.Alias = (genericTypeIndex < DotNetQualifiedClassName.DefaultGenericTypeNames.Length) ? DotNetQualifiedClassName.DefaultGenericTypeNames[this.genericTypeIndex] : "?";
		}

		/// <summary>
		/// Creates a generic-type using the provided alias.
		/// </summary>
		/// <remarks>
		/// Index value will be set to 0 if it is less than 0.
		/// </remarks>
		/// <param name="genericTypeIndex">0-based index of generic-type in relation to the class's declaration.</param>
		/// <param name="alias">The provided value will be used for the type alias, regardless of the index.</param>
		public DotNetReferenceClassGeneric(int genericTypeIndex, string alias)
		{
			this.genericTypeIndex = (genericTypeIndex < 0) ? 0 : genericTypeIndex;
			this.Alias = alias;
		}

		/// <summary>
		/// Parses a .Net XML documentation type names that reference class generic types parameters.
		/// </summary>
		/// <example>
		/// Given:
		/// <![CDATA[
		/// public class MyType<T>
		/// { 
		///		T MyField;
		/// }
		/// ]]> 
		/// the type of the field is formatted as <c>`0</c>.</example>
		/// <returns>Returns a default value if the <paramref name='typeName'/> is not in the correct format.</returns>
		public static new DotNetReferenceClassGeneric FromVisualStudioXml(string typeName)
		{
			if(!HasExpectedVisualStudioXmlFormat(typeName))
				return new DotNetReferenceClassGeneric(0);
			int genericTypeIndex = Int32.Parse(typeName.Substring(1));
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

		/// <summary>
		/// Returns true if these types match. Does not look at aliases.
		/// </summary>
		public bool MatchesSignature(DotNetQualifiedName other)
		{
			if(!(other is DotNetReferenceClassGeneric))
				return false;
			return (genericTypeIndex == (other as DotNetReferenceClassGeneric).genericTypeIndex);
		}

		/// <summary>
		/// Returns true if this generic type matches the reflected type.
		/// Compares generic indexes and whether it is a class-generic or method-generic.
		/// Does not compare aliases or which specific class/method the type is referencing.
		/// </summary>
		public bool MatchesSignature(Type type)
		{
			if(!type.IsGenericParameter)
				return false;
			if(type.DeclaringType == null)
				return false;
			if(type.GenericParameterPosition != this.genericTypeIndex)
				return false;
			return true;
		}

		/// <inheritdoc/>
		public new DotNetReferenceClassGeneric GetLocalized(DotNetQualifiedName other)
		{
			DotNetReferenceClassGeneric clone = this.Clone();
			clone.Localize(other);
			return clone;
		}

		#region Low Level

		/// <summary>
		/// Returns deep clone of generic reference name.
		/// </summary>
		public new DotNetReferenceClassGeneric Clone()
		{
			return new DotNetReferenceClassGeneric(genericTypeIndex, Alias);
		}

		#endregion
	}
}
