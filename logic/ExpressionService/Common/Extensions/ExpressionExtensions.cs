using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.ExpressionService.Common.Extensions
{
    public static class ExpressionExtensions
    {
        public static string transformToInfix<T>(this T PrefixExpression) where T : IPrefixExpression
        {
            Stack stack = new Stack();
            IParsedExpression parsed = PrefixExpression.parseExpression();
            // Length of expression 
            int l = parsed.Value.Length;

            // Reading from right to left 
            for (int i = l - 1; i >= 0; i--)
            {
                char c = parsed.Value[i];

                if (c.isOperator())
                {
                    if (((char)Operators.Negation).Equals(c))
                    {
                        string op1 = (string)stack.Pop();
                        string temp = c + "(" + op1 + ")";
                        stack.Push(temp);
                    }
                    else
                    {
                        string op1 = (string)stack.Pop();
                        string op2 = (string)stack.Pop();
                        string temp = "(" + op1 + c + op2 + ")";
                        stack.Push(temp);
                    }
                }
                else
                {
                    stack.Push(c + "");
                }
            }
            return $@"{stack.Pop()}";
        }

        public static IParsedExpression parseExpression<T>(this T expression) where T : IExpression
        {
            return new ParsedExpression { Value = expression.Value.Replace("(", "").Replace(")", "").Replace(",", "").Replace(" ", "").ToCharArray() };
        }

        private static Queue<char> prepareExpressionValues<T>(this T parsedExpression) where T : IParsedExpression
        {
            Queue<char> nodes = new Queue<char>(parsedExpression.Value.Length);
            for (int i = 0; i < parsedExpression.Value.Length; i++)
            {
                nodes.Enqueue(parsedExpression.Value[i]);
            }
            return nodes;
        }

        private static BinaryExpressionTree buildPrefixTree(this Queue<char> expressionValues, int identation)
        {
            if (expressionValues.Count <= 0) return null;

            char value = expressionValues.Dequeue();

            if (!value.isOperator())
            {
                return new BinaryExpressionTree(new Node(value, identation, true));
            }
            else
            {
                BinaryExpressionTree tree = new BinaryExpressionTree(
                    new Node(value, identation++, false),
                    buildPrefixTree(expressionValues, identation),
                    ((char)Operators.Negation).Equals(value)
                        ? null
                        : buildPrefixTree(expressionValues, identation)
                );
                return tree;
            }
        }

        public static BinaryExpressionTree BuildExpressionTree<T>(this T expression) where T : IExpression
        {
            return expression.parseExpression().prepareExpressionValues().buildPrefixTree(0);
        }
        public static TruthTable BuildTruthTable(this IBinaryExpressionTree tree, string infixNotation)
        {
            List<INode> leafs = tree.GetLeafsSortedA2z();
            int rows = (int)Math.Pow(2, leafs.Count());

            return FillTruthTableVariablesValues(leafs, rows)
                .EvaluateTruthTable(tree, infixNotation, rows);
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
            string infixNotation,
            int rows)
        {
            tableValues.Add(infixNotation, new List<string>());
            Stack<bool> stack = new Stack<bool>();
            var postOrderedTree = tree.TraversePostOrder();
            for (int i = 0; i < rows; i++)
            {
                foreach (var node in postOrderedTree)
                {
                    if (node.Value.isOperator())
                    {
                        if (((char)Operators.Negation).Equals(node.Value))
                        {
                            bool value = stack.Pop();
                            Operators operatoValue = (Operators)node.Value;

                            bool operationResult = GetOperationResult(value, operatoValue, null);
                            stack.Push(operationResult);
                        }
                        else
                        {
                            bool value2 = stack.Pop();
                            bool value1 = stack.Pop();
                            Operators operatoValue = (Operators)node.Value;

                            bool operationResult = GetOperationResult(value1, operatoValue, value2);
                            stack.Push(operationResult);
                        }
                    }
                    else
                    {
                        stack.Push(Convert.ToInt32(tableValues[$@"{node.Value}"][i]).ToBool());
                    }
                }
                tableValues[infixNotation].Add(stack.Pop() ? "1" : "0");
            }

            return new TruthTable(tree, tableValues);
        }
        public static bool GetOperationResult(bool value1, Operators operatorValue, bool? value2)
        {
            switch (operatorValue)
            {
                case Operators.Negation:
                    return !value1;
                case Operators.Implication:
                    return (!value1 | value2.Value);
                case Operators.Biimplication:
                    return (value1 == value2.Value);
                case Operators.Conjunction:
                    return (value1 && value2.Value);
                case Operators.Disjunction:
                    return (value1 || value2.Value);
                default:
                    return false;
            }
        }
    }
}
