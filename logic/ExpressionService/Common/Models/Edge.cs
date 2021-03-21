using logic.ExpressionService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public class Edge : IEdge
    {
        public INode Parent { get; }
        public INode Child { get; }
        public Edge(INode child, INode parent)
        {
            this.Child = child;
            this.Parent = parent;
        }

        public override bool Equals(object obj)
        {
            return obj is Edge edge &&
                   EqualityComparer<INode>.Default.Equals(Parent, edge.Parent) &&
                   EqualityComparer<INode>.Default.Equals(Child, edge.Child);
        }

        public override int GetHashCode()
        {
            int hashCode = -157375006;
            hashCode = hashCode * -1521134295 + EqualityComparer<INode>.Default.GetHashCode(Parent);
            hashCode = hashCode * -1521134295 + EqualityComparer<INode>.Default.GetHashCode(Child);
            return hashCode;
        }

    }
}
