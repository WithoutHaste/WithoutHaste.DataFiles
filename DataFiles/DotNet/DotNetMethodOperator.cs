using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a method that is an operator.
	/// </summary>
	public class DotNetMethodOperator : DotNetMethod, IComparable
	{
		/// <summary>Operators will be sorted into this order.</summary>
		public static readonly string[] OperatorOrder = new string[] {
				"op_Implicit",
				"op_Explicit",

				"op_Addition",
				"op_Increment",
				"op_Subtraction",
				"op_Decrement",
				"op_Multiply",
				"op_Division",
				"op_Modulus",

				"op_Equality",
				"op_Inequality",
				"op_GreaterThan",
				"op_LessThan",
				"op_GreaterThanOrEqual",
				"op_LessThanOrEqual",
				"op_BitwiseAnd",
				"op_BitwiseOr",
				"op_ExclusiveOr",
				"op_LogicalNot",
				"op_OnesComplement",
				"op_True",
				"op_False",

				"op_RightShift",
				"op_LeftShift",
			};

		/// <summary></summary>
		public DotNetMethodOperator(DotNetQualifiedMethodName name) : base(name)
		{
			Category = MethodCategory.Normal;
		}

		#region Low Level

		/// <duplicate cref='CompareTo(object)' />
		public static bool operator <(DotNetMethodOperator a, DotNetMethodOperator b)
		{
			return (a.CompareTo(b) == -1);
		}

		/// <duplicate cref='CompareTo(Object)' />
		public static bool operator >(DotNetMethodOperator a, DotNetMethodOperator b)
		{
			return (a.CompareTo(b) == 1);
		}

		/// <summary>
		/// Methods are sorted:
		/// <list type='number'>
		///		<item>alphabetically by namespace</item>
		///		<item>then into <see cref='OperatorOrder'/></item>
		///		<item>then as a normal method (see <see cref='DotNetQualifiedMethodName.CompareTo(object)'/></item>
		/// </list>
		/// </summary>
		public int CompareTo(object b)
		{
			if(!(b is DotNetMethodOperator))
				return -1;
			DotNetMethodOperator other = (b as DotNetMethodOperator);

			if(this.Name.FullNamespace != other.Name.FullNamespace)
				return this.Name.FullNamespace.CompareTo(other.Name.FullNamespace);

			if(this.Name.LocalName != other.Name.LocalName)
			{
				int indexThis = Array.IndexOf(OperatorOrder, this.Name.LocalName);
				int indexOther = Array.IndexOf(OperatorOrder, other.Name.LocalName);
				if(indexThis == -1 && indexOther == -1)
				{
					return this.Name.LocalName.CompareTo(other.Name.LocalName);
				}
				return indexThis.CompareTo(indexOther);
			}

			return this.MethodName.CompareTo(other.MethodName);
		}

		#endregion
	}
}
