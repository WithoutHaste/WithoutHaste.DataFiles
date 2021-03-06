﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a type's property.
	/// </summary>
	public class DotNetProperty : DotNetField
	{
		/// <summary>The "get" method of the property.</summary>
		public DotNetPropertyMethod GetterMethod { get; protected set; }

		/// <summary>The "set" method of the property.</summary>
		public DotNetPropertyMethod SetterMethod { get; protected set; }

		/// <summary></summary>
		public bool HasGetterMethod { get { return (GetterMethod != null); } }

		/// <summary></summary>
		public bool HasSetterMethod { get { return (SetterMethod != null); } }

		#region Constructors

		/// <summary></summary>
		public DotNetProperty(DotNetQualifiedName name) : base(name)
		{
		}

		/// <summary>
		/// Parse .Net XML documentation for Property data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		/// <example><![CDATA[<member name="P:Namespace.Type.PropertyName"></member>]]></example>
		public static new DotNetProperty FromVisualStudioXml(XElement memberElement)
		{
			string xmlName = memberElement.GetAttributeValue("name");
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
			GetterMethod = DotNetPropertyMethod.FromAssemblyInfo(propertyInfo.GetGetMethod(true)); //true means also return non-public methods
			SetterMethod = DotNetPropertyMethod.FromAssemblyInfo(propertyInfo.GetSetMethod(true));
			TypeName = DotNetQualifiedTypeName.FromAssemblyInfo(propertyInfo.PropertyType);

			Category = FieldCategory.Normal;
			if(HasGetterMethod && GetterMethod.IsAbstract && 
				HasSetterMethod && SetterMethod.IsAbstract)
				Category = FieldCategory.Abstract;
		}
	}
}
