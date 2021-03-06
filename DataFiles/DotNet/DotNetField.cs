﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>Categories of fields, properties, indexers, and events.</summary>
	public enum FieldCategory
	{
		/// <summary>Not enough information is available to determine field category.</summary>
		Unknown = 0,
		/// <summary>No special category.</summary>
		Normal,
		/// <summary></summary>
		Constant,
		/// <summary></summary>
		ReadOnly,
		/// <remarks>Value only valid on properties and events.</remarks>
		Abstract
	};

	/// <summary>
	/// Represents a type's field.
	/// </summary>
	public class DotNetField : DotNetMember
	{
		/// <summary></summary>
		public FieldCategory Category { get; protected set; }

		/// <summary></summary>
		public AccessModifier AccessModifier { get; protected set; } //todo: this property is not used by DotNetProperty - does that break inheritance?

		/// <summary>False means unknown or is not static.</summary>
		public bool IsStatic { get; protected set; }

		/// <summary>Fully qualified name of data type, if known. Null if not known.</summary>
		public DotNetQualifiedTypeName TypeName { get; protected set; }

		/// <summary>The "FullName" of the field data type.</summary>
		public string FullTypeName { get { return TypeName.FullName; } }

		/// <summary>For constant fields, the value of the constant. Null otherwise.</summary>
		public object ConstantValue { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetField(DotNetQualifiedName name) : base(name)
		{
		}

		/// <summary>
		/// Parse .Net XML documentation for Field data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		/// <example><![CDATA[<member name="F:Namespace.Type.FieldName"></member>]]></example>
		public static DotNetField FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(memberElement.GetAttributeValue("name"));
			DotNetField field = new DotNetField(name);
			field.ParseVisualStudioXmlDocumentation(memberElement);
			return field;
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public virtual void AddAssemblyInfo(FieldInfo fieldInfo)
		{
			if(fieldInfo.Attributes.IsConstant())
				Category = FieldCategory.Constant;
			else if(fieldInfo.IsInitOnly)
				Category = FieldCategory.ReadOnly;
			else
				Category = FieldCategory.Normal;

			if(fieldInfo.IsPublic)
				AccessModifier = AccessModifier.Public;
			else if(fieldInfo.IsFamily)
				AccessModifier = AccessModifier.Protected;
			else if(fieldInfo.IsAssembly)
				AccessModifier = AccessModifier.Internal;
			else if(fieldInfo.IsFamilyOrAssembly)
				AccessModifier = AccessModifier.InternalProtected;
			else
				AccessModifier = AccessModifier.Private;

			if(fieldInfo.IsStatic)
				IsStatic = true;

			if(Category == FieldCategory.Constant)
			{
				try
				{
					ConstantValue = fieldInfo.GetRawConstantValue();
				}
				catch(Exception)
				{
					//System.InvalidOperationException: Operation is not valid due to the current state of the object.
				}
			}

			TypeName = DotNetQualifiedTypeName.FromAssemblyInfo(fieldInfo.FieldType);
		}

		/// <summary>
		/// Update field type with the class's generic-type aliases.
		/// </summary>
		/// <param name="genericTypeAliases">Ordered list of aliases.</param>
		internal virtual void PushGenericTypes(string[] genericTypeAliases)
		{
			if(TypeName is DotNetReferenceClassGeneric)
			{
				(TypeName as DotNetReferenceClassGeneric).SetAlias(genericTypeAliases);
			}
		}
	}
}
