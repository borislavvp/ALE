using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.Extensions
{
    public static class HelperExtensions
    {
        public static bool isOperator(this char value)
        {
            return ((char)Operators.Biimplication).Equals(value) ||
                    ((char)Operators.Implication).Equals(value) ||
                    ((char)Operators.Conjunction).Equals(value) ||
                   ((char)Operators.Disjunction).Equals(value) ||
                    ((char)Operators.Negation).Equals(value);
        }

        public static bool ToBool(this int value)
        {
            return Convert.ToBoolean(value);
        }

        public static List<INode> countSortLeafs(List<INode> arr)
        {
            int n = arr.Count;
            int A = 'A';
            int z = 'z';
            // The output character array that
            // will have sorted arr
            INode[] output = new INode[n];
            // Create a count array to store
            // count of inidividul characters
            // and initialize count array as 0
            Dictionary<int, int> count = new Dictionary<int, int>();

            for (int i = A; i <= z; ++i)
                count[i] = 0;

            // store count of each character
            for (int i = 0; i < n; ++i)
                ++count[arr[i].Value];

            // Change count[i] so that count[i]
            // now contains actual position of
            // this character in output array

            for (int i = A + 1; i <= z; ++i)
                count[i] += count[i - 1];

            // Build the output character array
            // To make it stable we are operating in reverse order.
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[arr[i].Value] - 1] = arr[i];
            }

            // Copy the output array to arr, so
            // that arr now contains sorted
            // characters
            for (int i = 0; i < n; ++i)
                arr[i] = output[i];

            return arr;
        }
    }
}
