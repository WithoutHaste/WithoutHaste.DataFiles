﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary></summary>
	public enum FieldCategory
	{
		/// <summary>Not enough information is available to determine field category.</summary>
		Unknown = 0,
		/// <summary>No special category.</summary>
		Normal,
		/// <summary>Constant or Readonly field.</summary>
		Constant
	};

	/// <summary>
	/// Represents a type's field.
	/// </summary>
	public class DotNetField : DotNetMember
	{
		/// <summary></summary>
		public FieldCategory Category { get; protected set; }

		/// <summary>Fully qualified name of data type, if known. Null if not known.</summary>
		public DotNetParameterBase DataTypeName { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetField(DotNetQualifiedName name) : base(name)
		{
		}

		/// <summary>
		/// Parse .Net XML documentation for Field data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		public static DotNetField FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(memberElement.Attribute("name")?.Value);
			return new DotNetField(name);
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(FieldInfo fieldInfo)
		{
			if(fieldInfo.Attributes.IsConstant())
				Category = FieldCategory.Constant;
			else
				Category = FieldCategory.Normal;
			DataTypeName = DotNetParameterBase.FromAssemblyInfo(fieldInfo.FieldType);
		}
	}
}
