using FluentAssertions;
using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using Moq;
using NUnit.Framework;
using QuineMcCluskey;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ExpressionService.Models
{
    class TruthTableTests
    {
        private static readonly TruthTableMocks _mocks = new TruthTableMocks();
        private static readonly object[] asd = {
            new object[] { _mocks.PAIR_ONE.Item1, _mocks.PAIR_ONE.Item2},  
        };
        [Test]
        [TestCaseSource(nameof(asd))]
        public void BuildExpressionTree_Should_Create_ExpressionTree_From_PrefixExpression(TruthTable original, TruthTableValues simplified)
        {
            var test = original.Simplify();
            test.Should().BeEquivalentTo(simplified);
            var list = new List<int>() {1,2,3,5,9,11,12,13,15 };

           var loops = QuineMcCluskeySolver.QMC_Solve(list, new int[] { });

        }

        [Test]
        public void test()
        {
            int res = (int)Math.Log2(2);
            string asd = "213";
            var asd2 = new string(asd);

        }
    }
}
