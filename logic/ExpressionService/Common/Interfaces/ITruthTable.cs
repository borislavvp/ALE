using logic.ExpressionService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public interface ITruthTable 
    {
        TruthTableValues Value { get; set; }
        string HexResult { get;}

        IPrefixExpression NormalizeOriginal();
        IPrefixExpression NormalizeSimplified();
        TruthTableValues Simplify();
    }
}
