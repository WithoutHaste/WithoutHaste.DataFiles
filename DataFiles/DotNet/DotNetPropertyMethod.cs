using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a getter or a setter method of a property.
	/// </summary>
	public class DotNetPropertyMethod
	{
		/// <summary></summary>
		public bool IsAbstract { get; protected set; }

		/// <summary></summary>
		public AccessModifier AccessModifier { get; protected set; }

		#region Constructors

		/// <summary></summary>
		public DotNetPropertyMethod() : this(AccessModifier.Unknown, false)
		{
		}

		/// <summary></summary>
		public DotNetPropertyMethod(AccessModifier accessModifier, bool isAbstract)
		{
			AccessModifier = accessModifier;
			IsAbstract = isAbstract;
		}

		/// <summary>
		/// Load additional documentation information from the assembly itself.
		/// </summary>
		public static DotNetPropertyMethod FromAssemblyInfo(MethodInfo methodInfo)
		{
			if(methodInfo == null)
				return null;

			bool isAbstract = methodInfo.IsAbstract;

			AccessModifier accessModifier = AccessModifier.Unknown;
			if(methodInfo.IsPublic)
				accessModifier = AccessModifier.Public;
			else if(methodInfo.IsPrivate)
				accessModifier = AccessModifier.Private;
			else
				accessModifier = AccessModifier.Protected;

			return new DotNetPropertyMethod(accessModifier, isAbstract);
		}

		#endregion
	}
}
