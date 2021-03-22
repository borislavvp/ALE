using System;
using System.Collections.Generic;
using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using System.Text;
using System.Linq;

namespace logic.ExpressionService.Common.Models
{
    public class QMCGroup : Dictionary<int, List<QMCGroupData>>
    {
        public QMCGroup() : base() { }
        public QMCGroup(int capacity) : base(capacity) { }
    }

    public class QMCGroupData
    {
        public int OriginRow { get; set; }
        public string[] Rows { get; set; }
        public string RowData { get; set; }
        public char RowsDelimeter { get; } = ',';

        public QMCGroupData()
        {

        }
        public QMCGroupData(int Origin, string RowData)
        {
            this.OriginRow = Origin;
            this.Rows = new string[1] { $@"{Origin}" };
            this.RowData = RowData;
        }
        public QMCGroupData(string[] Rows, string RowData)
        {
            this.OriginRow = Int32.Parse(Rows[0]);
            this.Rows = Rows;
            this.RowData = RowData;
        }

        public override bool Equals(object obj)
        {
            return obj is QMCGroupData data &&
                   RowData == data.RowData;
        }

        public int GetAbsoluteRowComparableIndex()
        {
            return Int32.Parse(this.Rows[1]) - Int32.Parse(this.Rows[0]);
        }

        public int NumberOfRows()
        {
            return this.Rows.Length;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowData);
        }

        public static bool operator ==(QMCGroupData left, QMCGroupData right)
        {
            return EqualityComparer<QMCGroupData>.Default.Equals(left, right);
        }

