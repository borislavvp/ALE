using logic.ExpressionService.Common.Models;
using logic.ExpressionService.Common.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Tests.ExpressionService.Tests.Extensions
{
    class TruthTableExtensions
    {
        private static char VARIABLE_A = 'A';
        private static char VARIABLE_B = 'B';
        private static char VARIABLE_C = 'C';
        private static string VARIABLE_RESULT = "RESULT";
        private static TruthTableValues VALID_TRUTHTABLE_VALUES = new TruthTableValues()
        {
            {@$"{VARIABLE_A}",new List<string>(){"0","0","0","0","1","1","1","1"} },
            {@$"{VARIABLE_B}",new List<string>(){"0","0","1","1","0","0","1","1"} },
            {@$"{VARIABLE_C}",new List<string>(){"0","1","0","1","0","1","0","1"} },
            {VARIABLE_RESULT,new List<string>(){"0","1","1","1","1","1","1","1"} },
        };
        private static readonly object[] TABLE_VALUES_PER_ROW = {
            new object[] { VALID_TRUTHTABLE_VALUES, 0,new List<string>(){ "0","0","0"} },
            new object[] { VALID_TRUTHTABLE_VALUES, 1,new List<string>(){ "0","0","1"} },
            new object[] { VALID_TRUTHTABLE_VALUES, 2,new List<string>(){ "0","1","0"} },
            new object[] { VALID_TRUTHTABLE_VALUES, 3,new List<string>(){ "0","1","1"} },
            new object[] { VALID_TRUTHTABLE_VALUES, 4,new List<string>(){ "1","0","0"} },
            new object[] { VALID_TRUTHTABLE_VALUES, 5,new List<string>(){ "1","0","1"} },
            new object[] { VALID_TRUTHTABLE_VALUES, 6,new List<string>(){ "1","1","0"} },
            new object[] { VALID_TRUTHTABLE_VALUES, 7,new List<string>(){ "1","1","1"} },
        };

        [Test]
        [TestCaseSource(nameof(TABLE_VALUES_PER_ROW))]
        public void Should_Return_List_Of_Values_For_Specified_Row_In_Specified_TruthTable_Values_Without_Result_Column_Values(
            TruthTableValues tableValues,
            int row,
            List<string> expectedResult)
        {
            tableValues.GetRowValuesWithouthResultColumn(row).Should().BeEquivalentTo(expectedResult);
        } 
        
        [Test]
        public void Should_Build_Valid_TruthTable_With_Passed_Valid_ExpressionTree()
        {
            var tree = new BinaryExpressionTree(new Node('|', 1),
                           new BinaryExpressionTree(new Node('|', 2),
                               new BinaryExpressionTree(new Node(VARIABLE_A, 3,true)),
                               new BinaryExpressionTree(new Node(VARIABLE_B, 3,true))
                           ),
                           new BinaryExpressionTree(new Node('|', 2),
                               new BinaryExpressionTree(new Node(VARIABLE_A, 3,true)),
                               new BinaryExpressionTree(new Node(VARIABLE_C, 3,true))
                           )
                       );

            tree.BuildTruthTable(VARIABLE_RESULT).Value.Should().BeEquivalentTo(VALID_TRUTHTABLE_VALUES);
        }
    }
}
