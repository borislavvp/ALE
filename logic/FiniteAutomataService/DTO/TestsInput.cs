using logic.FiniteAutomataService.Constants;
using logic.FiniteAutomataService.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class TestsInput
    {
        public string value { get; set; }

        public bool GetDFATestCase()
        {
            var result = RegexHelper.Match(this.value, TestCase.DFA);

            return result.Trim().Equals(TestCase.TEST_GUESS_TRUE);
        }
        public bool GetFiniteTestCase()
        {
            var result = RegexHelper.Match(this.value, TestCase.FINITE);

            return result.Trim().Equals(TestCase.TEST_GUESS_TRUE);
        }

        public string[] GetWordsTestCase()
        {
            var result = RegexHelper.Match(this.value, TestCase.WORDS);

            return result.Split("\n");
        }
    }
}
