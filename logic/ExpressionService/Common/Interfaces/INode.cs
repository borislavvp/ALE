using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Interfaces
{
    public interface INode
    {
        Guid ID { get; set; }
        char Value { get; set; }
        int Level { get; set; }
        bool IsLeaf { get; set; }
    }
}
