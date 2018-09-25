using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using WithoutHaste.Libraries;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a .Net XML documentation file, such as those produced by Visual Studio.
	/// </summary>
	public class DotNetDocumentationFile
	{
		/// <summary>Accepted .Net XML documentation file extensions.</summary>
		public readonly string[] Extensions = new string[] { ".xml", ".XML" };

		/// <summary></summary>
		public string AssemblyName { get; protected set; }

		/// <summary>Returns the full count of types within assembly, including nested types and enums.</summary>
		public int TypeCount {
			get {
				if(Types.Count == 0)
					return 0;
				return Types.Sum(t => 1 + t.NestedTypeCount);
			}
		}

		/// <summary>Top-level types in assembly.</summary>
		public List<DotNetType> Types = new List<DotNetType>();

		/// <summary>Returns the i-th top-level type.</summary>
		/// <param name="i">0-based index of top-level types.</param>
		/// <exception cref="IndexOutOfRangeException">Top-level type index is out of range.</exception>
		public DotNetType this[int i] {
			get {
				if(i < 0 || i >= Types.Count)
				{
					if(Types.Count == 0)
						throw new IndexOutOfRangeException("Top-level type index is out of range: collection is empty.");
					else
						throw new IndexOutOfRangeException(String.Format("Top-level type index is out of range [0,{0}]: index {1}.", Types.Count - 1, i));
				}
				return Types[i];
			}
		}

		#region Constructors and Init

		/// <summary>
		/// Loads .Net XML documentation from file.
		/// </summary>
		/// <param name="filename">Full path, filename, and extension.</param>
		/// <exception cref="ArgumentException">Filename is null.</exception>
		/// <exception cref="FileExtensionException">Unexpected file extension.</exception>
		public DotNetDocumentationFile(string filename)
		{
			FileInfo.ValidateExtension(filename, Extensions);
			XDocument document = XDocument.Load(filename);
			Load(document);
		}

		/// <summary>
		/// Loads .Net XML documentation from XDocument.
		/// </summary>
		public DotNetDocumentationFile(XDocument document)
		{
			Load(document);
		}

		private void Load(XDocument document)
		{
			LoadAssemblyInfo(document);
			LoadMembersInfo(document);
		}

		#endregion

		private void LoadAssemblyInfo(XDocument document)
		{
			XElement assemblyElement = document.Root.Elements("assembly").FirstOrDefault();
			if(assemblyElement == null) return;

			XElement nameElement = assemblyElement.Elements("name").FirstOrDefault();
			if(nameElement == null) return;

			AssemblyName = nameElement.Value;
		}

		private void LoadMembersInfo(XDocument document)
		{
			XElement membersElement = document.Root.Elements("members").FirstOrDefault();
			if(membersElement == null) return;

			foreach(XElement memberElement in membersElement.Elements("member"))
			{
				XAttribute nameAttribute = memberElement.Attribute("name");
				if(nameAttribute == null) continue;

				switch(nameAttribute.Value.Substring(0, 2))
				{
					case "T:":
						DotNetType type = DotNetType.FromVisualStudioXml(memberElement);
						type.ParseVisualStudioXmlDocumentation(memberElement);
						if(IsNestedType(type)) AddMemberToType(type);
						else Types.Add(type);
						break;

					case "M:":
						DotNetMethod method = DotNetMethod.FromVisualStudioXml(memberElement);
						method.ParseVisualStudioXmlDocumentation(memberElement);
						AddMemberToType(method);
						break;

					case "F:":
						DotNetField field = DotNetField.FromVisualStudioXml(memberElement);
						field.ParseVisualStudioXmlDocumentation(memberElement);
						AddMemberToType(field);
						break;

					case "P:":
						DotNetProperty property = DotNetProperty.FromVisualStudioXml(memberElement);
						property.ParseVisualStudioXmlDocumentation(memberElement);
						AddMemberToType(property);
						break;

					case "E:":
						DotNetEvent eventMember = DotNetEvent.FromVisualStudioXml(memberElement);
						eventMember.ParseVisualStudioXmlDocumentation(memberElement);
						AddMemberToType(eventMember);
						break;
				}
			}
		}

		private bool IsNestedType(DotNetType type)
		{
			return Types.Any(t => t.Owns(type));
		}

		private void AddMemberToType(DotNetMember member)
		{
			foreach(DotNetType parentType in Types)
			{
				if(parentType.Owns(member))
				{
					parentType.AddMember(member);
					return;
				}
			}
			throw new XmlFormatException("Member has no parent type: " + member.Name.FullName);
		}

	}
}
