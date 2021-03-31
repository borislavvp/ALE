using logic.ExpressionService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public interface ITruthTable 
    {
        TruthTableValues Value { get; set; }
        TruthTableValues SimplifiedValue { get; set; }
        string HexResult { get; set; }

        string CalculateHexResult();
        IPrefixExpression NormalizeOriginal();
        IPrefixExpression NormalizeSimplified();
        TruthTableValues Simplify();
    }
}
