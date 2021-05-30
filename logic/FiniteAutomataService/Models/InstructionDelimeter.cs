using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public static class InstructionDelimeter
    {
        public const string INSTRUCTION_VALUE = ":";
        public const string TRANSITION_FROM_TO = "-->";
        public const string TRANSITION_FROM_VALUES = ",";
        public const string STATES_VALUES = ",";
        public const string FINAL_STATES_VALUES = ",";
        public const string ALPHABET_VALUES = "";
    }
}
