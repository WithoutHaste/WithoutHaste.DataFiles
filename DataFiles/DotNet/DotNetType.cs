﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>Privacy/access modifiers.</summary>
	public enum AccessModifier {
		/// <summary>Not enough information is available to determine access modifier.</summary>
		Unknown = 0,
		/// <summary></summary>
		Public,
		/// <summary></summary>
		Protected,
		/// <summary></summary>
		Internal,
		/// <summary></summary>
		InternalProtected,
		/// <summary></summary>
		Private
	};

	/// <summary>Categories of data types for classes, interfaces, structs, and enums.</summary>
	public enum TypeCategory {
		/// <summary>Not enough information is available to determine type category.</summary>
		Unknown = 0,
		/// <summary>No special category.</summary>
		Normal,
		/// <summary></summary>
		Abstract,
		/// <summary></summary>
		Static,
		/// <summary></summary>
		Interface,
		/// <summary></summary>
		Struct,
		/// <summary></summary>
		Enum,
		/// <summary></summary>
		Exception,
	};

	/// <summary>
	/// Represents a data type: a class, interface, struct, or enum.
	/// </summary>
	public class DotNetType : DotNetMember
	{
		/// <summary></summary>
		public TypeCategory Category { get; protected set; }

		/// <summary>Strongly-typed name.</summary>
		public DotNetQualifiedClassName ClassName { get { return (Name as DotNetQualifiedClassName); } }

		/// <summary>True if the type is sealed.</summary>
		/// <remarks>Abstract classes, static classes, and interfaces cannot be sealed. Exceptions can be sealed.</remarks>
		public bool IsSealed { get; protected set; }

		/// <summary>Base type this type inherits from. Null if not known or none exists.</summary>
		public DotNetBaseType BaseType { get; protected set; }

		/// <summary>Interfaces this type implements, if known.</summary>
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

		/// <summary>Data types defined/nested within this type.</summary>
		public List<DotNetType> NestedTypes = new List<DotNetType>();
		/// <summary>The subset of NestedTypes that are enums.</summary>
		public List<DotNetType> NestedEnums {
			get {
				return NestedTypes.Where(x => x.Category == TypeCategory.Enum).ToList();
			}
		}

		/// <summary>Delegates defined within this type.</summary>
		public List<DotNetDelegate> Delegates = new List<DotNetDelegate>();

		/// <summary>All methods defined within this type.</summary>
		public List<DotNetMethod> Methods = new List<DotNetMethod>();
		/// <summary>The subset of Methods that are constructors.</summary>
		public List<DotNetMethodConstructor> ConstructorMethods { get { return Methods.OfType<DotNetMethodConstructor>().ToList(); } }
		/// <summary>The subset of Methods that are destructors. There can be zero or one destructors.</summary>
		public DotNetMethodDestructor DestructorMethod { get { return Methods.OfType<DotNetMethodDestructor>().FirstOrDefault(); } }
		/// <summary>The subset of Methods that are operator overloads.</summary>
		public List<DotNetMethodOperator> OperatorMethods { get { return Methods.OfType<DotNetMethodOperator>().ToList(); } }
		/// <summary>The subset of Methods that are static (including extension methods), but not constructors, nor destructors, nor operators.</summary>
		public List<DotNetMethod> StaticMethods { get { return Methods.Where(m => (m.Category == MethodCategory.Static || m.Category == MethodCategory.Extension) && !(m is DotNetMethodConstructor) && !(m is DotNetMethodDestructor) && !(m is DotNetMethodOperator)).ToList(); } }
		/// <summary>The subset of Methods that are not static, nor constructors, nor destructors, nor operators.</summary>
		public List<DotNetMethod> NormalMethods { get { return Methods.Where(m => (m.Category != MethodCategory.Static) && (m.Category != MethodCategory.Extension) && !(m is DotNetMethodConstructor) && !(m is DotNetMethodDestructor) && !(m is DotNetMethodOperator)).ToList(); } }

		/// <summary>All fields defined within this type.</summary>
		/// <remarks>By the .Net definition of "field", meaning that properties and events are not included.</remarks>
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

		/// <summary>All properties defined within the type.</summary>
		public List<DotNetProperty> Properties = new List<DotNetProperty>();
		/// <summary>The subset of Properties that are indexers.</summary>
		public List<DotNetIndexer> IndexerProperties { get { return Properties.OfType<DotNetIndexer>().Cast<DotNetIndexer>().ToList(); } }
		/// <summary>The subset of Properties that are not indexers.</summary>
		public List<DotNetProperty> NormalProperties { get { return Properties.Where(p => !(p is DotNetIndexer)).ToList(); } }

		/// <summary>All events defined within the type.</summary>
		public List<DotNetEvent> Events = new List<DotNetEvent>();

		/// <summary>
		/// Lists all methods, delegates, fields, properties, and events.
		/// Does not include nested types.
		/// </summary>
		public List<DotNetMember> AllMembers {
			get {
				List<DotNetMember> members = new List<DotNetMember>();
				//because LINQlone does not support members.AddRange(Methods), etc
				foreach(DotNetMember method in Methods)
				{
					members.Add(method);
				}
				foreach(DotNetMember field in Fields)
				{
					members.Add(field);
				}
				foreach(DotNetMember property in Properties)
				{
					members.Add(property);
				}
				foreach(DotNetMember _event in Events)
				{
					members.Add(_event);
				}
				foreach(DotNetMember _delegate in Delegates)
				{
					members.Add(_delegate);
				}
				return members;
			}
		}

		#region Constructors

		/// <summary></summary>
		public DotNetType(DotNetQualifiedClassName name) : base(name)
		{
			Category = TypeCategory.Unknown;
			IsSealed = false;
		}

		/// <summary>
		/// Parse .Net XML documentation for Type data.
		/// </summary>
		/// <param name="memberElement">Expects tag name "member".</param>
		/// <example><![CDATA[<member name="T:Namespace.Type"></member>]]></example>
		public static DotNetType FromVisualStudioXml(XElement memberElement)
		{
			DotNetQualifiedClassName name = DotNetQualifiedClassName.FromVisualStudioXml(memberElement.GetAttributeValue("name"));
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
		/// Returns true if this member is defined within this type or any of its nested type descendents.
		/// </summary>
		public bool Owns(DotNetMember member)
		{
			return Owns(member.Name);
		}

		/// <summary>
		/// Returns true if this qualified name is defined within this type or any of its nested type dscendents.
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
		/// Returns true if this qualified name is defined directly within this type.
		/// </summary>
		public bool IsDirectChild(DotNetQualifiedName name)
		{
			return (Name.FullName == name.FullNamespace);
		}

		/// <summary>
		/// Returns the selected field/property/event/method/delegate/type from this type.
		/// </summary>
		private DotNetMember GetDirectChild(DotNetQualifiedName name)
		{
			if(name is DotNetQualifiedMethodName)
			{
				foreach(DotNetMethod method in Methods)
				{
					if(method.MatchesSignature(name as DotNetQualifiedMethodName))
						return method;
				}
				foreach(DotNetDelegate _delegate in Delegates)
				{
					if(_delegate.MatchesSignature(name as DotNetQualifiedMethodName))
						return _delegate;
				}
			}
			foreach(DotNetField field in Fields)
			{
				if(field.Name.LocalName == name.LocalName)
					return field;
			}
			foreach(DotNetField property in Properties)
			{
				if(property.Name.LocalName == name.LocalName)
					return property;
			}
			foreach(DotNetField _event in Events)
			{
				if(_event.Name.LocalName == name.LocalName)
					return _event;
			}
			foreach(DotNetType nestedType in NestedTypes)
			{
				if(nestedType.Is(name))
					return nestedType;
			}
			return null;
		}

		/// <summary>
		/// Returns the selected type, whether it is the current type or one of its nested type descendents. Returns null if the type is not found.
		/// </summary>
		public DotNetType FindType(DotNetQualifiedName name)
		{
			if(this.Is(name))
				return this;
			DotNetType type = NestedTypes.FirstOrDefault(x => x.Is(name) || x.Owns(name));
			if(type == null)
				return null;
			return type.FindType(name);
		}

		/// <summary>
		/// Returns the selected field, if it exists in this type.
		/// </summary>
		/// <param name="FindType">Function that returns the selected type from all known types in the assembly.</param>
		/// <param name="localName">Name of field, local to this type.</param>
		public DotNetField FindInheritedField(Func<DotNetQualifiedName, DotNetType> FindType, string localName)
		{
			DotNetField field = Fields.FirstOrDefault(f => f.Name.LocalName == localName);
			if(field != null)
				return field;
			if(BaseType != null)
			{
				DotNetType baseType = FindType(BaseType.Name);
				if(baseType != null)
				{
					return baseType.FindInheritedField(FindType, localName);
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the selected property, if it exists in this type.
		/// </summary>
		/// <param name="FindType">Function that returns the selected type from all known types in the assembly.</param>
		/// <param name="localName">Name of property, local to this type.</param>
		public DotNetProperty FindInheritedProperty(Func<DotNetQualifiedName, DotNetType> FindType, string localName)
		{
			DotNetProperty property = Properties.FirstOrDefault(p => p.Name.LocalName == localName);
			if(property != null)
				return property;
			if(BaseType != null)
			{
				DotNetType baseType = FindType(BaseType.Name);
				if(baseType != null)
				{
					return baseType.FindInheritedProperty(FindType, localName);
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the selected event, if it exists in this type.
		/// </summary>
		/// <param name="FindType">Function that returns the selected type from all known types in the assembly.</param>
		/// <param name="localName">Name of event, local to this type.</param>
		public DotNetEvent FindInheritedEvent(Func<DotNetQualifiedName, DotNetType> FindType, string localName)
		{
			DotNetEvent _event = Events.FirstOrDefault(e => e.Name.LocalName == localName);
			if(_event != null)
				return _event;
			if(BaseType != null)
			{
				DotNetType baseType = FindType(BaseType.Name);
				if(baseType != null)
				{
					return baseType.FindInheritedEvent(FindType, localName);
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the selected method, if it exists in this type.
		/// </summary>
		/// <param name="FindType">Function that returns the selected type from all known types in the assembly.</param>
		/// <param name="methodName">Name of method, local to this type.</param>
		public DotNetMethod FindInheritedMethod(Func<DotNetQualifiedName, DotNetType> FindType, DotNetQualifiedMethodName methodName)
		{
			DotNetMethod method = Methods.FirstOrDefault(m => m.MatchesLocalSignature(methodName));
			if(method != null)
				return method;
			if(BaseType != null)
			{
				DotNetType baseType = FindType(BaseType.Name);
				if(baseType != null)
				{
					return baseType.FindInheritedMethod(FindType, methodName);
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the specified member from this type of its nested type descendents. Can return a field, property, event, method, delegate, or type.
		/// </summary>
		public DotNetMember FindMember(DotNetQualifiedName name)
		{
			if(this.Is(name))
				return this;
			if(IsDirectChild(name))
				return GetDirectChild(name);
			foreach(DotNetType nestedType in NestedTypes)
			{
				if(nestedType.Is(name) || nestedType.Owns(name))
					return nestedType.FindMember(name);
			}
			return null;
		}

		/// <summary>
		/// Add a member to this type or one of its nested type descendents.
		/// </summary>
		public void AddMember(DotNetMember member)
		{
			if(Name.FullName == member.Name.FullNamespace)
			{
				if(member is DotNetEvent) Events.Add(member as DotNetEvent);
				else if(member is DotNetProperty) Properties.Add(member as DotNetProperty);
				else if(member is DotNetField) Fields.Add(member as DotNetField);
				else if(member is DotNetMethod)
				{
					Methods.Add(member as DotNetMethod);
					if(member is DotNetMethodConstructor)
					{
						(member as DotNetMethodConstructor).SetClassName(ClassName);
					}
				}
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
			//no exception if member parent is not found
		}

		/// <summary>
		/// Load additional documentation information from the assembly itself for this type or one of its nested type descendents.
		/// </summary>
		public void AddAssemblyInfo(Type type, DotNetQualifiedName name)
		{
			if(this.Is(name))
			{
				AddAssemblyInfo(type);
				return;
			}
			foreach(DotNetType nestedType in NestedTypes)
			{
				if(nestedType.Is(name) || nestedType.Owns(name))
				{
					nestedType.AddAssemblyInfo(type, name);
					return;
				}
			}
			//no error if type is not found
		}

		/// <summary>
		/// Load additional information from the assembly for this type.
		/// </summary>
		private void AddAssemblyInfo(Type type)
		{
			if(type.Attributes.IsAbstract())
				Category = TypeCategory.Abstract;
			if(type.Attributes.IsStatic())
				Category = TypeCategory.Static;
			if(type.Attributes.IsInterface())
				Category = TypeCategory.Interface;
			if(type.IsEnum())
				Category = TypeCategory.Enum;
			if(type.IsException())
				Category = TypeCategory.Exception;
			if(type.IsValueType && !type.IsEnum())
				Category = TypeCategory.Struct;

			if(Category == TypeCategory.Unknown)
				Category = TypeCategory.Normal;

			IsSealed = type.IsSealed;

			ClassName.AddAssemblyInfo(type);

			if(type.BaseType != null)
			{
				BaseType = new DotNetBaseType(type.BaseType);
			}

			Type[] implementedInterfaces = type.GetInterfaces();
			if(implementedInterfaces != null)
			{
				foreach(Type interfaceType in implementedInterfaces)
				{
					ImplementedInterfaces.Add(new DotNetBaseType(interfaceType));
				}
			}
			
			foreach(FieldInfo fieldInfo in type.GetDeclaredFields())
			{
				DotNetField field = Fields.FirstOrDefault(f => fieldInfo.Name == f.Name.LocalName);
				if(field == null)
					continue;
				field.AddAssemblyInfo(fieldInfo);
			}
			foreach(PropertyInfo propertyInfo in type.GetDeclaredProperties().Where(x => x.GetGetMethod() == null || x.GetGetMethod().GetParameters().Count() == 0))
			{
				DotNetProperty property = Properties.FirstOrDefault(p => propertyInfo.Name == DotNetQualifiedName.Combine(p.Name.ExplicitInterface, p.Name.LocalName));
				if(property == null)
					continue;
				property.AddAssemblyInfo(propertyInfo);
			}
			foreach(PropertyInfo propertyInfo in type.GetDeclaredProperties().Where(x => x.GetGetMethod() != null && x.GetGetMethod().GetParameters().Count() > 0))
			{
				DotNetIndexer indexer = Properties.OfType<DotNetIndexer>().Cast<DotNetIndexer>().FirstOrDefault(i => i.MatchesSignature(propertyInfo.GetGetMethod()));
				if(indexer == null)
					continue;
				indexer.AddAssemblyInfo(propertyInfo);
			}
			foreach(MethodInfo methodInfo in type.GetDeclaredMethods())
			{
				DotNetMethod method = Methods.FirstOrDefault(m => m.MatchesSignature(methodInfo));
				if(method == null) continue;

				if(methodInfo.Attributes.IsPrivate() && method.Name.ExplicitInterface == null)
				{
					Methods.Remove(method);
					continue;
				}

				method.AddAssemblyInfo(methodInfo);
			}
			foreach(ConstructorInfo constructorInfo in type.GetDeclaredConstructors())
			{
				DotNetMethodConstructor method = Methods.OfType<DotNetMethodConstructor>().Cast<DotNetMethodConstructor>().FirstOrDefault(m => m.MatchesArguments(constructorInfo.GetParameters()));
				if(method == null)
					continue;
				method.AddAssemblyInfo(constructorInfo);
			}
			foreach(EventInfo eventInfo in type.GetDeclaredEvents())
			{
				DotNetEvent e = Events.FirstOrDefault(m => m.Name.LocalName == eventInfo.Name);
				if(e == null)
					continue;
				e.AddAssemblyInfo(eventInfo);
			}

			foreach(Type nestedType in type.GetDeclaredNestedTypes())
			{
				DotNetQualifiedName qualifiedName = DotNetQualifiedName.FromAssemblyInfo(nestedType);
				DotNetType dotNetNestedType = NestedTypes.FirstOrDefault(x => x.Owns(qualifiedName));
				if(dotNetNestedType == null)
					continue;
				dotNetNestedType.AddAssemblyInfo(type, qualifiedName);
			}

			PushGenericTypes();
		}

		/// <summary>
		/// Push updates to generic-type aliases to all members.
		/// </summary>
		private void PushGenericTypes()
		{
			if(!ClassName.IsGeneric)
				return;
			string[] aliases = ClassName.GenericTypeAliases;
			foreach(DotNetField field in Fields)
			{
				field.PushGenericTypes(aliases);
			}
			foreach(DotNetProperty property in Properties)
			{
				property.PushGenericTypes(aliases);
			}
			foreach(DotNetEvent _event in Events)
			{
				_event.PushGenericTypes(aliases);
			}
			foreach(DotNetMethod method in Methods)
			{
				method.PushClassGenericTypes(aliases);
			}
			foreach(DotNetDelegate _delegate in Delegates)
			{
				_delegate.PushClassGenericTypes(aliases);
			}
		}

		/// <summary>
		/// Collect full list of local names used throughout documentation.
		/// Includes namespaces, internal types, external types, and members.
		/// </summary>
		/// <returns></returns>
		internal List<string> GetFullListOfLocalNames()
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
				localNames.AddRange(method.GetFullListOfLocalNames());
			}

			return localNames;
		}

		/// <summary>
		/// For all "inheritdoc" comments, replace the inheritance comment with the inherited comments.
		/// </summary>
		/// <remarks>
		/// Classes can inherit from their base class (or the base class's base, etc).
		/// Interfaces can inherit from interfaces.
		/// Class members can inherit from their base class or from interfaces.
		/// </remarks>
		/// <param name="FindType">Function that returns the selected type from all known types in the assembly.</param>
		/// <param name="inheritancePath">List of types or members inheriting from each other, from top level to bottom level. Used to avoid loops.</param>
		public void ResolveInheritedComments(Func<DotNetQualifiedName,DotNetType> FindType, List<DotNetQualifiedName> inheritancePath = null)
		{
			if(inheritancePath == null)
				inheritancePath = new List<DotNetQualifiedName>();
			if(inheritancePath.Contains(this.Name))
				return; //inheritance loop

			DotNetType baseType = null;
			if(BaseType != null)
			{
				baseType = FindType(BaseType.Name);
				if(baseType != null && baseType.InheritsDocumentation)
				{
					List<DotNetQualifiedName> copiedInheritancePath = inheritancePath.Select(x => x).ToList();
					copiedInheritancePath.Add(this.Name);
					baseType.ResolveInheritedComments(FindType, copiedInheritancePath);
				}
			}
			List<DotNetType> baseInterfaces = new List<DotNetType>();
			foreach(DotNetBaseType implementedInterface in ImplementedInterfaces)
			{
				DotNetType baseInterface = FindType(implementedInterface.Name);
				if(baseInterface != null)
				{
					baseInterfaces.Add(baseInterface);
					if(baseInterface.InheritsDocumentation)
					{
						List<DotNetQualifiedName> copiedInheritancePath = inheritancePath.Select(x => x).ToList();
						copiedInheritancePath.Add(this.Name);
						baseInterface.ResolveInheritedComments(FindType, copiedInheritancePath);
					}
				}
			}

			if(this.InheritsDocumentation && baseType != null)
			{
				this.CopyComments(baseType);
			}

			if(baseType != null)
			{
				foreach(DotNetField field in Fields)
				{
					if(!field.InheritsDocumentation) continue;

					DotNetField baseField = baseType.FindInheritedField(FindType, field.Name.LocalName);
					if(baseField != null)
						field.CopyComments(baseField);
				}
			}
			foreach(DotNetProperty property in Properties)
			{
				if(!property.InheritsDocumentation) continue;

				if(baseType != null)
				{
					DotNetProperty baseProperty = baseType.FindInheritedProperty(FindType, property.Name.LocalName);
					if(baseProperty != null)
					{
						property.CopyComments(baseProperty);
						continue;
					}
				}
				foreach(DotNetType baseInterface in baseInterfaces)
				{
					if(property.Name.ExplicitInterface != null && property.Name.ExplicitInterface != baseInterface.Name)
						continue;

					DotNetProperty baseProperty = baseInterface.FindInheritedProperty(FindType, property.Name.LocalName);
					if(baseProperty != null)
					{
						property.CopyComments(baseProperty);
						break;
					}
				}
			}
			foreach(DotNetEvent _event in Events)
			{
				if(!_event.InheritsDocumentation) continue;

				if(baseType != null)
				{
					DotNetEvent baseEvent = baseType.FindInheritedEvent(FindType, _event.Name.LocalName);
					if(baseEvent != null)
					{
						_event.CopyComments(baseEvent);
						continue;
					}
				}
			}
			foreach(DotNetMethod method in Methods)
			{
				if(!method.InheritsDocumentation) continue;

				if(baseType != null)
				{
					DotNetMethod baseMethod = baseType.FindInheritedMethod(FindType, method.MethodName);
					if(baseMethod != null)
					{
						method.CopyComments(baseMethod);
						continue;
					}
				}
				foreach(DotNetType baseInterface in baseInterfaces)
				{
					if(method.Name.ExplicitInterface != null && method.Name.ExplicitInterface != baseInterface.Name)
						continue;

					DotNetMethod baseMethod = baseInterface.FindInheritedMethod(FindType, method.MethodName);
					if(baseMethod != null)
					{
						method.CopyComments(baseMethod);
						break;
					}
				}
			}

			foreach(DotNetType nestedType in NestedTypes)
			{
				nestedType.ResolveInheritedComments(FindType);
			}
		}

		/// <summary>
		/// For all "duplicate" comments, replace the comment with the duplicated comments.
		/// </summary>
		/// <param name="FindMember">Function that returns the selected member from all known members in the assembly.</param>
		public void ResolveDuplicatedComments(Func<DotNetQualifiedName, DotNetMember> FindMember)
		{
			base.ResolveDuplicatedComments(FindMember);

			foreach(DotNetMember member in AllMembers)
			{
				member.ResolveDuplicatedComments(FindMember);
			}

			foreach(DotNetType nestedType in NestedTypes)
			{
				nestedType.ResolveDuplicatedComments(FindMember);
			}
		}

		#region Convert

		/// <summary>
		/// Converts the selected type into a delegate, transfering all applicable data.
		/// </summary>
		/// <remarks>
		/// If the <paramref name="name"/> refers to a nested type descendent, that type is the one converted.
		/// The nested type is removed from its parent and the new delegate is added in its place
		/// </remarks>
		/// <param name="name">The fully qualified name of the delegate.</param>
		/// <returns>The new delegate, or null if the type is not found.</returns>
		public DotNetDelegate ToDelegate(DotNetQualifiedName name)
		{
			if(Is(name))
			{
				return ToDelegate();
			}
			else if(IsDirectChild(name))
			{
				DotNetType subtype = (DotNetType)GetDirectChild(name);
				DotNetDelegate _delegate = subtype.ToDelegate();
				NestedTypes.Remove(subtype);
				Delegates.Add(_delegate);
				return _delegate;
			}
			else
			{
				foreach(DotNetType nestedType in NestedTypes)
				{
					if(nestedType.Owns(name))
						return nestedType.ToDelegate(name);
				}
			}
			return null;
		}

		private DotNetDelegate ToDelegate()
		{
			DotNetDelegate _delegate = new DotNetDelegate(Name);
			_delegate.CopyComments(this);
			return _delegate;
		}

		#endregion

		#region Low Level

		/// <summary>Returns FullName of type.</summary>
		public override string ToString()
		{
			return Name.FullName;
		}

		#endregion
	}
}
