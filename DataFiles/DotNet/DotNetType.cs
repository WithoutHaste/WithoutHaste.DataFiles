using System;
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

		private List<DotNetType> nestedTypes = new List<DotNetType>();
		private List<DotNetMethod> methods = new List<DotNetMethod>();
		private List<DotNetField> fields = new List<DotNetField>();
		private List<DotNetProperty> properties = new List<DotNetProperty>();
		private List<DotNetEvent> events = new List<DotNetEvent>();

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
			foreach(DotNetType nestedType in nestedTypes)
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
				if(member is DotNetEvent) events.Add(member as DotNetEvent);
				else if(member is DotNetProperty) properties.Add(member as DotNetProperty);
				else if(member is DotNetField) fields.Add(member as DotNetField);
				else if(member is DotNetMethod) methods.Add(member as DotNetMethod);
				else if(member is DotNetType) nestedTypes.Add(member as DotNetType);
				else
					throw new XmlFormatException("Member with unknown category added to type: " + member.Name.FullName);
				return;
			}
			foreach(DotNetType nestedType in nestedTypes)
			{
				if(nestedType.Owns(member))
				{
					nestedType.AddMember(member);
				}
			}
			throw new XmlFormatException("Member has no parent type: " + member.Name.FullName);
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
