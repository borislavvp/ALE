using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.QMC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public class TruthTable
    {
        public IBinaryExpressionTree Tree { get; set; }
        public TruthTableValues Value { get; set; }
        public string HexResult { get; set; }
        public TruthTable(IBinaryExpressionTree tree, TruthTableValues Value)
        {
            this.Tree = tree;
            this.Value = Value;
            this.HexResult = CalculateHexResult();
        }

        public string CalculateHexResult()
        {
            string binaryNumber = String.Join("", Value[Value.Keys.Last()]);

            int rest = binaryNumber.Length % 4;
            if (rest != 0)
                binaryNumber = new string('0', 4 - rest) + binaryNumber;

            string output = "";

            for (int i = 0; i <= binaryNumber.Length - 4; i += 4)
            {
                output += string.Format("{0:X}", Convert.ToByte(binaryNumber.Substring(i, 4), 2));
            }

            return output;
        }

        public TruthTableValues Simplify()
        {
            /* Get a clone version of the table that is going to be simplified */
            var Simplified = this.Value.Clone() as TruthTableValues;

            /* CalculatePrimeImplicants the prime implicant values with the QuineMcCluskey algorithm*/
            var primeImplicants = QuineMcCluskey.SimplifyTable(this.Value);

            /* Get the number of rows so we can iterate through them and remove the positive results from the table */
            int numberOfRows = Simplified.First().Value.Count();

            /* Remove the positive resulte from the table */
            for (int i = 0; i < numberOfRows; i++)
            {
                Simplified.DeleteRowValues(i - (numberOfRows - Simplified.First().Value.Count));
            }

            /* Fill the prime implicant values into the table*/
            foreach (var res in primeImplicants)
            {
                Simplified[Simplified.Keys.Last()].Add("1");
                for (int i = 0; i < res.RowData.Length; i++)
                {
                    Simplified.ElementAt(i).Value.Add(res.RowData[i].ToString());
                }
            }

            return Simplified;
        }
    }
}
