using System;
using System.Collections.Generic;
using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using System.Text;
using System.Linq;
using logic.ExpressionService.Common.QMC.Models;
using logic.ExpressionService.Common.Models;

namespace logic.ExpressionService.Common.QMC
{
    public static class QuineMcCluskey
    {
        /// <summary>
        /// Function that evaluates whether a number is produced by multiplying 2 by 2 n number of times
        /// </summary>
        /// <param name="number">The number to evaluate</param>
        /// <returns>True if the number is power of 2 otherwise false</returns>
        private static bool IsNumberPowerOf2(int number)
        {
            return (number >= 0) && ((number & (number - 1)) == 0);
        }

        /// <summary>
        /// Function that evaluates whether two data groups can be compared.
        /// </summary>
        /// <param name="data1">First data group to compare</param>
        /// <param name="data2">Second data group to compare</param>
        /// <returns>Returns the index of the difference between the groups.</returns>
        private static int? AreGroupsComparable(QMCGroupData data1, QMCGroupData data2)
        {
            /* 
             * Check if the number of rows which the data was produced is bigger than 1. 
             * For example if we produced the data by simplifying the values from 1st row and 3rd row we are having 
             * number of rows equal to 2 - 1,3.
             * 
             */
            if(data1.NumberOfRows() > 1 && data2.NumberOfRows() > 1)
            {
                /*
                 * If the values of the two data groups are produced by simpplifying more than one row
                 * the two data groups are comparable only if 
                 * the substraction of the pair of rows that produced the values of the first group is equal to the one of the second group.
                 * Ex: data group with values 00*1 produced by simplifying rows 1,3
                 * and data group with values 01*1 produced by simplifying rows 7,9 
                 * 3-1 is equal to 2 and so is 9-7, this means the two groups are comparable.
                 * 
                 *                                       IMPORTANT
                 * This is only the case when we have produced the values of the variables in the table group 
                 * are columns with 0/1 grouped as multiples of power of 2 using the number of variables in descending order
                 * 
                 */
                if (data1.GetAbsoluteRowComparableIndex() == data2.GetAbsoluteRowComparableIndex() 
                    && IsNumberPowerOf2(data2.OriginRow - data1.OriginRow)
                )
                {
                    return data2.OriginRow - data1.OriginRow;
                }
                else
                {
                    return null;
                }
            }
            else if(IsNumberPowerOf2(data2.OriginRow - data1.OriginRow))
            {
                return data2.OriginRow - data1.OriginRow;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Function that gets the combined rows from the two data groups.
        /// </summary>
        /// <param name="data1">First data group to get the rows from</param>
        /// <param name="data2">Second data group to get the rows from</param>
        /// <returns>Returns the combined rows from the two data groups</returns>
        private static string[] GetNewRows(QMCGroupData data1, QMCGroupData data2)
        {
            if(data1.NumberOfRows() > 1 && data2.NumberOfRows() > 1)
            {
                string[] combined = new string[data1.NumberOfRows() + data2.NumberOfRows()];
                Array.Copy(data1.Rows, combined, data1.NumberOfRows());
                Array.Copy(data2.Rows, 0, combined, data1.NumberOfRows(), data2.NumberOfRows());
                return combined;
            }
            else
            {
                return new string[2] { @$"{data1.OriginRow}", $@"{data2.OriginRow}" };
            }
        }

        /// <summary>
        /// Function that forms groups of number of 1s from the truth table values that need to be simplified.
        /// Ex: Group for values with only 1 positive variables, group for values with 2 positive variables and so on...
        /// </summary>
        /// <param name="reference">The values of the truth table that has to be simplified.</param>
        /// <returns>Formed gruops with keys the number of 1's in the data.</returns>
        private static QMCGroups FormGroupsForComparison(TruthTableValues reference)
        {
            // Groups with number of ones //
            var groups = new QMCGroups();

            var resultData = reference[reference.Keys.Last()];

            for (int i = 0; i < resultData.Count; i++)
            {
                if (resultData[i] == "1")
                {
                    var rowValues = reference.GetRowValuesWithouthResultColumn(i);
                    int GroupNumber = rowValues.FindAll(n => n == "1").Count;
                    if (!groups.ContainsKey(GroupNumber))
                    {
                        groups.Add(GroupNumber, new List<QMCGroupData>() { new QMCGroupData(i, String.Join("", rowValues)) });
                    }
                    else
                    {
                        groups[GroupNumber].Add(new QMCGroupData(i, String.Join("", rowValues)));
                    }
                }
            }
            return groups;
        }
        public static IEnumerable<QMCGroupData> CalculatePrimeImplicants(TruthTableValues reference)
        {
            var groups = FormGroupsForComparison(reference);
            
            Queue<KeyValuePair<int, List<QMCGroupData>>> queue = new Queue<KeyValuePair<int, List<QMCGroupData>>>();
            foreach (var group in groups)
            {
                queue.Enqueue(group);
            }

            var marked = new HashSet<QMCGroupData>();
            var simplifiedValues = new HashSet<QMCGroupData>();

            int indexShouldNotCompare = queue.Count - 1;
            int nextKey = groups.Keys.Max()+1;

            while (queue.Count >= 2)
            {
                var group1 = queue.Dequeue();
                var group2 = queue.Peek();

                if(group1.Key != indexShouldNotCompare)
                {
                    QMCGroupData[] g1Data = new QMCGroupData[group1.Value.Count];
                    QMCGroupData[] g2Data = new QMCGroupData[group2.Value.Count];

                    for (int i = 0; i < g1Data.Length; i++)
                    {
                        g1Data[i] = group1.Value[i];
                    }
                
                    for (int i = 0; i < g2Data.Length; i++)
                    {
                        g2Data[i] = group2.Value[i];
                    }

                    List<QMCGroupData> newGroupData = new List<QMCGroupData>();
                    int g1Index = 0;
                    int g2Index = 0;

                    while (g1Index < g1Data.Length)
                    {
                        QMCGroupData data1 = g1Data[g1Index];
                        QMCGroupData data2 = g2Data[g2Index];

                        int? comparisonResult = AreGroupsComparable(data1, data2);
                        if (comparisonResult.HasValue)
                        {
                            var chars = data1.RowData.ToCharArray();
                            int indexToSimplify = chars.Length - 1 - (int)Math.Log2(comparisonResult.Value);
                            chars[indexToSimplify] = '*';
                            string simplifiedData = new string(chars);

                            QMCGroupData data = new QMCGroupData(GetNewRows(data1,data2), simplifiedData);
                            if (!simplifiedValues.Contains(data))
                            {
                                newGroupData.Add(data);
                                simplifiedValues.Add(data);
                            }

                            marked.Add(data1);
                            marked.Add(data2);

                            if (simplifiedValues.Contains(data1))
                                simplifiedValues.Remove(data1);

                            if (simplifiedValues.Contains(data2))
                                simplifiedValues.Remove(data2);
                        }
                        else if (!simplifiedValues.Contains(data1) && !marked.Contains(data1))
                        {
                            simplifiedValues.Add(data1);
                        }

                        if(++g2Index == g2Data.Length)
                        {
                            g1Index++;
                            g2Index = 0;
                        }
                    }
                    if (newGroupData.Count > 0)
                        queue.Enqueue(new KeyValuePair<int, List<QMCGroupData>>(nextKey++, newGroupData));
                    else
                        nextKey++;
                }
                else
                {
                    indexShouldNotCompare = nextKey - 1;
                }

            }

            return GetPrimeImplicants(simplifiedValues);
           
        }
  
        private static IEnumerable<QMCGroupData> GetPrimeImplicants(HashSet<QMCGroupData> simplifiedValues)
        {
            Dictionary<string, QMCGroupData> primeImplicants = new Dictionary<string, QMCGroupData>();
            HashSet<string> repeatedRows = new HashSet<string>();
            foreach (var values in simplifiedValues)
            {
                foreach (var row in values.Rows)
                {
                    if (!repeatedRows.Contains(row))
                    {
                        if (primeImplicants.ContainsKey(row)) 
                        {
                            primeImplicants.Remove(row);
                            repeatedRows.Add(row);
                        }
                        else
                        {
                            primeImplicants.Add(row, values);
                        };
                    }
                }
            }
            
            return primeImplicants.Values.Distinct();
        }
    }
}
