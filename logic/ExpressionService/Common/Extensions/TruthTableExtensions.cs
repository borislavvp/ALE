using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.ExpressionService.Common.Extensions
{
    public static class TruthTableExtensions
    {
        public static List<string> GetRowValuesWithouthResultColumn(this TruthTableValues values, int row)
        {
            var list = new List<string>();
            foreach (var key in values.Keys)
            {
                if (key != values.Keys.Last())
                {
                    list.Add(values[key][row]);
                }
            }
            return list;
        }
        public static void DeleteRowValues(this TruthTableValues values, int row)
        {
            foreach (var key in values.Keys)
            {
                values[key].RemoveAt(row);
            }
        }
        public static TruthTable BuildTruthTable(this IBinaryExpressionTree tree, string resultColumnName)
        {
            List<INode> leafs = tree.GetLeafsSortedA2z();
            int rows = (int)Math.Pow(2, leafs.Count());

            return FillTruthTableVariablesValues(leafs, rows)
                .EvaluateTruthTable(tree, resultColumnName, rows);
        }
        public static TruthTableValues FillTruthTableVariablesValues(List<INode> leafs, int rows)
        {
            TruthTableValues tableValues = new TruthTableValues();
            foreach (INode leaf in leafs)
            {
                int numberOfZeros = (int)Math.Pow(2, (leafs.Count() - tableValues.Count()) - 1);

                string valueToAdd = "0";

                int curr = 0;

                for (int i = 0; i < rows; i++)
                {
                    if (tableValues.ContainsKey($@"{leaf.Value}"))
                        tableValues[$@"{leaf.Value}"].Add(valueToAdd);
                    else
                        tableValues.Add($@"{leaf.Value}", new List<string>() { valueToAdd });

                    if (++curr == numberOfZeros)
                    {
                        valueToAdd = valueToAdd == "0" ? "1" : "0";
                        curr = 0;
                    }
                }
            }
            return tableValues;
        }

        private static TruthTable EvaluateTruthTable(
            this TruthTableValues tableValues,
            IBinaryExpressionTree tree,
            string resultColumnName,
            int rows)
        {
            tableValues.Add(resultColumnName, new List<string>());
            Stack<bool> stack = new Stack<bool>();
            var postOrderedTree = tree.TraversePostOrder();
            for (int i = 0; i < rows; i++)
            {
                foreach (var node in postOrderedTree)
                {
                    if (node.Value.IsOperator())
                    {
                        if (((char)Operators.Negation).Equals(node.Value))
                        {
                            bool value = stack.Pop();
                            Operators operatoValue = (Operators)node.Value;

                            bool operationResult = HelperExtensions.GetOperationResult(value, operatoValue);
                            stack.Push(operationResult);
                        }
                        else
                        {
                            bool value2 = stack.Pop();
                            bool value1 = stack.Pop();
                            Operators operatoValue = (Operators)node.Value;

                            bool operationResult = HelperExtensions.GetOperationResult(value1, operatoValue, value2);
                            stack.Push(operationResult);
                        }
                    }
                    else
                    {
                        stack.Push(Convert.ToInt32(tableValues[$@"{node.Value}"][i]).ToBool());
                    }
                }
                tableValues[resultColumnName].Add(stack.Pop() ? "1" : "0");
            }

            return new TruthTable(tableValues);
        }
        
    }
}
