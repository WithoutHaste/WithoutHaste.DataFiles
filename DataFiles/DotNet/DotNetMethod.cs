using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a method.
	/// </summary>
	public class DotNetMethod : DotNetMember
	{
		/// <summary>True if this method represents a constructor.</summary>
		public bool IsConstructor { get; protected set; }

		/// <summary>True if this method represents an overloaded operator.</summary>
		public bool IsOperator { get; protected set; }

		/// <summary></summary>
		public List<DotNetBaseParameter> Parameters = new List<DotNetBaseParameter>();

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetMethod() : base(new DotNetQualifiedName(null))
		{
		}

		/// <summary>Normal constructor</summary>
		public DotNetMethod(DotNetQualifiedName name, List<DotNetBaseParameter> parameters, bool isConstructor = false, bool isOperator = false) : base(name)
		{
			this.Parameters.AddRange(parameters);
			IsConstructor = isConstructor;
			IsOperator = isOperator;
		}

		/// <summary>
		/// Parse .Net XML documentation for method signature data.
		/// </summary>
		/// <param name="memberElement">Expects tag "member".</param>
		public static DotNetMethod FromVisualStudioXml(XElement memberElement)
		{
			string signature = memberElement.Attribute("name")?.Value;
			if(signature == null)
				return new DotNetMethod();

			//parameterless methods don't have a () at all
			int divider = signature.IndexOf('(');
			string name = null;
			string parameters = null;
			if(divider == -1)
			{
				name = signature;
			}
			else
			{
				name = signature.Substring(0, divider);
				parameters = signature.Substring(divider);
			}

			DotNetQualifiedName qualifiedName = DotNetQualifiedName.FromVisualStudioXml(name);

			//for constructors
			bool isConstructor = qualifiedName.LocalName.EndsWith("#ctor");
			if(isConstructor)
			{
				qualifiedName = qualifiedName.SetLocalName(qualifiedName.FullNamespace.LocalName);
			}

			//for operators
			bool isOperator = qualifiedName.LocalName.StartsWith("op_");
			if(isOperator)
			{
				qualifiedName = qualifiedName.SetLocalName(qualifiedName.LocalName.RemoveFromStart("op_"));
			}

			//parse parameters
			List<DotNetBaseParameter> qualifiedParameters = ParametersFromVisualStudioXml(parameters);

			return new DotNetMethod(qualifiedName, qualifiedParameters, isConstructor, isOperator);
		}

		#endregion

		/// <summary>
		/// Parse .Net XML documentation parameter lists.
		/// </summary>
		/// <param name="text">
		/// Expects: null
		/// Expects: empty string
		/// Expects: "(type, type, type)"
		/// </param>
		public static List<DotNetBaseParameter> ParametersFromVisualStudioXml(string text)
		{
			List<DotNetBaseParameter> parameters = new List<DotNetBaseParameter>();
			if(!string.IsNullOrEmpty(text))
			{
				if(text.StartsWith("{") && text.EndsWith("}"))
				{
					text = text.Substring(1, text.Length - 2);
				}
				text = text.Replace("(", "").Replace(")", "");
				string[] fields = text.Split(',');
				for(int i = 0; i < fields.Length; i++)
				{
					string f = fields[i];
					if(!String.IsNullOrEmpty(f))
					{
						parameters.Add(DotNetBaseParameter.FromVisualStudioXml(f));
					}
				}
			}
			return parameters;
		}
	}
}
