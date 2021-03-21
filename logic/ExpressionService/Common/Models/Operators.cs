using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public enum Operators
    {
        Negation = '~',
        Conjunction = '&',
        Disjunction = '|',
        Implication = '>',
        Biimplication = '='
    }
}
