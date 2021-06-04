using logic.FiniteAutomataService.Constants;
using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Helpers;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace logic.FiniteAutomataService.Extensions
{
    public static class InstructionsExtensions
    {
        public static IAlphabet PopulateAlphabet(this InstructionsInput input)
        {
            var result = RegexHelper.Match(input.instructions, Instruction.ALPHABET);

            var letters = result.Trim().ToCharArray();

            return new Alphabet
            {
                Letters = letters.Select(l => new Letter(l)).ToHashSet<ILetter>()
            };
        }
        public static string GetRegex(this InstructionsInput input)
        {
            return RegexHelper.Match(input.instructions, Instruction.REGEX);
        }
        public static HashSet<IState> PopulateStates(this InstructionsInput input)
        {
            var result = RegexHelper.Match(input.instructions, Instruction.STATES);
           
            var statesInput = result.Trim().Split(InstructionDelimeter.STATES_VALUES);
            HashSet<IState> states = new HashSet<IState>();

            for (int i = 0; i < statesInput.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(statesInput[i]))
                {
                    states.Add(new State(i, statesInput[i], i == 0));
                }
            }
            return states;
        }
    }
}
