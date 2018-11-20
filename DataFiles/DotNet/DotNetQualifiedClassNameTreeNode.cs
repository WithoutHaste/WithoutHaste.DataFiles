using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles.DotNet
{
	/// <summary>
	/// A node in a tree data structure made up of DotNetQualifiedClassNames organized by their namespaces.
	/// </summary>
	public class DotNetQualifiedClassNameTreeNode
	{
		/// <summary></summary>
		public DotNetQualifiedClassNameTreeNode Parent { get; protected set; }

		/// <summary></summary>
		public DotNetQualifiedClassName Value { get; protected set; }

		/// <summary></summary>
		public List<DotNetQualifiedClassNameTreeNode> Children { get; protected set; }

		internal DotNetQualifiedClassNameTreeNode(DotNetQualifiedClassName value)
		{
			Value = value;
			Parent = null;
			Children = new List<DotNetQualifiedClassNameTreeNode>();
		}

		/// <summary>
		/// Organize a list of namespaces into a tree, based on which namespaces are within other namespaces.
		/// </summary>
		/// <returns>
		/// Returns the root of the new tree.
		/// If there is one top-level namespace, root.Value will be that namespace.
		/// If there are more than one top-level namespaces, root.Value will be null and root.Children will contain the top-level namespaces.
		/// </returns>
		public static DotNetQualifiedClassNameTreeNode Generate(List<DotNetQualifiedClassName> names)
		{
			if(names == null || names.Count == 0)
				return new DotNetQualifiedClassNameTreeNode(null);

			DotNetQualifiedClassNameTreeNode root = new DotNetQualifiedClassNameTreeNode(null);
			foreach(DotNetQualifiedClassName name in names)
			{
				root.Insert(name);
			}
			return root;
		}

		/// <summary>
		/// Insert a new namespace into its proper position, based on the current node as the root.
		/// </summary>
		public void Insert(DotNetQualifiedClassName name)
		{
			//first root
			if(this.Value == null && this.Children.Count == 0)
			{
				this.Value = name;
				return;
			}

			if(this.Value == null) //null root over multiple top-level namespaces
			{
				//this belongs below a child
				foreach(DotNetQualifiedClassNameTreeNode childNode in this.Children)
				{
					if(name.IsWithin(childNode.Value))
					{
						childNode.Insert(name);
						return;
					}
				}
				//this is at the same level as children
				DotNetQualifiedClassNameTreeNode newNode = new DotNetQualifiedClassNameTreeNode(name);
				this.AddChild(newNode);
				//children that belong below the new node
				int index = 0;
				while(index < this.Children.Count)
				{
					if(this.Children[index].Value.IsWithin(name))
					{
						this.TransferChildTo(this.Children[index], newNode);
						continue;
					}
					index++;
				}
				//all children are within new name
				if(this.Children.Count == 1)
				{
					newNode.TransferChildrenTo(this);
					this.Value = newNode.Value;
					this.Children.Remove(newNode);
				}
			}
			else //normal, full tree
			{
				if(this.Value.IsWithin(name)) //new root
				{
					DotNetQualifiedClassNameTreeNode rootTransfer = new DotNetQualifiedClassNameTreeNode(this.Value);
					this.TransferChildrenTo(rootTransfer);
					this.Value = name;
					this.AddChild(rootTransfer);
					return;
				}
				else if(name.IsWithin(this.Value)) //descendent of root
				{
					//belongs below a child
					foreach(DotNetQualifiedClassNameTreeNode childNode in this.Children)
					{
						if(name.IsWithin(childNode.Value))
						{
							childNode.Insert(name);
							return;
						}
					}
					//belongs at same level as children
					DotNetQualifiedClassNameTreeNode newNode = new DotNetQualifiedClassNameTreeNode(name);
					this.AddChild(newNode);
					//children that belong below the new node
					int index = 0;
					while(index < this.Children.Count)
					{
						if(this.Children[index].Value.IsWithin(name))
						{
							this.TransferChildTo(this.Children[index], newNode);
							continue;
						}
						index++;
					}
				}
				else //same level as root
				{
					DotNetQualifiedClassNameTreeNode rootTransfer = new DotNetQualifiedClassNameTreeNode(this.Value);
					this.TransferChildrenTo(rootTransfer);
					this.Value = null;
					this.AddChild(rootTransfer);
					DotNetQualifiedClassNameTreeNode newNode = new DotNetQualifiedClassNameTreeNode(name);
					this.AddChild(newNode);
				}
			}
		}

		private void TransferChildrenTo(DotNetQualifiedClassNameTreeNode other)
		{
			while(this.Children.Count > 0)
			{
				other.AddChild(this.Children[0]);
				this.Children.RemoveAt(0);
			}
		}

		private void TransferChildTo(DotNetQualifiedClassNameTreeNode child, DotNetQualifiedClassNameTreeNode other)
		{
			other.AddChild(child);
			this.Children.Remove(child);
		}

		private void AddChild(DotNetQualifiedClassNameTreeNode child)
		{
			Children.Add(child);
			child.Parent = this;
		}
	}
}
