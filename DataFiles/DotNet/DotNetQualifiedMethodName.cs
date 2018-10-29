using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a fully qualified method name.
	/// </summary>
	/// <remarks>
	/// Cannot handle methods that declare more than 12 generic types,
	/// such as <![CDATA[MyMethod<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13>]]>.
	/// </remarks>
	public class DotNetQualifiedMethodName : DotNetQualifiedName
	{
		/// <summary>Default names that will be given to generic-method-types, in order.</summary>
		public static string[] GenericTypeNames = new string[] { "A", "B", "C", "A2", "B2", "C2", "A3", "B3", "C3", "A4", "B4", "C4" };

		/// <summary>Local method name with generic type parameters (if applicable).</summary>
		public override string LocalName {
			get {
				if(GenericTypeCount == 0)
					return localName;
				if(genericTypeAliases != null)
					return String.Format("{0}<{1}>", localName, String.Join(",", genericTypeAliases));
				else
					return String.Format("{0}<{1}>", localName, String.Join(",", GenericTypeNames.Take(GenericTypeCount).ToArray()));
			}
		}

		/// <inheritdoc/>
		public override string LocalXmlName {
			get {
				if(GenericTypeCount == 0)
					return localName;
				return String.Format("{0}``{1}", localName, GenericTypeCount);
			}
		}

		/// <summary>True for generic methods.</summary>
		public bool IsGeneric { get { return (GenericTypeCount > 0); } }

		/// <summary>The number of generic-types required by the method declaration.</summary>
		public int GenericTypeCount { get; protected set; }

		/// <summary>Specific generic type aliases for this method. If null, the shared <see cref="GenericTypeNames"/> will be used.</summary>
		protected string[] genericTypeAliases;

		#region Constructors

		/// <summary>Empty constructor</summary>
		public DotNetQualifiedMethodName() : base()
		{
			GenericTypeCount = 0;
		}

		/// <summary></summary>
		public DotNetQualifiedMethodName(string localName, int genericTypeCount = 0) : base(localName)
		{
			GenericTypeCount = genericTypeCount;
		}

		/// <summary></summary>
		public DotNetQualifiedMethodName(string localName, DotNetQualifiedName fullNamespace, int genericTypeCount = 0) : base(localName, fullNamespace)
		{
			GenericTypeCount = genericTypeCount;
		}

		/// <summary>
		/// Parses a .Net XML documentation method signature.
		/// </summary>
		/// <example>
		///   <para>
		///     How .Net xml documentation formats generic types:
		///     Backtics are followed by integers, identifying generic types.
		///   </para>
		///   <para>
		///     Double backtics (such as ``1) on a method name indicate a count of generic types for the method.
		///     <example><![CDATA[MyMethod<A,B,C> is documented as MyMethod``3]]></example>
		///     Anywhere else within this method's documentation that a double backtic appears, it indicates the index of the generic type in reference to the method declaration.
		///     <example><![CDATA[MyMethod<A,B,C>(A,B,C) is documented as MyMethod``3(``0,``1,``2)]]></example>
		///     A method that uses both its own generic types AND generic types from the class declaration will look like this:
		///     <example><![CDATA[MyMethod<A,B,C>(A,B,C,T,U) is documented as MyMethod``3(``0,``1,``2,`0,`1)]]></example>
		///   </para>
		/// </example>
		/// <param name="signature">Name may or may not start with "M:". Includes parameter list.</param>
		public new static DotNetQualifiedMethodName FromVisualStudioXml(string signature)
		{
			if(signature.StartsWith("M:")) signature = signature.Substring(2);

			string parameters = null;
			if(signature.Contains("("))
			{
				parameters = signature.Substring(signature.IndexOf("("));
				signature = signature.Substring(0, signature.IndexOf("("));
				//parameters are ignored
			}

			int divider = signature.LastIndexOf('.');
			string localName = signature;
			string fullNamespace = null;
			if(divider != -1)
			{
				localName = signature.Substring(divider + 1);
				fullNamespace = signature.Substring(0, divider);
			}

			int methodGenericTypeCount = 0;
			if(localName.Contains("``"))
			{
				Int32.TryParse(localName.Substring(localName.IndexOf("``") + 2), out methodGenericTypeCount);
				localName = localName.Substring(0, localName.IndexOf("``"));
			}

			if(String.IsNullOrEmpty(fullNamespace)) return new DotNetQualifiedMethodName(localName, methodGenericTypeCount);

			return new DotNetQualifiedMethodName(localName, DotNetQualifiedClassName.FromVisualStudioXml(fullNamespace), methodGenericTypeCount);
		}

		#endregion

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public void AddAssemblyInfo(MethodInfo methodInfo)
		{
			if(IsGeneric)
			{
				Type[] genericTypes = methodInfo.GetGenericArguments();
				this.genericTypeAliases = genericTypes.Select(g => g.ToString()).ToArray();
			}
			if(methodInfo.DeclaringType != null && FullNamespace != null && FullNamespace is DotNetQualifiedClassName)
			{
				(FullNamespace as DotNetQualifiedClassName).AddAssemblyInfo(methodInfo.DeclaringType.GetTypeInfo());
			}
		}

		/// <summary>Set the local name of the method. Does not affect generic type parameters or method parameters.</summary>
		public void SetLocalName(string name)
		{
			this.localName = name;
		}
	}
}
