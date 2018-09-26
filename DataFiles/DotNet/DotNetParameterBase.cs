using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	//todo: using this for return type, field type, property type - but those don't have names like parameters have names (the name is in the main DotNetField class for example) - how to take those out of here?

	/// <summary>
	/// Represents a parameter in a method signature.
	/// </summary>
	public abstract class DotNetParameterBase
	{
		/// <summary>Fully qualified data type name.</summary>
		public virtual string FullTypeName { get; }

		/// <summary>Local data type name.</summary>
		public virtual string LocalTypeName { get; }

		/// <summary>Parameter name in method signature. Null if not known.</summary>
		public virtual string Name { get; protected set; }

		#region Constructors

		/// <summary>
		/// Parses a .Net XML documentation parameter type name.
		/// </summary>
		public static DotNetParameterBase FromVisualStudioXml(string typeName)
		{
			if(String.IsNullOrEmpty(typeName)) return new DotNetParameter();

			if(DotNetParameterClassGeneric.HasExpectedVisualStudioXmlFormat(typeName))
				return DotNetParameterClassGeneric.FromVisualStudioXml(typeName);

			if(DotNetParameterMethodGeneric.HasExpectedVisualStudioXmlFormat(typeName))
				return DotNetParameterMethodGeneric.FromVisualStudioXml(typeName);

			//fully qualified generic type parameters
			//such as System.Collections.Generic.List<T> which are formatted as System.Collections.Generic.List{`0}
			List<DotNetParameterBase> parameters = new List<DotNetParameterBase>();
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

		/// <summary>
		/// Parses a System.Reflection.AssemblyInfo full name.
		/// </summary>
		/// <list>
		///   <item>The escape character is '\'</item>
		///   <item>Nested types are separated with '+' instead of '.'</item>
		///   <item>Class declaration of generic types are shown the same as .Net XML documentation: MyType`1 for one generic type</item>
		///   <item>When a generic type is defined: System.Collections.Generic.List`1[U], where U is the type alias from the class declaration</item>
		/// </list>
		public static DotNetParameterBase FromAssemblyInfo(Type type)
		{
			if(type == null) return new DotNetParameter();

			if(type.IsGenericParameter)
			{
				if(type.DeclaringMethod != null)
					return new DotNetParameterMethodGeneric(type.GenericParameterPosition, type.FullName);
				else
					return new DotNetParameterClassGeneric(type.GenericParameterPosition, type.FullName);
			}

			string typeName = type.FullName;
			typeName = typeName.ReplaceUnescapedCharacters('\\', '+', '.');
			List<DotNetParameterBase> parameters = new List<DotNetParameterBase>();
			int genericParameterCount = 0;
			if(type.GenericTypeArguments.Length > 0)
			{
				parameters = type.GenericTypeArguments.Select(a => FromAssemblyInfo(a)).ToList();
				typeName = typeName.Substring(0, typeName.IndexOf("["));

				Int32.TryParse(typeName.Substring(typeName.LastIndexOf("`") + 1), out genericParameterCount);
				typeName = typeName.Substring(0, typeName.LastIndexOf("`"));
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

			//so, if a generic type is nested inside another generic type, the Assembly info returns the full list of expected generic parameters to the innermost generic type
			//which means I need to pass them back up
			List<DotNetParameterBase> outerParameters = null;
			if(genericParameterCount < parameters.Count)
			{
				outerParameters = parameters.Take(parameters.Count - genericParameterCount).ToList();
				parameters = parameters.Skip(outerParameters.Count).ToList();
			}

			DotNetParameter result = new DotNetParameter(new DotNetQualifiedName(localName, parameters, DotNetQualifiedName.FromAssemblyInfo(fullNamespace)));
			if(outerParameters != null)
				result.TypeName.FullNamespace.SetMisplacedGenericParameters(outerParameters);
			return result;
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(ParameterInfo parameterInfo)
		{
			Name = parameterInfo.Name;
		}
	}
}
