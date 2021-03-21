using logic.ExpressionService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public class ParsedExpression : IParsedExpression
    {
        public char[] Value { get; set; }
    }
}
