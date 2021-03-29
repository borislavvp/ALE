using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ExpressionService.Helpers
{
    public static class TruthTableHelpers
    {
        private static TruthTableValues GenerateVariableValues(List<int> minterms)
        {
            int maxMinterm = minterms.Max();
            int numberOfVariables = -1;

            for (int i = 1; i <= 50; i++)
            {
                if (Math.Pow(2, i) > maxMinterm)
                {
                    numberOfVariables = i;
                    break;
                }
            }

            List<INode> tableVariables = new List<INode>();
            for (int i = 0; i < numberOfVariables; i++)
            {
                char variable = i > 25 ? (char)(70 + i) : (char)(65 + i);
                tableVariables.Add(new Node(variable, i));
            }

            return TruthTableExtensions.FillTruthTableVariablesValues(tableVariables, (int)Math.Pow(2, numberOfVariables));
        }
        public static TruthTable GenerateTruthTable(List<int> minterms)
        {
            TruthTableValues values = GenerateVariableValues(minterms);
            int rows = (int)Math.Pow(2, values.Count);

            List<string> resultValues = new List<string>();
            for (int i = 0; i < rows; i++)
            {
                resultValues.Add(minterms.Contains(i) ? "1" : "0");
            }

            values.Add("RESULT", resultValues);

            return new TruthTable(new Mock<IBinaryExpressionTree>().Object, values);

        }
    }
}
