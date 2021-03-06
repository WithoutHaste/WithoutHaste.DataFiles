﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a fully qualified type name, for return types / field types / property types / parameter types.
	/// </summary>
	public class DotNetQualifiedTypeName : DotNetQualifiedName
	{
		/// <summary>
		/// Strongly-typed FullNamespace.
		/// </summary>
		public DotNetQualifiedTypeName FullTypeNamespace {
			get {
				if(FullNamespace == null)
					return null;
				return (FullNamespace as DotNetQualifiedTypeName);
			}
		}

		/// <summary>Local data type name with generic type parameters (if applicable).</summary>
		public override string LocalName {
			get {
				if(GenericTypeParameters.Count == 0)
					return localName;
				return String.Format("{0}<{1}>", localName, String.Join(",", GenericTypeParameters.Select(x => x.FullName).ToArray()));
			}
		}

		/// <summary>If this is a generic type, these are the specified parameter types.</summary>
		/// <example>In parameter type <![CDATA[List<System.String>]]>, System.String is the generic-type parameter of List.</example>
		public List<DotNetQualifiedTypeName> GenericTypeParameters = new List<DotNetQualifiedTypeName>();

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedTypeName() : base()
		{
		}

		/// <summary></summary>
		public DotNetQualifiedTypeName(string localName)
		{
			this.localName = localName;
		}

		/// <summary></summary>
		public DotNetQualifiedTypeName(string localName, DotNetQualifiedTypeName fullNamespace)
		{
			this.localName = localName;
			this.FullNamespace = fullNamespace;
		}

		/// <summary></summary>
		public DotNetQualifiedTypeName(string localName, List<DotNetQualifiedTypeName> genericTypeParameters) : this(localName, genericTypeParameters, null)
		{
		}

		/// <param name="localName"></param>
		/// <param name="genericTypeParameters">List of generic-type parameters within this type.</param>
		/// <param name="fullNamespace"></param>
		/// <exception cref="ArgumentException"><paramref name="genericTypeParameters"/> cannot be null.</exception>
		public DotNetQualifiedTypeName(string localName, List<DotNetQualifiedTypeName> genericTypeParameters, DotNetQualifiedTypeName fullNamespace)
		{
			if(genericTypeParameters == null)
				throw new ArgumentException("GenericTypeParameters list cannot be null.", "genericTypeParameters");
			this.localName = localName;
			this.FullNamespace = fullNamespace;
			GenericTypeParameters.AddRange(genericTypeParameters);
		}
		
		/// <summary>
		/// Parses a .Net XML documentation type name.
		/// Not intended for type declarations. Intended for field types, property types, parameter types, and return types.
		/// </summary>
		public static new DotNetQualifiedTypeName FromVisualStudioXml(string typeName)
		{
			if(String.IsNullOrEmpty(typeName)) return new DotNetQualifiedTypeName();

			if(DotNetReferenceClassGeneric.HasExpectedVisualStudioXmlFormat(typeName))
				return DotNetReferenceClassGeneric.FromVisualStudioXml(typeName);

			if(DotNetReferenceMethodGeneric.HasExpectedVisualStudioXmlFormat(typeName))
				return DotNetReferenceMethodGeneric.FromVisualStudioXml(typeName);

			string[] dotDelimited = typeName.SplitIgnoreNested('.');
			string localName = dotDelimited.Last();
			string fullNamespace = (dotDelimited.Length == 1) ? null : String.Join(".", dotDelimited.Take(dotDelimited.Length - 1).ToArray());

			//fully qualified generic type parameters
			//such as System.Collections.Generic.List<T> which are formatted as System.Collections.Generic.List{`0}
			List<DotNetQualifiedTypeName> parameters = new List<DotNetQualifiedTypeName>();
			if(localName.EndsWith("}"))
			{
				string parameterSignature = localName.Substring(localName.IndexOf("{")).RemoveOuterBraces();
				localName = localName.Substring(0, localName.IndexOf("{"));
				parameters = parameterSignature.SplitIgnoreNested(',').Select(p => DotNetQualifiedTypeName.FromVisualStudioXml(p)).ToList();
			}

			if(String.IsNullOrEmpty(fullNamespace))
				return new DotNetQualifiedTypeName(localName, parameters);

			return new DotNetQualifiedTypeName(localName, parameters, DotNetQualifiedTypeName.FromVisualStudioXml(fullNamespace));
		}

		/// <summary>
		/// Parses a System.Reflection.AssemblyInfo full name.
		/// </summary>
		public static new DotNetQualifiedTypeName FromAssemblyInfo(Type type)
		{
			return FromAssemblyInfo(type, null);
		}

		/// <summary>
		/// Parses a System.Reflection.AssemblyInfo full name.
		/// </summary>
		/// <list>
		///   <item>The escape character is backslash (\)</item>
		///   <item>Nested types are separated with '+' instead of '.'</item>
		///   <item>Class declaration of generic types are shown the same as .Net XML documentation: MyType`1 for one generic type</item>
		///   <item>When a generic type is defined: System.Collections.Generic.List`1[U], where U is the type alias from the class declaration</item>
		/// </list>
		/// <param name="type"></param>
		/// <param name="bubbleUpParameters">Optional. When reflection gives type information about a generic type nested inside a generic type, all the generic-type-arguments are listed in the inner-most type. This is for passing that information back up the chain of types.</param>
		public static DotNetQualifiedTypeName FromAssemblyInfo(Type type, List<DotNetQualifiedTypeName> bubbleUpParameters = null)
		{
			if(type == null) return new DotNetQualifiedTypeName();

			string typeName = type.FullName;
			if(String.IsNullOrEmpty(typeName))
				typeName = type.Name; //for generic types

			typeName = typeName.Replace("@", "").Replace("&", ""); //out and ref modifiers do not differentiate method signatures

			typeName = typeName.ReplaceUnescapedCharacters('\\', '+', '.');
			if(String.IsNullOrEmpty(typeName))
				typeName = ""; //todo: this should not be necessary, track down case in EarlyDocs
			List<DotNetQualifiedTypeName> parameters = new List<DotNetQualifiedTypeName>();
			int genericParameterCount = 0;
			if(!type.IsGenericParameter)
			{
				if(type.ContainsGenericParameters) //generic type, with parameters unspecified
				{
					if(bubbleUpParameters != null && bubbleUpParameters.Count > 0) //actually, this is a generic type with specific parameter types that has a nested generic type within it
					{
						parameters = new List<DotNetQualifiedTypeName>(bubbleUpParameters);
					}
					else
					{
						parameters = type.GetGenericArguments().Select(a => FromAssemblyInfo(a)).ToList();
					}

					if(typeName.Contains("["))
					{
						typeName = typeName.Substring(0, typeName.IndexOf("["));
					}
					Int32.TryParse(typeName.Substring(typeName.LastIndexOf("`") + 1), out genericParameterCount);
					typeName = typeName.Substring(0, typeName.LastIndexOf("`"));
				}
				else if(type.GetGenericArguments().Length > 0) //generic type, with specific parameter types
				{
					//TODO is this all correct? what case where these commented out statements meant to handle?
					//int parameterPosition = 0;
					foreach(Type parameter in type.GetGenericArguments())
					{
						parameters.Add(FromAssemblyInfo(parameter));
						//parameters.Add(new DotNetReferenceClassGeneric(parameterPosition, parameter.Name));
						//parameterPosition++;
					}
					typeName = typeName.Substring(0, typeName.IndexOf("["));
					string numberString = typeName.Substring(typeName.LastIndexOf("`") + 1);
					Int32.TryParse(numberString, out genericParameterCount);
					typeName = typeName.Substring(0, typeName.LastIndexOf("`"));

					if(bubbleUpParameters != null)
					{
						if(parameters.Count > bubbleUpParameters.Count)
							throw new Exception("Type has more generic-type-parameters than expected.");
						int remainder = bubbleUpParameters.Count - parameters.Count;
						parameters = bubbleUpParameters.Skip(remainder).ToList();
						bubbleUpParameters = bubbleUpParameters.Take(remainder).ToList();
					}
				}
			}

			int divider = typeName.LastIndexOf('.');
			string localName = typeName;
			string fullNamespaceString = null;
			if(divider != -1)
			{
				localName = typeName.Substring(divider + 1);
				fullNamespaceString = typeName.Substring(0, divider);
			}

			if(String.IsNullOrEmpty(fullNamespaceString))
				return new DotNetQualifiedTypeName(localName, parameters);

			//so, if a generic type is nested inside another generic type, the Assembly info returns the full list of expected generic parameters to the innermost generic type
			//which means I need to pass them back up
			List<DotNetQualifiedTypeName> outerParameters = null;
			if(genericParameterCount < parameters.Count)
			{
				outerParameters = parameters.Take(parameters.Count - genericParameterCount).ToList();
				parameters = parameters.Skip(outerParameters.Count).ToList();
			}

			DotNetQualifiedTypeName fullNamespace = null;
			if(type.DeclaringType != null)
				fullNamespace = DotNetQualifiedTypeName.FromAssemblyInfo(type.DeclaringType, outerParameters);
			else
				fullNamespace = DotNetQualifiedTypeName.FromAssemblyInfo(fullNamespaceString);

			return new DotNetQualifiedTypeName(localName, parameters, fullNamespace);
		}

		/// <summary></summary>
		public new static DotNetQualifiedTypeName FromAssemblyInfo(string typeName)
		{
			DotNetQualifiedName name = DotNetQualifiedName.FromAssemblyInfo(typeName);
			return name.ToDotNetQualifiedTypeName();
		}

		#endregion

		/// <summary>
		/// Collect full list of local names used throughout documentation.
		/// Includes namespaces, internal types, external types, and members.
		/// Does not include generic paremeters.
		/// </summary>
		internal override List<string> GetFullListOfLocalNames()
		{
			List<string> localNames = new List<string>();

			localNames.Add(localName);
			foreach(DotNetQualifiedName parameter in GenericTypeParameters)
			{
				localNames.AddRange(parameter.GetFullListOfLocalNames());
			}
			if(FullNamespace != null)
			{
				localNames.AddRange(FullNamespace.GetFullListOfLocalNames());
			}

			return localNames;
		}

		/// <inheritdoc/>
		public new DotNetQualifiedTypeName GetLocalized(DotNetQualifiedName other)
		{
			DotNetQualifiedTypeName clone = this.Clone();
			clone.Localize(other);
			return clone;
		}

		/// <inheritdoc />
		/// <remarks>
		/// All generic parameters are also localized.
		/// </remarks>
		public override void Localize(DotNetQualifiedName other)
		{
			if(FullNamespace == null || other == null)
				return;
			base.Localize(other);
			foreach(DotNetQualifiedTypeName p in GenericTypeParameters)
			{
				p.Localize(other);
			}
		}

		#region Low Level

		/// <summary>
		/// Returns deep clone of qualified name.
		/// </summary>
		public new DotNetQualifiedTypeName Clone()
		{
			DotNetQualifiedTypeName clonedFullNamespace = null;
			if(FullNamespace != null)
				clonedFullNamespace = FullTypeNamespace.Clone();

			List<DotNetQualifiedTypeName> clonedGenericTypeParameters = GenericTypeParameters.Select(gtp => gtp.Clone()).ToList();

			return new DotNetQualifiedTypeName(localName, clonedGenericTypeParameters, clonedFullNamespace);
		}

		#endregion
	}
}
