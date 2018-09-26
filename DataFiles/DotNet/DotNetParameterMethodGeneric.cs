using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a method-generic-type parameter in a method signature.
	/// </summary>
	/// <example><![CDATA[Namespace.MyType.MyMethod<A>(A) => Namespace.MyType.MyMethod``1(``0)]]></example>
	public class DotNetParameterMethodGeneric : DotNetParameterGeneric
	{
		/// <inheritdoc/>
		public override string FullTypeName { get { return LocalTypeName; } }

		/// <inheritdoc/>
		public override string LocalTypeName { get { return DotNetQualifiedMethodName.GenericTypeNames[genericTypeIndex]; } }

		/// <summary>0-based index in method's generic type list corresponding to this parameter.</summary>
		protected int genericTypeIndex = 0;

		#region Constructors

		/// <summary></summary>
		/// <param name="genericTypeIndex">0-based index in method's generic type list corresponding to this parameter.</param>
		public DotNetParameterMethodGeneric(int genericTypeIndex) : this(genericTypeIndex, null)
		{
		}

		/// <summary></summary>
		/// <param name="genericTypeIndex">0-based index in method's generic type list corresponding to this parameter.</param>
		/// <param name="alias">Assembly-defined alias of generic type.</param>
		public DotNetParameterMethodGeneric(int genericTypeIndex, string alias) : base(alias)
		{
			if(genericTypeIndex < 0 || genericTypeIndex >= DotNetQualifiedMethodName.GenericTypeNames.Length)
				throw new ArgumentException(String.Format("GenericTypeIndex is invalid. Expects range [0,{0}]. Index: {1}.", DotNetQualifiedMethodName.GenericTypeNames.Length - 1, genericTypeIndex), "genericTypeIndex");
			this.genericTypeIndex = genericTypeIndex;
		}

		/// <summary>
		/// Parses a .Net XML documentation method-generic-type parameter.
		/// </summary>
		/// <exception cref="XmlFormatException"><paramref name="name"/> is not in expected format: ``Index.</exception>
		public static new DotNetParameterMethodGeneric FromVisualStudioXml(string name)
		{
			if(!HasExpectedVisualStudioXmlFormat(name))
				throw new XmlFormatException("Generic parameters that refer to method-generic-types should be in the format ``Index, where Index is the 0-based index of the generic type in the method declaraction.");

			int genericTypeIndex = 0;
			Int32.TryParse(name.Substring(name.IndexOf("``") + 2), out genericTypeIndex);
			return new DotNetParameterMethodGeneric(genericTypeIndex);
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
