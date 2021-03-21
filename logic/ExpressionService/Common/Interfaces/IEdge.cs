using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Interfaces
{
    public interface IEdge
    {
        INode Parent { get; }
        INode Child { get; }
    }
}
