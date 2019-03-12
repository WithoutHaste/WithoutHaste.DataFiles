using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a .Net XML documentation file, such as those produced by Visual Studio.
	/// Can add additional documentation derived from the assembly itself.
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
		/// <exception cref="ArgumentException">Unexpected file extension of the *.XML documentation file.</exception>
		public DotNetDocumentationFile(string filename)
		{
			if(!Extensions.Contains(Path.GetExtension(filename)))
				throw new ArgumentException("Unexpected file extension. Use one of these extensions: " + String.Join(", ", Extensions));

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
			ResolveDuplicatedComments();
			ResolveInheritedComments();
		}

#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		/// <param name="assemblyFilename">Full path and filename of the *.dll library being documentated.</param>
		/// <param name="thirdPartyAssemblyFilenames">
		/// List of third-party libraries referenced by your library.
		/// These libraries will not be documented, but they must be loaded if you want to see the full type names for return types and parameter types from these libraries.
		/// Each item in the list should be the full path and filename of a library.
		/// <example>
		/// To document the return type of <c>public Company.SomeType MyMethod() {}</c>, the library for <c>Company.SomeType</c> must be loaded.
		/// </example>
		/// </param>
		public void AddAssemblyInfo(string assemblyFilename, params string[] thirdPartyAssemblyFilenames)
		{
			if(thirdPartyAssemblyFilenames != null)
			{
				foreach(string thirdParty in thirdPartyAssemblyFilenames)
				{
					Assembly.LoadFrom(thirdParty);
				}
			}

			Assembly assembly = Assembly.LoadFrom(assemblyFilename);
			foreach(Type type in assembly.GetTypes())
			{
				AddAssemblyInfoToType(type);
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

		private void AddAssemblyInfoToType(Type type)
		{
			DotNetQualifiedName qualifiedName = DotNetQualifiedName.FromAssemblyInfo(type);
			DotNetType dotNetType = Types.FirstOrDefault(x => x.Is(qualifiedName) || x.Owns(qualifiedName));
			if(dotNetType == null)
				return; //no error if type is not found

			if(type.IsDelegate())
			{
				ConvertTypeToDelegate(type, qualifiedName, dotNetType);
				return;
			}

			dotNetType.AddAssemblyInfo(type, qualifiedName);
		}

		private void ConvertTypeToDelegate(Type type, DotNetQualifiedName qualifiedName, DotNetType dotNetType)
		{
			DotNetDelegate _delegate = dotNetType.ToDelegate(qualifiedName);
			if(dotNetType.Is(qualifiedName))
			{
				Types.Remove(dotNetType);
				Delegates.Add(_delegate);
			}
			_delegate.AddAssemblyInfo(type);
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
