using FluentAssertions;
using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using logic.ExpressionService.Common.QMC;
using Moq;
using NUnit.Framework;
using QuineMcCluskey;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Tests.ExpressionService.Helpers;

namespace Tests.ExpressionService.Tests.Models
{
    class TruthTableTests
    {
        private static string VARIABLE_RESULT = "RESULT";
        
        private static readonly object[] TRUTHTABLE_RESULTS_WITH_HEX = {
            new object[] { new TruthTableValues() {{ VARIABLE_RESULT,new List<string>(){"0","1","1","1","1","1","1","1"}}},"7F" },
            new object[] { new TruthTableValues() {{ VARIABLE_RESULT,new List<string>(){"0","0","1","0","1","1","1","0"}}},"2E" },
            new object[] { new TruthTableValues() {{ VARIABLE_RESULT,new List<string>(){"1","1","1","1","1","0","0","1"}}},"F9" },
            new object[] { new TruthTableValues() {{ VARIABLE_RESULT,new List<string>(){"0","1","1","1","1","1","1","1","1","1","1","1","0","1","1","1" } }},"7FF7" },
            new object[] { new TruthTableValues() {{ VARIABLE_RESULT,new List<string>(){"1","1","1","1","1","1","1","1","1","1","1","1","1","1","1","1" } }},"FFFF" },
            new object[] { new TruthTableValues() {{ VARIABLE_RESULT,new List<string>(){"1","0","0","0","0","0","1","1","0","1","0","1","1","0","1","0" } }},"835A" },
        };

        [Test]
        public void Should_Duplicate_TruthTableValues_Without_Reference()
        {
            TruthTableValues values1 = new TruthTableValues();
            TruthTableValues values2 = (TruthTableValues)values1.Clone();

            values1.Add("dummy_key", new List<string>());
            values1.Should().NotBeSameAs(values2);
        }

        [Test]
        [TestCaseSource(nameof(TRUTHTABLE_RESULTS_WITH_HEX))]
        public void Should_Calculate_And_Return_Expected_Hex_Value_From_TruthTable_Result_Column(TruthTableValues values,string expectedResult)
        {
            TruthTable table = new TruthTable(new Mock<IBinaryExpressionTree>().Object, values);
            table.CalculateHexResult().Should().BeEquivalentTo(expectedResult);
        }
    }
}
