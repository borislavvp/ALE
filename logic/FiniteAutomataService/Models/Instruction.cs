using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public static class Instruction
    {
        public const string UNKONWN = "";
        public const string ALPHABET = "alphabet";
        public const string REGEX = "regex";
        public const string STATES = "states";
        public const string FINAL_STATES = "final";
        public const string TRANSITIONS = "transitions";
        public const string WORDS = "words";
        public const string END = "end.";
    }
}
