using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

		/// <summary>Top-level delegates in assembly.</summary>
		public List<DotNetDelegate> Delegates = new List<DotNetDelegate>();

		#region Constructors and Init

		/// <summary>
		/// Empty constructor.
		/// </summary>
		public DotNetDocumentationFile()
		{
		}

		/// <summary>
		/// Loads .Net XML documentation from file.
		/// </summary>
		/// <param name="filename">Full path, filename, and extension.</param>
		/// <exception cref="ArgumentException">Filename is null.</exception>
		/// <exception cref="FileExtensionException">Unexpected file extension.</exception>
		public DotNetDocumentationFile(string filename)
		{
			WithoutHaste.Libraries.FileInfo.ValidateExtension(filename, Extensions);
			XDocument document = XDocument.Load(filename, LoadOptions.PreserveWhitespace);
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
			LoadAssemblyInfoFromXml(document);
			LoadMembersInfoFromXml(document);
			ResolveGenericTypeNameConflicts();
			ResolveDuplicatedComments();
			ResolveInheritedComments();
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(string assemblyFilename)
		{
			Assembly assembly = Assembly.Load(File.ReadAllBytes(assemblyFilename));
			foreach(TypeInfo typeInfo in assembly.DefinedTypes)
			{
				AddAssemblyInfoToType(typeInfo);
			}

			ResolveDuplicatedComments();
			ResolveInheritedComments();
		}

		private void LoadAssemblyInfoFromXml(XDocument document)
		{
			XElement assemblyElement = document.Root.Elements("assembly").FirstOrDefault();
			if(assemblyElement == null) return;

			XElement nameElement = assemblyElement.Elements("name").FirstOrDefault();
			if(nameElement == null) return;

			AssemblyName = nameElement.Value;
		}

		private void LoadMembersInfoFromXml(XDocument document)
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
						if(IsNestedType(type)) AddMemberToType(type);
						else Types.Add(type);
						break;

					case "M:":
						DotNetMethod method = DotNetMethod.FromVisualStudioXml(memberElement);
						AddMemberToType(method);
						break;

					case "F:":
						DotNetField field = DotNetField.FromVisualStudioXml(memberElement);
						AddMemberToType(field);
						break;

					case "P:":
						DotNetProperty property = DotNetProperty.FromVisualStudioXml(memberElement);
						AddMemberToType(property);
						break;

					case "E:":
						DotNetEvent eventMember = DotNetEvent.FromVisualStudioXml(memberElement);
						AddMemberToType(eventMember);
						break;
				}
			}
		}

		/// <summary>
		/// Ensure that default generic-type names do not conflict with actual types used in assembly.
		/// </summary>
		private void ResolveGenericTypeNameConflicts()
		{
			string filler = "X";
			bool foundConflict = false;
			List<string> actualNames = GetFullListOfLocalNames();
			do
			{
				foundConflict = false;
				foreach(string actualName in actualNames)
				{
					if(DotNetQualifiedMethodName.GenericTypeNames.Contains(actualName))
					{
						foundConflict = true;
						int index = Array.IndexOf(DotNetQualifiedMethodName.GenericTypeNames, actualName);
						DotNetQualifiedMethodName.GenericTypeNames[index] = actualName + filler;
					}
					if(DotNetQualifiedClassName.GenericTypeNames.Contains(actualName))
					{
						foundConflict = true;
						int index = Array.IndexOf(DotNetQualifiedClassName.GenericTypeNames, actualName);
						DotNetQualifiedClassName.GenericTypeNames[index] = actualName + filler;
					}
				}
			} while(foundConflict); //in case the updated generic-type names now conflict with other actual type names
		}

		internal void ResolveInheritedComments()
		{
			foreach(DotNetType type in Types)
			{
				type.ResolveInheritedComments(FindType);
			}
		}

		internal void ResolveDuplicatedComments()
		{
			foreach(DotNetType type in Types)
			{
				type.ResolveDuplicatedComments(FindMember);
			}
			foreach(DotNetDelegate _delegate in Delegates)
			{
				_delegate.ResolveDuplicatedComments(FindMember);
			}
		}

		private DotNetType FindType(DotNetQualifiedName name)
		{
			DotNetType type = Types.FirstOrDefault(x => x.Is(name) || x.Owns(name));
			if(type == null) return null;

			return type.FindType(name);
		}

		private DotNetMember FindMember(DotNetQualifiedName name)
		{
			DotNetType type = Types.FirstOrDefault(x => x.Is(name) || x.Owns(name));
			if(type == null)
			{
				return Delegates.FirstOrDefault(x => x.Is(name));
			}
			return type.FindMember(name);
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
			//no exception if type not found, member is ignored
		}

		private void AddAssemblyInfoToType(TypeInfo typeInfo)
		{
			DotNetQualifiedName qualifiedName = DotNetQualifiedName.FromAssemblyInfo(typeInfo);
			DotNetType type = Types.FirstOrDefault(x => x.Is(qualifiedName) || x.Owns(qualifiedName));
			if(type == null)
				return; //no error if type is not found

			if(typeInfo.IsDelegate())
			{
				ConvertTypeToDelegate(typeInfo, qualifiedName, type);
				return;
			}

			type.AddAssemblyInfo(typeInfo, qualifiedName);
		}

		private void ConvertTypeToDelegate(TypeInfo typeInfo, DotNetQualifiedName qualifiedName, DotNetType type)
		{
			DotNetDelegate _delegate = type.ToDelegate(qualifiedName);
			if(type.Is(qualifiedName))
			{
				Types.Remove(type);
				Delegates.Add(_delegate);
			}
			_delegate.AddAssemblyInfo(typeInfo);
		}

		/// <summary>
		/// Collect full list of local names used throughout documentation.
		/// Includes namespaces, internal types, external types, and members.
		/// </summary>
		/// <returns></returns>
		private List<string> GetFullListOfLocalNames()
		{
			List<string> localNames = new List<string>();

			localNames.Add(AssemblyName);
			foreach(DotNetType type in Types)
			{
				localNames.AddRange(type.GetFullListOfLocalNames());
			}

			return localNames;
		}

	}
}
