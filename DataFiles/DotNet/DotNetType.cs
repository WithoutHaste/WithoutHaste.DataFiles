using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary></summary>
	public enum AccessModifier {
		/// <summary>Not enough information is available to determine access modifier.</summary>
		Unknown = 0,
		/// <summary></summary>
		Public,
		/// <summary></summary>
		Protected,
		/// <summary></summary>
		Private
	};

	/// <summary></summary>
	public enum TypeCategory {
		/// <summary>Not enough information is available to determine type category.</summary>
		Unknown = 0,
		/// <summary>No special category.</summary>
		Normal,
		/// <summary>Abstract type.</summary>
		Abstract,
		/// <summary>Static type.</summary>
		Static,
		/// <summary>Interface.</summary>
		Interface,
		/// <summary>Enumeration.</summary>
		Enum,
		/// <summary>Exception.</summary>
		Exception
	};

	/// <summary>
	/// Represents a data type.
	/// </summary>
	public class DotNetType : DotNetMember
	{
		/// <summary></summary>
		public TypeCategory Category { get; protected set; }

		/// <summary>Base type this type inherits from. Null if not known or none exists.</summary>
		public DotNetBaseType BaseType { get; protected set; }

		/// <summary>Interfaces this type inherits from, if known</summary>
		/// <remarks>If an interface extends another interface, reflection reports that the type implements both interfaces.</remarks>
		public List<DotNetBaseType> ImplementedInterfaces = new List<DotNetBaseType>();

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
		/// <summary>The subset of NestedTypes that are enums.</summary>
		public List<DotNetType> NestedEnums {
			get {
				return NestedTypes.Where(x => x.Category == TypeCategory.Enum).ToList();
			}
		}

		/// <summary></summary>
		public List<DotNetMethod> Methods = new List<DotNetMethod>();
		/// <summary>The subset of Methods that are constructors.</summary>
		public List<DotNetMethodConstructor> ConstructorMethods { get { return Methods.OfType<DotNetMethodConstructor>().ToList(); } }
		/// <summary>The subset of Methods that are operator overloads.</summary>
		public List<DotNetMethodOperator> OperatorMethods { get { return Methods.OfType<DotNetMethodOperator>().ToList(); } }
		/// <summary>The subset of Methods that are static, but not constructors nor operators.</summary>
		public List<DotNetMethod> StaticMethods { get { return Methods.Where(m => m.Category == MethodCategory.Static && !(m is DotNetMethodConstructor) && !(m is DotNetMethodOperator)).ToList(); } }
		/// <summary>The subset of Methods that are not static, nor constructors, nor operators.</summary>
		public List<DotNetMethod> NormalMethods { get { return Methods.Where(m => (m.Category == MethodCategory.Normal || m.Category == MethodCategory.Abstract) && !(m is DotNetMethodConstructor) && !(m is DotNetMethodOperator)).ToList(); } }

		/// <summary></summary>
		public List<DotNetField> Fields = new List<DotNetField>();
		/// <summary>The subset of Fields that are constants.</summary>
		public List<DotNetField> ConstantFields {
			get {
				return Fields.Where(x => x.Category == FieldCategory.Constant).ToList();
			}
		}
		/// <summary>The subset of Fields that are not constant, or where the category is unknown.</summary>
		public List<DotNetField> NormalFields {
			get {
				return Fields.Where(x => x.Category != FieldCategory.Constant).ToList();
			}
		}

		/// <summary></summary>
		public List<DotNetProperty> Properties = new List<DotNetProperty>();
		/// <summary>The subset of Properties that are indexers.</summary>
		public List<DotNetIndexer> IndexerProperties { get { return Properties.OfType<DotNetIndexer>().Cast<DotNetIndexer>().ToList(); } }
		/// <summary>The subset of Properties that are not indexers.</summary>
		public List<DotNetProperty> NormalProperties { get { return Properties.Where(p => !(p is DotNetIndexer)).ToList(); } }

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
		}

		/// <summary>
		/// Parse .Net XML documentation for Type data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		public static DotNetType FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromVisualStudioXml(memberElement.Attribute("name")?.Value);
			DotNetType type = new DotNetType(name);
			type.ParseVisualStudioXmlDocumentation(memberElement);
			return type;
		}

		#endregion

		/// <summary>
		/// Returns true if this member's name matches the provided name.
		/// </summary>
		public bool Is(DotNetQualifiedName name)
		{
			return (Name.FullName == name.FullName);
		}

		/// <summary>
		/// Returns true if this member is defined within this type or any of its nested types.
		/// </summary>
		public bool Owns(DotNetMember member)
		{
			return Owns(member.Name);
		}

		/// <summary>
		/// Returns true if this qualified name is defined within this type or any of its nested types.
		/// </summary>
		public bool Owns(DotNetQualifiedName name)
		{
			if(Name.FullName == name.FullNamespace)
				return true;
			foreach(DotNetType nestedType in NestedTypes)
			{
				if(nestedType.Owns(name))
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
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(TypeInfo typeInfo, DotNetQualifiedName name)
		{
			if(this.Is(name))
			{
				AddAssemblyInfo(typeInfo);
				return;
			}
			foreach(DotNetType nestedType in NestedTypes)
			{
				if(nestedType.Is(name) || nestedType.Owns(name))
				{
					nestedType.AddAssemblyInfo(typeInfo, name);
					return;
				}
			}
			//no error if type is not found
		}

		private void AddAssemblyInfo(TypeInfo typeInfo)
		{
			if(typeInfo.Attributes.IsAbstract())
				Category = TypeCategory.Abstract;
			if(typeInfo.Attributes.IsStatic())
				Category = TypeCategory.Static;
			if(typeInfo.Attributes.IsInterface())
				Category = TypeCategory.Interface;
			if(typeInfo.IsEnum())
				Category = TypeCategory.Enum;
			if(typeInfo.IsException())
				Category = TypeCategory.Exception;

			if(Category == TypeCategory.Unknown)
				Category = TypeCategory.Normal;

			if(typeInfo.BaseType != null)
			{
				BaseType = new DotNetBaseType(typeInfo.BaseType);
			}
			if(typeInfo.ImplementedInterfaces != null)
			{
				foreach(Type interfaceType in typeInfo.ImplementedInterfaces)
				{
					ImplementedInterfaces.Add(new DotNetBaseType(interfaceType));
				}
			}
			
			foreach(FieldInfo fieldInfo in typeInfo.DeclaredFields)
			{
				DotNetField field = Fields.FirstOrDefault(f => fieldInfo.Name == f.Name.LocalName);
				if(field == null)
					continue;
				field.AddAssemblyInfo(fieldInfo);
			}
			foreach(PropertyInfo propertyInfo in typeInfo.DeclaredProperties.Where(x => x.GetMethod.GetParameters().Count() == 0))
			{
				DotNetProperty property = Properties.FirstOrDefault(p => propertyInfo.Name == p.Name.LocalName);
				if(property == null)
					continue;
				property.AddAssemblyInfo(propertyInfo);
			}
			foreach(PropertyInfo propertyInfo in typeInfo.DeclaredProperties.Where(x => x.GetMethod.GetParameters().Count() > 0))
			{
				DotNetIndexer indexer = Properties.OfType<DotNetIndexer>().Cast<DotNetIndexer>().FirstOrDefault(i => i.MatchesSignature(propertyInfo.GetGetMethod()));
				if(indexer == null)
					continue;
				indexer.AddAssemblyInfo(propertyInfo);
			}
			foreach(MethodInfo methodInfo in typeInfo.DeclaredMethods)
			{
				DotNetMethod method = Methods.FirstOrDefault(m => m.MatchesSignature(methodInfo));
				if(method == null) continue;

				if(methodInfo.Attributes.IsPrivate())
				{
					Methods.Remove(method);
					continue;
				}

				method.AddAssemblyInfo(methodInfo);
			}
			foreach(ConstructorInfo constructorInfo in typeInfo.DeclaredConstructors)
			{
				DotNetMethodConstructor method = Methods.OfType<DotNetMethodConstructor>().Cast<DotNetMethodConstructor>().FirstOrDefault(m => m.MatchesArguments(constructorInfo.GetParameters()));
				if(method == null)
					continue;
				method.AddAssemblyInfo(constructorInfo);
			}
			foreach(EventInfo eventInfo in typeInfo.DeclaredEvents)
			{
				DotNetEvent e = Events.FirstOrDefault(m => m.Name.LocalName == eventInfo.Name);
				if(e == null)
					continue;
				e.AddAssemblyInfo(eventInfo);
			}

			foreach(TypeInfo nestedTypeInfo in typeInfo.DeclaredNestedTypes)
			{
				DotNetQualifiedName qualifiedName = DotNetQualifiedName.FromAssemblyInfo(nestedTypeInfo);
				DotNetType nestedType = NestedTypes.FirstOrDefault(x => x.Owns(qualifiedName));
				if(nestedType == null)
					continue;
				nestedType.AddAssemblyInfo(typeInfo, qualifiedName);
			}
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
