using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a type's property.
	/// </summary>
	public class DotNetProperty : DotNetField
	{
		/// <summary></summary>
		public DotNetPropertyMethod GetterMethod { get; protected set; }

		/// <summary></summary>
		public DotNetPropertyMethod SetterMethod { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetProperty(DotNetQualifiedName name) : base(name)
		{
		}

		/// <summary>
		/// Parse .Net XML documentation for Property data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		public static new DotNetProperty FromVisualStudioXml(XElement memberElement)
		{
			string xmlName = memberElement.Attribute("name")?.Value;
			if(xmlName.IndexOf("(") > -1)
			{
				return DotNetIndexer.FromVisualStudioXml(memberElement);
			}

			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(xmlName);
			DotNetProperty property = new DotNetProperty(name);
			property.ParseVisualStudioXmlDocumentation(memberElement);
			return property;
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(PropertyInfo propertyInfo)
		{
			GetterMethod = DotNetPropertyMethod.FromAssemblyInfo(propertyInfo.GetGetMethod());
			SetterMethod = DotNetPropertyMethod.FromAssemblyInfo(propertyInfo.GetSetMethod());
			TypeName = DotNetQualifiedTypeName.FromAssemblyInfo(propertyInfo.PropertyType);

			Category = FieldCategory.Normal;
			if(GetterMethod.IsAbstract && SetterMethod.IsAbstract)
				Category = FieldCategory.Abstract;
		}
	}
}
