﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents an indexer property.
	/// </summary>
	public class DotNetIndexer : DotNetProperty
	{
		/// <summary></summary>
		public List<DotNetParameter> Parameters = new List<DotNetParameter>();

		/// <summary>Returns indexer parameters format as "[TypeA,TypeB]".</summary>
		public string ParameterTypesSignature {
			get {
				return "[" + String.Join(",", Parameters.Select(p => p.FullTypeName).ToArray()) + "]";
			}
		}

		/// <summary>Returns indexer parameters format as "[TypeA a, TypeB b]".</summary>
		public string ParametersSignature {
			get {
				return "[" + String.Join(", ", Parameters.Select(p => p.FullTypeName + " " + p.Name).ToArray()) + "]";
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetIndexer(DotNetQualifiedName name, List<DotNetParameter> parameters) : base(name)
		{
			Parameters.AddRange(parameters);
		}

		/// <summary>
		/// Parse .Net XML documentation for Indexer data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		public static new DotNetIndexer FromVisualStudioXml(XElement memberElement)
		{
			string xmlName = memberElement.Attribute("name")?.Value;
			string xmlParameters = xmlName.Substring(xmlName.IndexOf("("));
			xmlName = xmlName.Substring(0, xmlName.IndexOf("("));

			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(xmlName);
			List<DotNetParameter> parameters = DotNetMethod.ParametersFromVisualStudioXml(xmlParameters);
			DotNetIndexer indexer = new DotNetIndexer(name, parameters);
			indexer.ParseVisualStudioXmlDocumentation(memberElement);
			return indexer;
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public override void AddAssemblyInfo(PropertyInfo propertyInfo)
		{
			base.AddAssemblyInfo(propertyInfo);

			int index = 0;
			foreach(ParameterInfo parameterInfo in propertyInfo.GetGetMethod().GetParameters())
			{
				Parameters[index].AddAssemblyInfo(parameterInfo);
				index++;
			}
		}

		/// <summary>
		/// Returns true if this method's signature matches the reflected MethodInfo.
		/// </summary>
		public bool MatchesSignature(MethodInfo methodInfo)
		{
			if(methodInfo.Name != "get_Item")
				return false;
			return MatchesArguments(methodInfo.GetParameters());
		}

		/// <summary>
		/// Returns true if this method's parameter list matches the reflected ParameterInfo.
		/// </summary>
		public bool MatchesArguments(ParameterInfo[] otherParameters)
		{
			if(Parameters.Count != otherParameters.Length)
				return false;

			for(int i = 0; i < Parameters.Count; i++)
			{
				string otherName = DotNetQualifiedTypeName.FromAssemblyInfo(otherParameters[i].ParameterType).FullName;
				//todo: something about parameters that end with @ vs &
				if(Parameters[i].FullTypeName != otherName)
					return false;
			}
			return true;
		}

	}
}