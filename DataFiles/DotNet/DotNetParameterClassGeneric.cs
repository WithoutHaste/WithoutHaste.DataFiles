using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a class-generic-type parameter in a method signature.
	/// </summary>
	/// <example><![CDATA[Namespace.MyType<T,U>.MyMethod(T) => Namespace.MyType`2.MyMethod(`0)]]></example>
	public class DotNetParameterClassGeneric : DotNetParameterGeneric
	{
		/// <inheritdoc/>
		public override string FullTypeName { get { return LocalTypeName; } }

		/// <inheritdoc/>
		public override string LocalTypeName { get { return DotNetQualifiedClassName.GenericTypeNames[genericTypeIndex]; } }

		/// <summary>0-based index in class's generic type list corresponding to this parameter.</summary>
		protected int genericTypeIndex = 0;

		#region Constructors

		/// <summary></summary>
		public DotNetParameterClassGeneric(int genericTypeIndex) : this(genericTypeIndex, null)
		{
		}

		/// <summary></summary>
		/// <param name="genericTypeIndex">0-based index in class's generic type list corresponding to this parameter.</param>
		/// <param name="alias">Assembly-defined alias of generic type.</param>
		public DotNetParameterClassGeneric(int genericTypeIndex, string alias) : base(alias)
		{
			if(genericTypeIndex < 0 || genericTypeIndex >= DotNetQualifiedClassName.GenericTypeNames.Length)
				throw new ArgumentException(String.Format("GenericTypeIndex is invalid. Expects range [0,{0}]. Index: {1}.", DotNetQualifiedClassName.GenericTypeNames.Length - 1, genericTypeIndex), "genericTypeIndex");
			this.genericTypeIndex = genericTypeIndex;
		}

		/// <summary>
		/// Parses a .Net XML documentation class-generic-type parameter.
		/// </summary>
		/// <exception cref="XmlFormatException"><paramref name="name"/> is not in expected format: `Index.</exception>
		public static new DotNetParameterClassGeneric FromVisualStudioXml(string name)
		{
			if(!HasExpectedVisualStudioXmlFormat(name))
				throw new XmlFormatException("Generic parameters that refer to class-generic-types should be in the format `Index, where Index is the 0-based index of the generic type in the class declaraction.");

			int genericTypeIndex = 0;
			Int32.TryParse(name.Substring(name.IndexOf('`') + 1), out genericTypeIndex);
			return new DotNetParameterClassGeneric(genericTypeIndex);
		}

		#endregion

		/// <summary>
		/// Check if a string is properly formatted as a parameter referencing a class-generic-type.
		/// </summary>
		/// <example>`0</example>
		public static bool HasExpectedVisualStudioXmlFormat(string name)
		{
			Regex regex = new Regex(@"^\`\d+$");
			Match match = regex.Match(name);
			return regex.Match(name).Success;
		}
	}
}
