using logic.ExpressionService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Interfaces
{
    public interface IExpressionStructure
    {
        public IPrefixExpression PrefixExpression { get; set; }
        public IBinaryExpressionTree ExpressionTree { get; set; }
        public TruthTable TruthTable { get; set; }

        void BuildExpressionTree();
        void BuildTruthTable();
    }
}
