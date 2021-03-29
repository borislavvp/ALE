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

                if (c.IsOperator())
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

        private static IBinaryExpressionTree buildPrefixTree(this Queue<char> expressionValues, int identation)
        {
            if (expressionValues.Count <= 0) return null;

            char value = expressionValues.Dequeue();

            if (!value.IsOperator())
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

        public static IBinaryExpressionTree BuildExpressionTree<T>(this T expression) where T : IExpression
        {
            return expression.parseExpression().prepareExpressionValues().buildPrefixTree(0);
        }
   
    }
}
