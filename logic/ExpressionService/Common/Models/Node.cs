using logic.ExpressionService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public class Node : INode
    {
        public Guid ID { get; set; }
        public char Value { get; set; }
        public bool IsLeaf { get; set; }
        public int Level { get; set; }

        public Node(char Value, int Level, bool IsLeaf = false)
        {
            this.ID = Guid.NewGuid();
            this.Value = Value;
            this.IsLeaf = IsLeaf;
            this.Level = Level;
        }
        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Node node &&
                   Value == node.Value;
        }

        public override int GetHashCode()
        {
            int hashCode = -1211885195;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }
    }
}
