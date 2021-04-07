using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public class BinaryExpressionTree : IBinaryExpressionTree
    {
        public INode Parent { get; set; }
        public INode CurrentNode { get; set; }
        public IBinaryExpressionTree LeftChild { get; set; }
        public IBinaryExpressionTree RightChild { get; set; }

        public BinaryExpressionTree(INode CurrentNode, IBinaryExpressionTree leftChild = null, IBinaryExpressionTree rightChild = null)
        {
            this.CurrentNode = CurrentNode;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            if (leftChild != null)
                this.LeftChild.Parent = this.CurrentNode;
            if (rightChild != null)
                this.RightChild.Parent = this.CurrentNode;
        }

        public List<INode> GetNodes()
        {
            List<INode> nodes = new List<INode>();
            Stack<IBinaryExpressionTree> temp = new Stack<IBinaryExpressionTree>();
            temp.Push(this);
            while (temp.Count > 0)
            {
                IBinaryExpressionTree current = temp.Pop();
                if (current.RightChild != null)
                    temp.Push(current.RightChild);
                if (current.LeftChild != null)
                    temp.Push(current.LeftChild);
                nodes.Add(current.CurrentNode);
            }
            return nodes;
        }
        public List<INode> GetLeafs()
        {
            HashSet<INode> addedLeafs = new HashSet<INode>();
            List<INode> nodes = this.GetNodes();

            List<INode> leafs = new List<INode>();

            nodes.ForEach(node => {
                if (node.IsLeaf)
                {
                    if (!addedLeafs.Contains(node))
                    {
                        leafs.Add(node);
                        addedLeafs.Add(node);
                    }
                }
            });
            return leafs;
        }
        public List<INode> GetLeafsSortedA2z()
        {
            return HelperExtensions.CountSortNodes(GetLeafs());
        }

        public Stack<INode> TraversePostOrder()
        {
            Stack<IBinaryExpressionTree> temp = new Stack<IBinaryExpressionTree>();
            Stack<INode> stack = new Stack<INode>();
            temp.Push(this);
            while (temp.Count > 0)
            {
                IBinaryExpressionTree root = temp.Pop();
                stack.Push(root.CurrentNode);

                if (root.LeftChild != null)
                    temp.Push(root.LeftChild);
                if (root.RightChild != null)
                    temp.Push(root.RightChild);
            }
            return stack;
        }

        public List<IEdge> GetEdges()
        {
            List<IEdge> edges = new List<IEdge>();
            Stack<IBinaryExpressionTree> temp = new Stack<IBinaryExpressionTree>();
            temp.Push(this);
            while (temp.Count > 0)
            {
                IBinaryExpressionTree current = temp.Pop();
                if (current.RightChild != null)
                    temp.Push(current.RightChild);
                if (current.LeftChild != null)
                    temp.Push(current.LeftChild);
                if (current.Parent != null)
                    edges.Add(new Edge(current.CurrentNode, current.Parent));
            }
            return edges;
        }
    }
}
