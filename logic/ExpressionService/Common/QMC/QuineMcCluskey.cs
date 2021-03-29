using System;
using System.Collections.Generic;
using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using System.Text;
using System.Linq;
using logic.ExpressionService.Common.QMC.Models;
using logic.ExpressionService.Common.Models;
using System.Diagnostics;

namespace logic.ExpressionService.Common.QMC
{
    public static class QuineMcCluskey
    {
        public static readonly char DONT_CARE = 'X';
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
        private static bool AreGroupsComparable(QMCGroupData data1, QMCGroupData data2)
        {
            /* 
             * Check if the number of rows which the data was produced is bigger than 1. 
             * For example if we produced the data by simplifying the values from 1st row and 3rd row we are having 
             * number of rows equal to 2 - 1,3.
             * 
             */
            if (data1.NumberOfRows() > 1 && data2.NumberOfRows() > 1)
            {
                /*
                 * If the values of the two data groups are produced by simpplifying more than one row
                 * the two data groups are comparable only if 
                 * the substraction of the last row and the first row in the pair of rows that produced the values of the first group is equal to the one of the second group.
                 * Ex: data group with values 00*1 produced by simplifying rows 1,3,5,7
                 * and data group with values 01*1 produced by simplifying rows 9,11,13,15 
                 * 7-1 is equal to 6 and so is 15-9, also the substraction of the Origin Rows which is 9-1 is 8 which is 
                 * number produced by multiplying 2 by itself 3 times. 
                 * Taking in mind these tow conditions, this means the two groups are comparable.
                 * 
                 *                                       IMPORTANT
                 * This is only the case when we have produced the values of the variables in the table group 
                 * are columns with 0/1 grouped as multiples of power of 2 using the number of variables in descending order
                 * 
                 */
                return data1.GetAbsoluteRowComparableIndex() == data2.GetAbsoluteRowComparableIndex()
                    && IsNumberPowerOf2(data2.OriginRow - data1.OriginRow);
            }
            else return IsNumberPowerOf2(data2.OriginRow - data1.OriginRow);
            
        }
        private static int GetDontCareIndex(QMCGroupData data1, QMCGroupData data2)
        {
            return data2.OriginRow - data1.OriginRow;
        }
        /// <summary>
        /// Function that gets the combined rows from the two data groups.
        /// </summary>
        /// <param name="data1">First data group to get the rows from</param>
        /// <param name="data2">Second data group to get the rows from</param>
        /// <returns>Returns the combined rows from the two data groups</returns>
        private static int[] GetNewRows(QMCGroupData data1, QMCGroupData data2)
        {
            if(data1.NumberOfRows() > 1 && data2.NumberOfRows() > 1)
            {
                int[] combined = new int[data1.NumberOfRows() + data2.NumberOfRows()];
                Array.Copy(data1.Rows, combined, data1.NumberOfRows());
                Array.Copy(data2.Rows, 0, combined, data1.NumberOfRows(), data2.NumberOfRows());
                return combined;
            }
            else
            {
                return new int[2] { data1.OriginRow, data2.OriginRow };
            }
        }

