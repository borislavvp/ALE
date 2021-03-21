using logic.ExpressionService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public class PrefixExpression : IPrefixExpression
    {
        public string Value { get; }

        public PrefixExpression(string Value)
        {
            this.Value = Value;
        }
    }
}
