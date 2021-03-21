using System;
using System.Collections.Generic;
using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using System.Text;
using System.Linq;

namespace logic.ExpressionService.Common.Models
{
    public class QMCGroup : Dictionary<int, HashSet<QMCGroupData>>
    {
        public QMCGroup() : base() { }
        public QMCGroup(int capacity) : base(capacity) { }
    }

    public class QMCGroupData
    {
        public string Rows { get; set; }
        public string RowData { get; set; }
        public QMCGroupData()
        {

        }
        public QMCGroupData(string Rows, string RowData)
        {
            this.Rows = Rows;
            this.RowData = RowData;
        }

        public override bool Equals(object obj)
        {
            return obj is QMCGroupData data &&
                   RowData == data.RowData;
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
                        groups.Add(GroupNumber, new HashSet<QMCGroupData>() { new QMCGroupData(@$"{i}", String.Join("", rowValues)) });
                    }
                    else
                    {
                        groups[GroupNumber].Add(new QMCGroupData(@$"{i}", String.Join("", rowValues)));
                    }

                    Simplified.DeleteRowValues(i - (resultData.Count - Simplified[Simplified.Keys.Last()].Count));
                }
            }
            var marked = new HashSet<QMCGroupData>();
            var simplifiedValues = new HashSet<QMCGroupData>();

            int indexShouldNotCompare = groups.Keys.Count;
            for (int i = 1; i < groups.Keys.Count; i++)
            {
                int nextKey = groups.Keys.Max() + i;
                if (indexShouldNotCompare == i)
                {
                    indexShouldNotCompare = groups.Keys.Count;
                }
                else
                {
                    foreach (var g in groups.ElementAt(i - 1).Value)
                    {
                        foreach (var g2 in groups.ElementAt(i).Value)
                        {
                            int? res = compareGroups(g, g2);
                            if (res.HasValue)
                            {
                                var evaluated = g.RowData.ToCharArray();
                                evaluated[res.Value] = '*';
                                string simplified = new string(evaluated);

                                simplifiedValues.Add(new QMCGroupData(g.Rows +","+ g2.Rows, simplified));
                                if (!groups.ContainsKey(nextKey))
                                {
                                    groups.Add(groups.Keys.Max() + i, new HashSet<QMCGroupData>() { new QMCGroupData(g.Rows + "," + g2.Rows, simplified) });
                                }
                                else
                                {
                                    groups[nextKey].Add(new QMCGroupData(g.Rows + "," + g2.Rows, simplified));
                                }
                                marked.Add(g);
                                marked.Add(g2);

                                if (simplifiedValues.Contains(g))
                                    simplifiedValues.Remove(g);

                                if (simplifiedValues.Contains(g2))
                                    simplifiedValues.Remove(g2);
                            }
                            else if (!simplifiedValues.Contains(g) && !marked.Contains(g))
                            {
                                simplifiedValues.Add(g);
                            }
                        }
                    }
                }
            }

            var primeImplicants = GetPrimeImplicants(simplifiedValues);
            foreach (var res in GetPrimeImplicants(simplifiedValues))
            {
                Simplified[Simplified.Keys.Last()].Add("1");
                for (int i = 0; i < res.RowData.Length; i++)
                {
                    Simplified.ElementAt(i).Value.Add(res.RowData[i].ToString());
                }
            }
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
