using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Constants
{
    public static class Instruction
    {
        //public const string UNKONWN = "";
        //public const string ALPHABET = "alphabet";
        //public const string REGEX = "regex";
        //public const string STATES = "states";
        //public const string FINAL_STATES = "final";
        //public const string TRANSITIONS = "transitions";
        //public const string END = "end.";
        public const string STATES = "(?<=states:)(.*?)\n|\r|\r\n";
        public const string ALPHABET = "(?<=alphabet:)(.*?)\n|\r|\r\n";
        public const string REGEX = "(?<=regex:)(.*?)\n|\r|\r\n";
        public const string FINAL_STATES = "(?<=final:)(.*?)\n|\r|\r\n";
        public const string TRANSITIONS = "(?<=transitions:)([\\S\\s].*?)*(?=end.)";
    }
}
