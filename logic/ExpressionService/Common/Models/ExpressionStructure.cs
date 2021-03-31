using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public class ExpressionStructure : IExpressionStructure
    {
        public IPrefixExpression PrefixExpression { get; set; }
        public IBinaryExpressionTree ExpressionTree { get; set; }
        public ITruthTable TruthTable { get; set; }

        public ExpressionStructure(IPrefixExpression expression)
        {
            this.PrefixExpression = expression ?? throw new NullReferenceException("Invalid prefix expression");
        }

        public void BuildExpressionTree()
        {
            this.ExpressionTree = this.PrefixExpression.BuildExpressionTree();
        }

        public void BuildTruthTable()
        {
            if (this.ExpressionTree == null)
                throw new NullReferenceException("The expression Tree has to be build in order the truth table to be created!");
            this.TruthTable = this.ExpressionTree.BuildTruthTable(this.ToString());
        }

        override
            public string ToString()
        {
            return this.PrefixExpression.TransformToInfix();
        }
    }
}
