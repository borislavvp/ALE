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
        private static string OnTraversePrefixExpression(this IPrefixExpression prefix,Func<string,Operators,string,string> action)
        {
            Stack<string> stack = new Stack<string>();
            IParsedExpression parsed = prefix.ParseExpression();
            // Length of expression 
            int l = parsed.Value.Length;

            // Reading from right to left 
            for (int i = l - 1; i >= 0; i--)
            {
                char c = parsed.Value[i];

                // Check whether the character is an operator
                if (c.IsOperator())
                {
                    // Check whether the character is operator of type Negation 
                    if (((char)Operators.Negation).Equals(c))
                    {
                        // Get the previous result from the stack,calculate the next nandified result and put it in the stack
                        string value = stack.Pop();
                        stack.Push(action(value,Operators.Negation,null));
                    }
                    else
                    {
                        // Get the previous result from the stack,calculate the next nandified result and put it in the stack
                        string value1 = stack.Pop();
                        string value2 = stack.Pop();
                        stack.Push(action(value1, (Operators)c, value2));
                    }
                }
                else
                {
                    // Put the variable in the stack
                    stack.Push($@"{c}");
                }
            }
            return stack.Pop();
        }
        public static string NandifyExpression<T>(this T PrefixExpression) where T : IPrefixExpression
        {
            return PrefixExpression.OnTraversePrefixExpression(HelperExtensions.NadifyOperationResult);
        }

        public static string TransformToInfix<T>(this T PrefixExpression) where T : IPrefixExpression
        {
            Func<string, Operators, string, string> getInfixNotation = (string value1, Operators op, string value2) =>
            {
                if (String.IsNullOrEmpty(value2))
                {
                    return op.OperatorValue() + "(" + value1 + ")";
                }
                else
                {
                    return "(" + value1 + op.OperatorValue() + value2 + ")";
                }
            };

            return PrefixExpression.OnTraversePrefixExpression(getInfixNotation);
        }

        public static IParsedExpression ParseExpression<T>(this T expression) where T : IExpression
        {
            return new ParsedExpression { Value = expression.Value.Replace("(", "").Replace(")", "").Replace(",", "").Replace(" ", "").ToCharArray() };
        }

        private static Queue<char> PrepareExpressionValues<T>(this T parsedExpression) where T : IParsedExpression
        {
            Queue<char> nodes = new Queue<char>(parsedExpression.Value.Length);
            for (int i = 0; i < parsedExpression.Value.Length; i++)
            {
                nodes.Enqueue(parsedExpression.Value[i]);
            }
            return nodes;
        }

        private static IBinaryExpressionTree BuildPrefixTree(this Queue<char> expressionValues, int identation)
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
                    BuildPrefixTree(expressionValues, identation),
                    ((char)Operators.Negation).Equals(value)
                        ? null
                        : BuildPrefixTree(expressionValues, identation)
                );
                return tree;
            }
        }

        public static IBinaryExpressionTree BuildExpressionTree<T>(this T expression) where T : IExpression
        {
            return expression.ParseExpression().PrepareExpressionValues().BuildPrefixTree(0);
        }
   
    }
}
