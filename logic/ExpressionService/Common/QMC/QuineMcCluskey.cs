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
        /// <returns>Formed gruops with keys the number of 1's in the data plus the minterms that are necessary for getting the prime implicants.</returns>
        private static Tuple<QMCGroups,HashSet<int>> FormGroupsWithMintermsForComparison(TruthTableValues reference)
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

        /// <summary>
        /// Function which evaluates which data values from passed collection of groups is comparable to the passed data 
        /// </summary>
        /// <param name="groupToCompare">Groups to evaluate and pick only the ones that are comparable</param>
        /// <param name="data">The data which the groups will be compared with</param>
        /// <returns>List of data values which are comparable to the passed <paramref name="data"/></returns>
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
        /// <summary>
        /// Function which simplifies the passed <see cref="TruthTableValues"/> for a <see cref="TruthTable"/>
        /// </summary>
        /// <param name="reference">The values of the table which has to be simplified</param>
        /// <returns>List of the essential prime implicants.</returns>
        public static IEnumerable<QMCGroupData> SimplifyTable(TruthTableValues reference)
        {
            // Form the groups and the minterms
            var result = FormGroupsWithMintermsForComparison(reference);

            //Assign the groups and the minterms
            var groups = result.Item1;
            var minterms = result.Item2;

            //Initialize a queue needed to iterate over the groups until no groups can be compared no more, and fill it with the initial groups
            Queue<KeyValuePair<int, List<QMCGroupData>>> queue = new Queue<KeyValuePair<int, List<QMCGroupData>>>();
            foreach (var group in groups)
            {
                queue.Enqueue(group);
            }

            //Set for keeping track of the values which were already simplified
            HashSet<QMCGroupData> marked = new HashSet<QMCGroupData>();

            //Set for keeping track of the prime latest simplified values
            HashSet<QMCGroupData> simplifiedValues = new HashSet<QMCGroupData>();

            //In order not to compare the last group with the first group from the completely newly formed groups which are different and not comparable to the previous
            //We keep track of the index of the last group of the currently evaluated groups
            int indexShouldNotCompare = groups.Keys.Last();

            //The key that has to be assigned to the eventually newly created group
            int nextKey = indexShouldNotCompare + 1;

            //Iterate through the groups until there are at least 2 groups available, we can't compare less than 2 groups
            while (queue.Count >= 2)
            {
                //Get the current group and the one that is following
                //NOTE: Groups are always sorted by the number of positive variables in the group
                var group1 = queue.Dequeue();
                var group2 = queue.Peek();

                if(group1.Key != indexShouldNotCompare)
                {
                    //The data for the newly created group
                    List<QMCGroupData> newGroupData = new List<QMCGroupData>();

                    for (int i = 0; i < group1.Value.Count; i++)
                    {
                        //Get the first values from the first group
                        QMCGroupData data1 = group1.Value[i];

                        //Find the comparable values from the second group with the first data values of the first group
                        var comparables = GetComparableRows(group2.Value, data1);

                        if(comparables.Count > 0)
                        {
                            //Iterate through the list of comparables
                            for (int j = 0; j < comparables.Count; j++)
                            {
                                QMCGroupData data2 = comparables[j];

                                //Get the don't care index and mark it in the data of the first group's data values
                                var chars = data1.RowData.ToCharArray();
                                int indexToSimplify = chars.Length - 1 - (int)Math.Log2(GetDontCareIndex(data1, data2));
                                chars[indexToSimplify] = DONT_CARE;
                                string simplifiedData = new string(chars);

                                //Create a new data object and put it in the latest simplified values and in the new group data values, if it is not there yet
                                QMCGroupData data = new QMCGroupData(GetNewRows(data1, data2), simplifiedData);
                                if (!simplifiedValues.Contains(data))
                                {
                                    newGroupData.Add(data);
                                    simplifiedValues.Add(data);
                                }

                                //Mark the data values that were simplified
                                marked.Add(data1);
                                marked.Add(data2);

                                //If the latest simplified values contains the data values that were simplified again,
                                //remove them from the set because we keep only the latest
                                if (simplifiedValues.Contains(data1))
                                    simplifiedValues.Remove(data1);

                                if (simplifiedValues.Contains(data2))
                                    simplifiedValues.Remove(data2);
                            }
                        }
                        else if (!simplifiedValues.Contains(data1) && !marked.Contains(data1))
                        {
                            //If the data values cannot be simplified, it still is count later as a prime implicant and it should be added in the simplified values.
                            //If later in the process it is simplified, it will be removed.
                            simplifiedValues.Add(data1);
                        }
                    }
                    
                    //Add the new group to the queue if it has any data values
                    if (newGroupData.Count > 0)
                        queue.Enqueue(new KeyValuePair<int, List<QMCGroupData>>(nextKey++, newGroupData));
                    else
                        nextKey++;
                }
                else
                {
                    //If the group should not be compared because it is the last one

                    //Set the next group key
                    indexShouldNotCompare = nextKey - 1;

                    //If the group has only one row, this means a group from the initial ones is not simplified, it should be added in the simplified values
                    //because it is still evaluated at the end as a prime implicant and it will not be compared anymore in the process of simplifying.
                    
                    //We add only the initial groups because if a group is already simplified it has more than 1 rows and it is already added in the simplified values.
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
            //Return the essential prime implicants
            return GetPrimeImplicants(simplifiedValues,minterms);
        }
  
        private static IEnumerable<QMCGroupData> GetPrimeImplicants(HashSet<QMCGroupData> simplifiedValues,HashSet<int> minterms)
        {
            //Initialize an empty dictionary to keep track of the prime implicants for each minterm
            Dictionary<int, QMCGroupData> primeImplicants = new Dictionary<int, QMCGroupData>();

            //Loop until there are no minterms left, this means all the prime implicants are reduced
            while (minterms.Count > 0)
            {
                //For each minterm get the simplified values which are produced if there is a single value, add it as an essential prime implicant
                foreach (var m in minterms)
                {
                    var data = simplifiedValues.Where(v => v.Rows.Contains(m)).ToHashSet();

                    if (data.Count == 1)
                    {
                        primeImplicants.Add(m, data.First());
                    }
                }
                //Remove from the simplified values each prime implicant, and also remove all the minterms that produced an essential prime implicant
                foreach (var pr in primeImplicants)
                {
                    simplifiedValues.Remove(pr.Value);
                    foreach (var row in pr.Value.Rows)
                    {
                        minterms.Remove(row);
                    }
                }
                //Collection to keep track of the new rows that can be evaluated
                var newRows = new HashSet<QMCGroupData>();
                foreach (var m in minterms)
                {
                    //For each minterm left, get the data values which have rows containing any of the minterms left.
                    //We have to evaluate and check for essential prime implicant only the data values that are produced by rows which contain any of the minterms left. 
                    var associatedRows = simplifiedValues.Where(v => v.Rows.Contains(m)).ToHashSet();

                    //After that we have to pick for each data that contains this minterm, the values that contain maximum number of minterms
                    //Ex: if we have data with rows:1,3,4,6 and values X0X01 and another data with rows: 1,5,7,11 with values 1X001
                    //    And let's say the left minterms are 1,3,9,12 - we are going to pick the first data because it contains more minterms in its rows - 1 and 3
                    //    the other contains just 1
                    var dataWithMaxRows = new QMCGroupData();
                    foreach (var data in associatedRows)
                    {
                        //Get the number of minterms that the data rows contains
                        var dataInMinterms = data.Rows.Intersect(minterms).Count();

                        //Get the number of minterms that the current max evaluated data rows contains
                        var dataWithMaxRowsInMinterms = dataWithMaxRows.Rows.Intersect(minterms).Count();

                        //Compare the two data values and consequently change the current max evaluated data if that is the case
                        if (dataInMinterms > dataWithMaxRowsInMinterms)
                            dataWithMaxRows = data;
                        else if(dataInMinterms == dataWithMaxRowsInMinterms && data.Rows.Length > dataWithMaxRows.Rows.Length)
                            dataWithMaxRows = data;
                    }
                    //Add the data with max minterms containing to the new rows that have to be evaluated
                    newRows.Add(dataWithMaxRows);
                }
                //Remove all the values from the simplified values that are not the new rows that can be evaluated
                //In this way we reduce the table and we leave only values that are candidates for essential prime implicants and have at least one minterm containing
                simplifiedValues.RemoveWhere(v => !newRows.Contains(v));
            }
            
            return primeImplicants.Values.Distinct();
        }
    }
}
