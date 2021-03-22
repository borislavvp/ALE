using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using logic.ExpressionService.Common.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Tests.ExpressionService.Models
{
    public enum TruthTableTestsMockPair
    {
        original,
        expected
    }
    public class TruthTableMocks
    {
        private static Mock<IBinaryExpressionTree> _mockTree = new Mock<IBinaryExpressionTree>();
        private readonly static TruthTableValues DEFAULT_VALUES = new TruthTableValues()
        {
            ["A"] = new List<string>() { "0", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "1", "1", "1", "1", "1" },
            ["B"] = new List<string>() { "0", "0", "0", "0", "1", "1", "1", "1", "0", "0", "0", "0", "1", "1", "1", "1" },
            ["C"] = new List<string>() { "0", "0", "1", "1", "0", "0", "1", "1", "0", "0", "1", "1", "0", "0", "1", "1" },
            ["D"] = new List<string>() { "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1", "0", "1" },
        };
        public Tuple<TruthTable, TruthTableValues> PAIR_ONE;
        public TruthTableMocks()
        {
            PAIR_ONE = INITIALIZE_PAIR_ONE();
        }
        private Tuple<TruthTable, TruthTableValues> INITIALIZE_PAIR_ONE()
        {
            Tuple<TruthTable, TruthTableValues> response;

            TruthTableValues values = DEFAULT_VALUES;
            values.Add("RESULT", new List<string>() { "0", "1", "1", "1", "0", "1", "0", "1", "1", "1", "0", "1", "1", "1", "0", "1" });
            TruthTable table = new TruthTable(_mockTree.Object, values);

            TruthTableValues expected_values = GetValuesWithoutPositiveResults(values);
            expected_values["RESULT"].AddRange(new string[] { "1", "1","1" });
            expected_values["A"].AddRange(new string[] { "*", "*","1" });
            expected_values["B"].AddRange(new string[] { "0", "*","*" });
            expected_values["C"].AddRange(new string[] { "*","0", "*" });
            expected_values["D"].AddRange(new string[] { "1","1", "1" });

            response = new Tuple<TruthTable, TruthTableValues>(table, expected_values);

            return response;
        }
        private static TruthTableValues GetValuesWithoutPositiveResults(TruthTableValues values)
        {
            TruthTableValues simplified = (TruthTableValues)values.Clone();

            var resultData = values[values.Keys.Last()];

            for (int i = 0; i < resultData.Count; i++)
            {
                if (resultData[i] == "1")
                {
                    simplified.DeleteRowValues(i - (values[values.Keys.Last()].Count - simplified[simplified.Keys.Last()].Count));
                }
            }
            return simplified;
        }
       
    }
}
