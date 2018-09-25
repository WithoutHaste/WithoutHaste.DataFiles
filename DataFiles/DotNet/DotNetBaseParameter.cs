using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a parameter in a method signature.
	/// </summary>
	public abstract class DotNetBaseParameter
	{
		/// <summary>Fully qualified data type name.</summary>
		public virtual string FullName { get; }

		/// <summary>Local data type name.</summary>
		public virtual string LocalName { get; }
		
		/// <summary>
		/// Parses a .Net XML documentation parameter type name.
		/// </summary>
		public static DotNetBaseParameter FromVisualStudioXml(string typeName)
		{
			if(String.IsNullOrEmpty(typeName)) return new DotNetParameter();

			if(DotNetClassGenericParameter.HasExpectedVisualStudioXmlFormat(typeName))
				return DotNetClassGenericParameter.FromVisualStudioXml(typeName);

			if(DotNetMethodGenericParameter.HasExpectedVisualStudioXmlFormat(typeName))
				return DotNetMethodGenericParameter.FromVisualStudioXml(typeName);

			//fully qualified generic type parameters
			//such as System.Collections.Generic.List<T> which are formatted as System.Collections.Generic{`0}
			List<DotNetBaseParameter> parameters = new List<DotNetBaseParameter>();
			if(typeName.EndsWith("}"))
			{
				string parameterSignature = typeName.Substring(typeName.IndexOf("{"));
				typeName = typeName.Substring(0, typeName.IndexOf("{"));
				parameters = DotNetMethod.ParametersFromVisualStudioXml(parameterSignature);
			}

			int divider = typeName.LastIndexOf('.');
			string localName = typeName;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = typeName.Substring(divider + 1);
				fullNamespace = typeName.Substring(0, divider);
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetParameter(new DotNetQualifiedName(localName, parameters));

			return new DotNetParameter(new DotNetQualifiedName(localName, parameters, DotNetQualifiedName.TypeNameFromVisualStudioXml(fullNamespace)));
		}
	}
}
