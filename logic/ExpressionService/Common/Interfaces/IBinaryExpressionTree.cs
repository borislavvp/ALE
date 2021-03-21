using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Interfaces
{
    public interface IBinaryExpressionTree
    {
        INode Parent { get; set; }
        INode CurrentNode { get; set; }
        IBinaryExpressionTree LeftChild { get; set; }
        IBinaryExpressionTree RightChild { get; set; }

        List<INode> GetNodes();
        List<INode> GetLeafs();
        List<INode> GetLeafsSortedA2z();
        List<IEdge> GetEdges();
        Stack<INode> TraversePostOrder();
    }
}
