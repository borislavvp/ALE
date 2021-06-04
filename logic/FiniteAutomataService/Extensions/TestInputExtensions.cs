using logic.FiniteAutomataService.Constants;
using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Helpers;
using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Extensions
{
    public static class TestInputExtensions
    {
        public static bool GetDFATestCase(this TestsInput input)
        {
            var result = RegexHelper.Match(input.value, TestCase.DFA);

            return result.Trim().Equals(TestCase.TEST_GUESS_TRUE);
        }
        
        public static bool GetFiniteTestCase(this TestsInput input)
        {
            var result = RegexHelper.Match(input.value, TestCase.FINITE);

            return result.Trim().Equals(TestCase.TEST_GUESS_TRUE);
        }
        
        public static string[] GetWordsTestCase(this TestsInput input)
        {
            var result = RegexHelper.Match(input.value, TestCase.WORDS);

            return result.Split("\n");
        }
    }
}
