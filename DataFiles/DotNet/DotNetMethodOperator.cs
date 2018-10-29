using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// Represents a method that is an operator.
	/// </summary>
	public class DotNetMethodOperator : DotNetMethod, IComparable
	{
		/// <summary>Operators will be sorted into this order.</summary>
		public static readonly string[] OperatorOrder = new string[] {
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
		public DotNetMethodOperator(DotNetQualifiedName name, List<DotNetParameter> parameters) : base(name, parameters)
		{
			Category = MethodCategory.Normal;
		}

		#region Low Level

		/// <duplicate cref='CompareTo(object)' />
		public static bool operator <(DotNetMethodOperator a, DotNetMethodOperator b)
		{
			if(a.Name.FullNamespace != b.Name.FullNamespace)
				return (a.Name.FullNamespace.ToString().CompareTo(b.Name.FullNamespace.ToString()) == -1);

			return a.Equals(b);
		}

		/// <duplicate cref='CompareTo(Object)' />
		public static bool operator >(DotNetMethodOperator a, DotNetMethodOperator b)
		{
			return !a.Equals(b);
		}

		/// <summary>
		/// Methods are sorted:
		/// <list type='numbered'>
		///		<item>alphabetically by namespace</item>
		///		<item>then into <see cref='OperatorOrder'/></item>
		///		<item>then parameter list, shortest to longest</item>
		///		<item>then alphabetically by parameter types</item>
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

			if(this.Parameters.Count != other.Parameters.Count)
				return this.Parameters.Count.CompareTo(other.Parameters.Count);

			for(int i = 0; i < this.Parameters.Count; i++)
			{
				if(this.Parameters[i].TypeName == other.Parameters[i].TypeName)
					continue;
				return this.Parameters[i].TypeName.CompareTo(other.Parameters[i].TypeName);
			}

			return 0;
		}

		#endregion
	}
}
