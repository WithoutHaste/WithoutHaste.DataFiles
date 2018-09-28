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
		/// <summary></summary>
		public override string LocalName { get { return DotNetQualifiedMethodName.GenericTypeNames[genericTypeIndex]; } }

		#region Constructors

		/// <param name="genericTypeIndex">
		///   0-based index of type in method declaration type parameter list.
		///   <example><![CDATA[Index 0 refers to "A" in "void MyMethod<A,B>() { }"]]></example>
		/// </param>
		/// <param name="alias">Alias of generic-type within assembly. Null if not known.</param>
		/// <exception cref="ArgumentException">GenericTypeIndex cannot be less than 0.</exception>
		public DotNetReferenceMethodGeneric(int genericTypeIndex, string alias = null) : base(genericTypeIndex, alias)
		{
		}

		/// <summary>
		/// Parses a .Net XML documentation method-generic-type parameter.
		/// </summary>
		/// <example><![CDATA[Namespace.MyType.MyMethod<A>(A) is formatted as Namespace.MyType.MyMethod``1(``0)]]></example>
		/// <exception cref="XmlFormatException"><paramref name="name"/> is not in expected format: ``Index.</exception>
		public static new DotNetReferenceMethodGeneric FromVisualStudioXml(string name)
		{
			if(!HasExpectedVisualStudioXmlFormat(name))
				throw new XmlFormatException("Generic parameters that refer to method-generic-types should be in the format ``Index, where Index is the 0-based index of the generic type in the method declaraction.");

			int genericTypeIndex = 0;
			Int32.TryParse(name.Substring(name.IndexOf("``") + 2), out genericTypeIndex);
			return new DotNetReferenceMethodGeneric(genericTypeIndex);
		}

		#endregion

		/// <summary>
		/// Check if a string is properly formatted as a parameter referencing a method-generic-type.
		/// </summary>
		/// <example>``0</example>
		public static bool HasExpectedVisualStudioXmlFormat(string name)
		{
			Regex regex = new Regex(@"^\`\`\d+$");
			Match match = regex.Match(name);
			return regex.Match(name).Success;
		}
	}
}
