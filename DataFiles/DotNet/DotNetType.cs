﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a data type.
	/// </summary>
	public class DotNetType : DotNetMember
	{
		/// <summary></summary>
		public bool IsException { get; protected set; }

		/// <summary>The number of types nested within this type, including sub-nested types and enums.</summary>
		public int NestedTypeCount {
			get {
				if(NestedTypes.Count == 0)
					return 0;
				return NestedTypes.Sum(t => 1 + t.NestedTypeCount);
			}
		}

		/// <summary></summary>
		public List<DotNetType> NestedTypes = new List<DotNetType>();

		/// <summary></summary>
		public List<DotNetMethod> Methods = new List<DotNetMethod>();

		/// <summary></summary>
		public List<DotNetField> Fields = new List<DotNetField>();

		/// <summary></summary>
		public List<DotNetProperty> Properties = new List<DotNetProperty>();

		/// <summary></summary>
		public List<DotNetEvent> Events = new List<DotNetEvent>();

		/// <summary>
		/// Lists all methods, fields, properties, and events.
		/// Does not include nested types.
		/// </summary>
		public List<DotNetMember> AllMembers {
			get {
				List<DotNetMember> members = new List<DotNetMember>();
				members.AddRange(Methods);
				members.AddRange(Fields);
				members.AddRange(Properties);
				members.AddRange(Events);
				return members;
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetType(DotNetQualifiedName name) : base(name)
		{
			IsException = name.LocalName.EndsWith("Exception"); //todo: can I check inheritance instead?
		}

		/// <summary>
		/// Parse .Net XML documentation for Type data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		public static DotNetType FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(memberElement.Attribute("name")?.Value);
			return new DotNetType(name);
		}

		#endregion

		/// <summary>
		/// Returns true if this member is defined within this type or any of its nested types.
		/// </summary>
		public bool Owns(DotNetMember member)
		{
			if(Name.FullName == member.Name.FullNamespace)
				return true;
			foreach(DotNetType nestedType in NestedTypes)
			{
				if(nestedType.Owns(member))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Add a member to the correct level within this type.
		/// </summary>
		public void AddMember(DotNetMember member)
		{
			if(Name.FullName == member.Name.FullNamespace)
			{
				if(member is DotNetEvent) Events.Add(member as DotNetEvent);
				else if(member is DotNetProperty) Properties.Add(member as DotNetProperty);
				else if(member is DotNetField) Fields.Add(member as DotNetField);
				else if(member is DotNetMethod) Methods.Add(member as DotNetMethod);
				else if(member is DotNetType) NestedTypes.Add(member as DotNetType);
				else
					throw new XmlFormatException("Member with unknown category added to type: " + member.Name.FullName);
				return;
			}
			foreach(DotNetType nestedType in NestedTypes)
			{
				if(nestedType.Owns(member))
				{
					nestedType.AddMember(member);
					return;
				}
			}
			throw new XmlFormatException("Member has no parent type: " + member.Name.FullName);
		}

		/// <summary>
		/// Collect full list of local names used throughout documentation.
		/// Includes namespaces, internal types, external types, and members.
		/// </summary>
		/// <returns></returns>
		public List<string> GetFullListOfLocalNames()
		{
			List<string> localNames = new List<string>();

			localNames.AddRange(Name.GetFullListOfLocalNames());
			foreach(DotNetType type in NestedTypes)
			{
				localNames.AddRange(type.GetFullListOfLocalNames());
			}
			foreach(DotNetMember member in AllMembers)
			{
				localNames.AddRange(member.Name.GetFullListOfLocalNames());
			}
			foreach(DotNetMethod method in Methods)
			{
				foreach(DotNetParameter parameter in method.Parameters.OfType<DotNetParameter>().Cast<DotNetParameter>())
				{
					localNames.AddRange(parameter.TypeName.GetFullListOfLocalNames());
				}
			}

			return localNames;
		}

		#region Low Level

		/// <summary></summary>
		public override string ToString()
		{
			return Name.FullName;
		}

		#endregion
	}
}