        public static bool operator !=(QMCGroupData left, QMCGroupData right)
        {
            return !(left == right);
        }
    }
    public static class QuineMcCluskey
    {
        private static bool IsNumberPowerOf2(int number)
        {
            return (number >= 0) && ((number & (number - 1)) == 0);
        }
        private static int? AreGroupsComparable(QMCGroupData data1, QMCGroupData data2)
        {
            if(data1.NumberOfRows() > 1 && data2.NumberOfRows() > 1)
            {
                if(data1.GetAbsoluteRowComparableIndex() - data2.GetAbsoluteRowComparableIndex() == 0)
                {
                    if(IsNumberPowerOf2(data2.OriginRow - data1.OriginRow)
                        && data1.Rows[data1.NumberOfRows()-2] != data2.Rows[data2.NumberOfRows() - 2] 
                        && data1.Rows[data2.NumberOfRows() - 1] != data2.Rows[data2.NumberOfRows() - 1])
                    {
                        return data2.OriginRow - data1.OriginRow;
                    }
                    else
                    {
                        return null;
                    }
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
        private static string[] GetNewRows(QMCGroupData data1, QMCGroupData data2)
        {
            if(data1.NumberOfRows() > 1 && data2.NumberOfRows() > 1)
            {
                string[] combined = new string[data1.NumberOfRows() + data2.NumberOfRows()];
                Array.Copy(data1.Rows, combined, data2.NumberOfRows());
                Array.Copy(data2.Rows, 0, combined, data1.NumberOfRows(), data2.NumberOfRows());
                return combined;
            }
            else
            {
                return new string[2] { @$"{data1.OriginRow}", $@"{data2.OriginRow}" };
            }
        }
        public static TruthTableValues Simplify(TruthTableValues reference)
        {
            TruthTableValues Simplified = (TruthTableValues)reference.Clone();
            // Groups with number of ones //
            var groups = new QMCGroup();

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

                    Simplified.DeleteRowValues(i - (resultData.Count - Simplified[Simplified.Keys.Last()].Count));
                }
            }
            Queue<KeyValuePair<int, List<QMCGroupData>>> queue = new Queue<KeyValuePair<int, List<QMCGroupData>>>();
            foreach (var group in groups)
            {
                queue.Enqueue(group);
            }

            var marked = new HashSet<QMCGroupData>();
            var simplifiedValues = new HashSet<QMCGroupData>();

            int indexShouldNotCompare = queue.Count;
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

                        /** The value we compare with should always be bigger if there is a match */
                        //int rowComparisonValue = data2.GetAbsoluteRowComparableIndex() - data1.GetAbsoluteRowComparableIndex() ;
                        int? comparisonResult = AreGroupsComparable(data1, data2);
                        if (comparisonResult.HasValue)
                        {
                            //string combinedRows = data1.Rows + data1.RowsDelimeter + data2.Rows;
                            
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
                    queue.Enqueue(new KeyValuePair<int, List<QMCGroupData>>(nextKey++, newGroupData));
                }
                else
                {
                    indexShouldNotCompare = nextKey - 1;
                }

            }
            //var resultData = reference[reference.Keys.Last()];

            //for (int i = 0; i < resultData.Count; i++)
            //{
            //    if (resultData[i] == "1")
            //    {
            //        var rowValues = reference.GetRowValuesWithouthResultColumn(i);
            //        int GroupNumber = rowValues.FindAll(n => n == "1").Count;
            //        if (!groups.ContainsKey(GroupNumber))
            //        {
            //            groups.Add(GroupNumber, new HashSet<QMCGroupData>() { new QMCGroupData(@$"{i}", String.Join("", rowValues)) });
            //        }
            //        else
            //        {
            //            groups[GroupNumber].Add(new QMCGroupData(@$"{i}", String.Join("", rowValues)));
            //        }

            //        Simplified.DeleteRowValues(i - (resultData.Count - Simplified[Simplified.Keys.Last()].Count));
            //    }
            //}
            //var marked = new HashSet<QMCGroupData>();
            //var simplifiedValues = new HashSet<QMCGroupData>();

            //int indexShouldNotCompare = groups.Keys.Count;
            //for (int i = 1; i < groups.Keys.Count; i++)
            //{
            //    int nextKey = groups.Keys.Max() + i;
            //    if (indexShouldNotCompare == i)
            //    {
            //        indexShouldNotCompare = groups.Keys.Count;
            //    }
            //    else
            //    {
            //        foreach (var g in groups.ElementAt(i - 1).Value)
            //        {
            //            foreach (var g2 in groups.ElementAt(i).Value)
            //            {
            //                int? res = compareGroups(g, g2);
            //                if (res.HasValue)
            //                {
            //                    var evaluated = g.RowData.ToCharArray();
            //                    evaluated[res.Value] = '*';
            //                    string simplified = new string(evaluated);

            //                    simplifiedValues.Add(new QMCGroupData(g.Rows +","+ g2.Rows, simplified));
            //                    if (!groups.ContainsKey(nextKey))
            //                    {
            //                        groups.Add(groups.Keys.Max() + i, new HashSet<QMCGroupData>() { new QMCGroupData(g.Rows + "," + g2.Rows, simplified) });
            //                    }
            //                    else
            //                    {
            //                        groups[nextKey].Add(new QMCGroupData(g.Rows + "," + g2.Rows, simplified));
            //                    }
            //                    marked.Add(g);
            //                    marked.Add(g2);

            //                    if (simplifiedValues.Contains(g))
            //                        simplifiedValues.Remove(g);

            //                    if (simplifiedValues.Contains(g2))
            //                        simplifiedValues.Remove(g2);
            //                }
            //                else if (!simplifiedValues.Contains(g) && !marked.Contains(g))
            //                {
            //                    simplifiedValues.Add(g);
            //                }
            //            }
            //        }
            //    }
            //}

            //var primeImplicants = GetPrimeImplicants(simplifiedValues);
            //foreach (var res in GetPrimeImplicants(simplifiedValues))
            //{
            //    Simplified[Simplified.Keys.Last()].Add("1");
            //    for (int i = 0; i < res.RowData.Length; i++)
            //    {
            //        Simplified.ElementAt(i).Value.Add(res.RowData[i].ToString());
            //    }
            //}
            return Simplified;
        }
        private static HashSet<QMCGroupData> GetPrimeImplicants(HashSet<QMCGroupData> simplifiedValues)
        {
            string combinedRows = "";
            foreach (var values in simplifiedValues)
            {
                combinedRows += "," + values.Rows;
            }
            string[] parsedCombinedRows = combinedRows.TrimStart(',').Split(",");

            Dictionary<string,int> rowsCount = new Dictionary<string, int>();
            foreach (var row in parsedCombinedRows)
            {
                if (rowsCount.ContainsKey(row)) {
                    rowsCount[row]++;
                } 
                else {
                    rowsCount.Add(row, 1);
                };
            }
            
            HashSet<QMCGroupData> primeImplicants = new HashSet<QMCGroupData>();
            foreach (var values in simplifiedValues)
            {
                foreach (var pair in rowsCount)
                {
                    if(pair.Value == 1 && values.Rows.Contains(pair.Key))
                    {
                        primeImplicants.Add(values);
                        break;
                    }
                }
            }
            return primeImplicants;
        }
        private static int? compareGroups(QMCGroupData group1, QMCGroupData group2)
        {
            int c = 0;
            int dont_care_index = -1;
            for (int i = 0; i < group1.RowData.Length; i++)
            {
                if (group1.RowData[i] != group2.RowData[i])
                {
                    dont_care_index = i;
                    c++;
                    if (c > 1) break;
                }
            }
            if (c != 1)
            {
                return null;
            }
            else
            {
                return dont_care_index;
            }
        }
    }
}