        /// <summary>
        /// Function that forms groups of number of 1s from the truth table values that need to be simplified.
        /// Ex: Group for values with only 1 positive variables, group for values with 2 positive variables and so on...
        /// </summary>
        /// <param name="reference">The values of the truth table that has to be simplified.</param>
        /// <returns>Formed gruops with keys the number of 1's in the data.</returns>
        private static Tuple<QMCGroups,HashSet<int>> FormGroupsForComparison(TruthTableValues reference)
        {
            // Groups with number of ones //
            var groups = new QMCGroups();
            var minterms = new HashSet<int>();
            var resultData = reference[reference.Keys.Last()];

            for (int i = 0; i < resultData.Count; i++)
            {
                if (resultData[i] == "1")
                {
                    minterms.Add(i);
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
            return new Tuple<QMCGroups, HashSet<int>>(groups,minterms);
        }
        private static List<QMCGroupData> GetComparableRows(List<QMCGroupData> groupToCompare, QMCGroupData data)
        {
            List<QMCGroupData> comparables = new List<QMCGroupData>();
            for (int i = 0; i < groupToCompare.Count; i++)
            {
                if(AreGroupsComparable(data, groupToCompare[i]))
                {
                    comparables.Add(groupToCompare[i]);
                }
            }
            return comparables;
        }
        public static IEnumerable<QMCGroupData> SimplifyTable(TruthTableValues reference)
        {

            var result = FormGroupsForComparison(reference);

            var groups = result.Item1;
            var minterms = result.Item2;

            Queue<KeyValuePair<int, List<QMCGroupData>>> queue = new Queue<KeyValuePair<int, List<QMCGroupData>>>();
            foreach (var group in groups)
            {
                queue.Enqueue(group);
            }
            HashSet<QMCGroupData> marked = new HashSet<QMCGroupData>();
            HashSet<QMCGroupData> simplifiedValues = new HashSet<QMCGroupData>();

            int indexShouldNotCompare = groups.Keys.Last();
            int nextKey = indexShouldNotCompare + 1;
            while (queue.Count >= 2)
            {
                var group1 = queue.Dequeue();
                var group2 = queue.Peek();

                if(group1.Key != indexShouldNotCompare)
                {
                    List<QMCGroupData> newGroupData = new List<QMCGroupData>();
                    for (int i = 0; i < group1.Value.Count; i++)
                    {
                        QMCGroupData data1 = group1.Value[i];
                        var comparables = GetComparableRows(group2.Value, data1);
                        if(comparables.Count > 0)
                        {
                            for (int j = 0; j < comparables.Count; j++)
                            {
                                QMCGroupData data2 = comparables[j];

                                var chars = data1.RowData.ToCharArray();
                                int indexToSimplify = chars.Length - 1 - (int)Math.Log2(GetDontCareIndex(data1, data2));
                                chars[indexToSimplify] = DONT_CARE;
                                string simplifiedData = new string(chars);

                                QMCGroupData data = new QMCGroupData(GetNewRows(data1, data2), simplifiedData);
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
                        }
                        else if (!simplifiedValues.Contains(data1) && !marked.Contains(data1))
                        {
                            simplifiedValues.Add(data1);
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
                    if(group1.Value.First().Rows.Length == 1)
                    {
                        foreach (var data in group1.Value)
                        {
                            if (!simplifiedValues.Contains(data) && !marked.Contains(data))
                            {
                                simplifiedValues.Add(data);
                            }
                        }
                    }
                }

            }
            return GetPrimeImplicants(simplifiedValues,minterms);
        }
  
        private static IEnumerable<QMCGroupData> GetPrimeImplicants(HashSet<QMCGroupData> simplifiedValues,HashSet<int> minterms)
        {
            Dictionary<int, QMCGroupData> primeImplicants = new Dictionary<int, QMCGroupData>();
            //Dictionary<int, HashSet<QMCGroupData>> implicants = new Dictionary<int, HashSet<QMCGroupData>>();
                //var repeatedRows = new HashSet<int>();
            while (minterms.Count > 0)
            {
                foreach (var m in minterms)
                {
                    var data = simplifiedValues.Where(v => v.Rows.Contains(m)).ToHashSet();

                    if (data.Count == 1)
                    {
                        primeImplicants.Add(m, data.First());
                    }
                }
                foreach (var pr in primeImplicants)
                {
                    simplifiedValues.Remove(pr.Value);
                    foreach (var row in pr.Value.Rows)
                    {
                        minterms.Remove(row);
                    }
                }
                    var newRows = new HashSet<QMCGroupData>();
                foreach (var m in minterms)
                {
                    var associatedRows = simplifiedValues.Where(v => v.Rows.Contains(m)).ToHashSet();
                    var dataWithMaxRows = new QMCGroupData();
                    foreach (var data in associatedRows)
                    {
                        var dataInMinterms = data.Rows.Intersect(minterms).Count();
                        var dataWithMaxRowsInMinterms = dataWithMaxRows.Rows.Intersect(minterms).Count();
                        if (dataInMinterms > dataWithMaxRowsInMinterms)
                            dataWithMaxRows = data;
                        else if(dataInMinterms == dataWithMaxRowsInMinterms && data.Rows.Length > dataWithMaxRows.Rows.Length)
                            dataWithMaxRows = data;
                    }
                    newRows.Add(dataWithMaxRows);
                }

                simplifiedValues.RemoveWhere(v => !newRows.Contains(v));
            }
            
            return primeImplicants.Values.Distinct();
        }
    }
}
