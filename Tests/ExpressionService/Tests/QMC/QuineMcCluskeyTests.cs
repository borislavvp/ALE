using FluentAssertions;
using logic.ExpressionService.Common.Models;
using NUnit.Framework;
using QuineMcCluskey;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tests.ExpressionService.Helpers;

namespace Tests.ExpressionService.Tests.QMC
{
    class QuineMcCluskeyTests
    {
        private static List<int> GetMaxMinterms(int numberOfVariables)
        {
            List<int> minterms = new List<int>();
            for (int i = 0; i < Math.Pow(2, numberOfVariables); i++)
            {
                minterms.Add(i);
            }
            return minterms;
        }

        private static readonly object[] MintermsList = {
            new object[] { new List<int> { 0, 2, 4, 6, 8, 10, 12, 14 } },
            new object[] { new List<int> { 1, 2, 3, 5, 8, 9, 11, 12, 13,14 } },
            new object[] { new List<int> { 1, 2, 3, 4, 5, 7, 8, 9, 11, 12, 15 } },
            new object[] { new List<int> { 0 ,3,6,7,11,15,16,17,19,21,25,27,28,29,31 } },
            //new object[] { new List<int> { 1 ,2,3,4,6,7,10,13,16,17,18,19,20,21,25,26,27,29,30 } },
            //new object[] { new List<int> { 0 ,1,2,3,5,7,8,10,12,13,14,15,17,18,19,23,24,25,27,28,29,31 } },
            //new object[] { new List<int> { 0 ,1,2,3,5,6,7,8,9,10,11,12,13,14,15,17,18,19,20,23,24,25,27,29,31 } },
            //new object[] { new List<int> { 2 ,8,9,11,14,16,17,20,22,23,24,26,32,35,40,41,42,43,45,47,49,50,53,54,55,57,58,59,62,63 } },
            //new object[] { new List<int> { 0 ,1,2,3,6,8,11,12,13,14,15,17,19,24,25,29,33,36,37,39,40,41,42,45,46,49,50,52,53,55,56,58,60,62 } },
            //new object[] { new List<int> { 0 ,1,4,5,7,11,15,17,19,20,24,25,29,31,32,34,43,45,47,48,50,51,56,57,58,60,61 } },
            //new object[] { new List<int> { 6 ,7,9,11,17,18,19,20,22,23,24,26,32,35,40,41,42,43,45,47,49,50,53,54,55,57,58,59,62,63 } },
            //new object[] { new List<int> { 7 ,5,9,12,18,19,20,21,22,23,26,29,31,32,35,40,42,43,46,48,49,50,51,53,54,57,58,59,60,63 } },
            //new object[] { new List<int> { 2 ,4,9,13,14,16,19,21,22,23,24,25,30,32,40,42,43,45,46,48,52,53,54,56,57,58,60,61,62,63 } },
            new object[] { new List<int> { 1,2,3,5,11,12,13,14,15,27,28,29,30,31 } },
            new object[] { GetMaxMinterms(2) },
            new object[] { GetMaxMinterms(3) },
            new object[] { GetMaxMinterms(4) },
            new object[] { GetMaxMinterms(5) },
            new object[] { GetMaxMinterms(6) },
            new object[] { GetMaxMinterms(7) },
            new object[] { GetMaxMinterms(8) },
            new object[] { GetMaxMinterms(9) },
            new object[] { GetMaxMinterms(10) },
        };


        [Test]
        [TestCaseSource(nameof(MintermsList))]
        public void BuildExpressionTree_Should_Create_ExpressionTree_From_PrefixExpression(List<int> minterms)
        {
            GC.Collect();
            Stopwatch s2 = Stopwatch.StartNew();
            var externalLibraryResult = QuineMcCluskeySolver.QMC_Solve(minterms, new int[] { });
            s2.Stop();

            TruthTable table = TruthTableHelpers.GenerateTruthTable(minterms);
            Stopwatch s1 = Stopwatch.StartNew();
            var customLibraryResult = logic.ExpressionService.Common.QMC.QuineMcCluskey.SimplifyTable(table.Value);
            s1.Stop();

            TestContext.Out.WriteLine(s1.ElapsedTicks);
            TestContext.Out.WriteLine(s2.ElapsedTicks);

            var helperSet = new HashSet<string>();
            foreach (var pr in customLibraryResult)
            {
                helperSet.Add(pr.RowData);
            }

            foreach (var item in externalLibraryResult)
            {
                helperSet.Should().Contain(item.ToString());
            }
        }
    }
}
