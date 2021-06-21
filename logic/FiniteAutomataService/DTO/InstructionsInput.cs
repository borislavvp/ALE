using logic.FiniteAutomataService.Constants;
using logic.FiniteAutomataService.Helpers;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class InstructionsInput
    {
        public string instructions { get; set; }

        public IAlphabet PopulateAlphabet()
        {
            var result = RegexHelper.Match(this.instructions, Instruction.ALPHABET);

            var letters = result.Trim().ToCharArray();

            return new Alphabet
            {
                Letters = letters.Select(l => new Letter(l)).ToHashSet<ILetter>()
            };
        }
        public string GetRegex()
        {
            return RegexHelper.Match(this.instructions, Instruction.REGEX);
        }
        public HashSet<IState> PopulateStates()
        {
            var result = RegexHelper.Match(this.instructions, Instruction.STATES);

            var statesInput = result.Trim().Split(InstructionDelimeter.STATES_VALUES);
            HashSet<IState> states = new HashSet<IState>();

            for (int i = 0; i < statesInput.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(statesInput[i]))
                {
                    states.Add(new State(i + 1, statesInput[i], i == 0));
                }
            }
            return states;
        }
    }
}
