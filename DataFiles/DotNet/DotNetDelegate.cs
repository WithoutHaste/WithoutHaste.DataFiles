﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a delegate type, categorized as a method.
	/// </summary>
	public class DotNetDelegate : DotNetMethod
	{
		/// <summary></summary>
		public DotNetDelegate(DotNetQualifiedName name) : base(name)
		{
			Category = MethodCategory.Delegate;
		}

		/// <summary></summary>
		public DotNetDelegate(DotNetQualifiedName name, List<DotNetParameter> parameters) : base(name, parameters)
		{
			Category = MethodCategory.Delegate;
		}

		/// <summary></summary>
		public void AddAssemblyInfo(TypeInfo typeInfo)
		{
			AddAssemblyInfo(typeInfo.DeclaredMethods.First(m => m.Name == "Invoke"));
		}
	}
}