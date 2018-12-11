using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a generic-type parameter that is in reference to a method's declared generic types.
	/// </summary>
	/// <example>
	/// The "A" and "B" in the MyMethod parameters.
	/// <![CDATA[
	/// class MyGeneric<T,U>
	/// {
	///     public MyGeneric(T t, U u) { }
	///     
	///     public void MyMethod<A,B>(A a, B b, T t, U u) { }
	/// }
	/// ]]>
	/// </example>
	public class DotNetReferenceMethodGeneric : DotNetReferenceGeneric
	{
		#region Constructors

		/// <summary>
		/// Creates a generic-type using the <see cref='DotNetQualifiedMethodName.DefaultGenericTypeNames'/>.
		/// </summary>
		/// <remarks>
		/// Index value will be set to 0 if it is less than 0.
		/// Alias will be set to "?" if the index value is out of range.
		/// </remarks>
		/// <param name="genericTypeIndex">0-based index of generic-type in relation to the method's declaration.</param>
		public DotNetReferenceMethodGeneric(int genericTypeIndex)
		{
			this.genericTypeIndex = (genericTypeIndex < 0) ? 0 : genericTypeIndex;
			this.Alias = (genericTypeIndex < DotNetQualifiedMethodName.DefaultGenericTypeNames.Length) ? DotNetQualifiedMethodName.DefaultGenericTypeNames[this.genericTypeIndex] : "?";
		}

		/// <summary>
		/// Creates a generic-type using the provided alias.
		/// </summary>
		/// <remarks>
		/// Index value will be set to 0 if it is less than 0.
		/// </remarks>
		/// <param name="genericTypeIndex">0-based index of generic-type in relation to the method's declaration.</param>
		/// <param name="alias">The provided value will be used for the type alias, regardless of the index.</param>
		public DotNetReferenceMethodGeneric(int genericTypeIndex, string alias)
		{
			this.genericTypeIndex = (genericTypeIndex < 0) ? 0 : genericTypeIndex;
			this.Alias = alias;
		}

		/// <summary>
		/// Parses a .Net XML documentation type names that reference method generic type parameters.
		/// </summary>
		/// <example>
		/// Given:
		/// <![CDATA[
		/// public class MyType
		/// { 
		///		public void MyMethod<A>(A a) { }
		/// }
		/// ]]> 
		/// the type of the method parameter is formatted as <c>``0</c>.</example>
		/// <returns>Returns a default value if the <paramref name='typeName'/> is not in the correct format.</returns>
		public static new DotNetReferenceMethodGeneric FromVisualStudioXml(string typeName)
		{
			if(!HasExpectedVisualStudioXmlFormat(typeName))
				return new DotNetReferenceMethodGeneric(0);
			int genericTypeIndex = Int32.Parse(typeName.Substring(2));
			return new DotNetReferenceMethodGeneric(genericTypeIndex);
		}

		#endregion

		/// <summary>
		/// Check if a string is properly formatted as a parameter referencing a method-generic-type.
		/// </summary>
		/// <example><c>``0</c>, <c>``1</c>, <c>``2</c>, etc.</example>
		public static bool HasExpectedVisualStudioXmlFormat(string name)
		{
			Regex regex = new Regex(@"^\`\`\d+$");
			Match match = regex.Match(name);
			return regex.Match(name).Success;
		}

		/// <summary>
		/// Returns true if these types match. Does not look at aliases.
		/// </summary>
		public bool MatchesSignature(DotNetQualifiedName other)
		{
			if(!(other is DotNetReferenceMethodGeneric))
				return false;
			return (genericTypeIndex == (other as DotNetReferenceMethodGeneric).genericTypeIndex);
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
			if(type.DeclaringMethod == null)
				return false;
			if(type.GenericParameterPosition != this.genericTypeIndex)
				return false;
			return true;
		}

		/// <inheritdoc/>
		public new DotNetReferenceMethodGeneric GetLocalized(DotNetQualifiedName other)
		{
			DotNetReferenceMethodGeneric clone = this.Clone();
			clone.Localize(other);
			return clone;
		}

		#region Low Level

		/// <summary>
		/// Returns deep clone of generic reference name.
		/// </summary>
		public new DotNetReferenceMethodGeneric Clone()
		{
			return new DotNetReferenceMethodGeneric(genericTypeIndex, Alias);
		}

		#endregion
	}
}
