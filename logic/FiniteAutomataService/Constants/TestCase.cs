using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Constants
{
    public static class TestCase
    {
        public const string UNKNOWN = "";
        public const string DFA = "(?<=dfa:)(.*?)\n|\r|\r\n";
        public const string FINITE = "(?<=finite:)(.*?)\n|\r|\r\n";
        public const string WORDS = "(?<=words:)(\\s.*?)*(?=end.)";
        public const string TEST_GUESS_TRUE = "y";
        public const string TEST_GUESS_FALSE = "n";
        public const string CASE_DELIMETER = ":";
        public const string WORDS_DELIMETER = ",";
    }
}
