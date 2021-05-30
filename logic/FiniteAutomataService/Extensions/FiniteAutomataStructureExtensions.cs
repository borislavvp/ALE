using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using logic.FiniteAutomataService.Models.TompsonConstruction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.FiniteAutomataService.Extensions
{
    public static class FiniteAutomataStructureExtensions
    {
        public static IAlphabet ParseAlphabetFromInstructionsInput(string input)
        {
            var letters = input.Trim().ToCharArray();

            return new Alphabet
            {
                Letters = letters.Select(l => new Letter(l)).ToHashSet<ILetter>()
            };
        }
        public static void PopulateStatesFromInstructionsInput(this SortedSet<IState> states,string input)
        {
            var statesInput = input.Trim().Split(InstructionDelimeter.STATES_VALUES);
           
            for (int i = 0; i < statesInput.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(statesInput[i]))
                {
                    states.Add(new State(i,statesInput[i], i == 0));
                }

            }
        } 
        public static void MarkFinalStatesFromInstructionsInput(this SortedSet<IState> states,string input)
        {
            var finalStates = input.Trim().Split(InstructionDelimeter.FINAL_STATES_VALUES);
            foreach (var state in states)
            {
                foreach (var finalState in finalStates)
                {
                    if (state.Value.Equals(finalState))
                    {
                        state.Final = true;
                    }
                }
            }
        }
        private static void PopulateTransitionsFromInstructionsInput(this IFiniteAutomataStructure structure, string input)
        {
            var transitionGroup = input.Trim().Split(InstructionDelimeter.TRANSITION_FROM_TO);
            var transitionFromValues = transitionGroup[0].Trim().Split(InstructionDelimeter.TRANSITION_FROM_VALUES);
            var toStateValue = transitionGroup[1].Trim();

            var fromStateValue = transitionFromValues[0].Trim();
            var transitionLetter = new Letter(transitionFromValues[1].Trim().ToCharArray()[0]);

            if (transitionLetter.IsEpsilon && !structure.StructureAlphabet.Letters.Contains(transitionLetter)) 
                structure.StructureAlphabet.Letters.Add(transitionLetter);

            if(structure.StructureAlphabet.Letters.Contains(transitionLetter))
            {
                foreach (var fromState in structure.States)
                {
                    if (fromState.Value.Equals(fromStateValue))
                    {
                        foreach (var toState in structure.States)
                        {
                            if (toState.Value.Equals(toStateValue))
                            {
                                if (fromState.Directions.ContainsKey(toState))
                                    fromState.Directions[toState].Add(transitionLetter);
                                else
                                    fromState.Directions.Add(toState, new HashSet<ILetter>() { transitionLetter });
                            }
                        }
                    }
                }
            }

        }

        private static bool WordExists(this IFiniteAutomataStructure structure, string word)
        {
            return CheckWordExistenceWithDirections(
                structure.GetInitialState(),
                structure.GetInitialState(),
                new Dictionary<IState, HashSet<IState>>(),
                word,
                0);
        }
        private static bool CheckWordExistenceWithDirections(this IState state, IState previous, Dictionary<IState, HashSet<IState>> traversed, string word,int letterToCheck)
        {
            if (traversed.Keys.Contains(previous))
            {
                traversed[previous].Add(state);
            }
            else
            {
                traversed.Add(previous, new HashSet<IState>() { state });
            }
            foreach (var direction in state.Directions)
            {
                if (letterToCheck < word.Length && direction.Value.Contains(new Letter(word[letterToCheck])))
                {
                    if (CheckWordExistenceWithDirections(direction.Key,state, traversed, word, letterToCheck + 1))
                        return true;
                }
                else if(
                    direction.Value.Contains(Alphabet.EPSILON_LETTER) && 
                    direction.Key != state && 
                    direction.Key != previous && (!traversed.Keys.Contains(direction.Key) || !traversed[direction.Key].Contains(previous)))
                {
                    if (CheckWordExistenceWithDirections(direction.Key,state, traversed, word, letterToCheck))
                        return true;
                }
               
            }
            return state.Final && letterToCheck >= word.Length;
        }

        public static IFiniteAutomataStructure BuildStructure(this InstructionsInput input)
        {
            IFiniteAutomataStructure structure = new FiniteAutomataStructure();
            var instructions = input.instructions.Split("\n");
            foreach (var inputValue in instructions)
            {
                if (!String.IsNullOrWhiteSpace(inputValue))
                {
                    var value = inputValue.Trim();

                    string instruction;
                    if (value.Contains(InstructionDelimeter.TRANSITION_FROM_TO))
                    {
                        instruction = Instruction.TRANSITIONS;
                    }
                    else
                    {
                        var InstructionValue = inputValue.Split(InstructionDelimeter.INSTRUCTION_VALUE);
                        instruction = InstructionValue[0].Trim();
                        value = InstructionValue.Length > 1 ? InstructionValue[1].Trim() : null;
                    }
                    if (!String.IsNullOrWhiteSpace(value))
                        structure.ProcessInstructions(instruction, value);
                }

            }
            return structure;
        }

        private static void ProcessInstructions(this IFiniteAutomataStructure structure,string instruction,string value)
        {
            switch (instruction)
            {
                case Instruction.ALPHABET:
                    structure.StructureAlphabet = ParseAlphabetFromInstructionsInput(value);
                    break;
                case Instruction.STATES:
                    structure.States.PopulateStatesFromInstructionsInput(value);
                    break;
                case Instruction.REGEX:
                    structure.BuildStructureFromRegex(value);
                    break;
                case Instruction.FINAL_STATES:
                    structure.States.MarkFinalStatesFromInstructionsInput(value);
                    break;
                case Instruction.TRANSITIONS:
                    structure.PopulateTransitionsFromInstructionsInput(value);
                    break;
                case Instruction.END:
                    break;
                default:
                    break;
            }
        }

        private static void BuildStructureFromRegex(this IFiniteAutomataStructure structure,string regex)
        {
            Stack<TompsonInitialFinalStatesHelperPair> processedValues = new Stack<TompsonInitialFinalStatesHelperPair>();
            
            var preparedRegex = regex.Trim().Replace("(", "").Replace(")", "").Replace(",", "");

            int statesIdCounter = 0;
            for (int i = preparedRegex.Length - 1; i >= 0; i--)
            {
                if (preparedRegex[i].IsTompsonRule())
                {
                    processedValues.Push(TompsonProcessor.ProcessRule(preparedRegex[i], processedValues, structure,ref statesIdCounter));
                }
                else {
                    var newLetter = new Letter(preparedRegex[i]);
                    structure.StructureAlphabet.Letters.Add(newLetter);

                    var newInitialState = new State(statesIdCounter++, "", true);
                    var newFinalState = new State(statesIdCounter++, "", false);
                    newFinalState.Final = true;
                    newInitialState.Directions.Add(newFinalState, new HashSet<ILetter>() { newLetter });

                    structure.States.Add(newInitialState);
                    structure.States.Add(newFinalState);

                    processedValues.Push(new TompsonInitialFinalStatesHelperPair()
                    {
                        CurrentFinal = newFinalState,
                        CurrentInitial = newInitialState
                    });
                }
            }
        }

        public static TestsEvaluationResult ProcessTestsEvaluation(this IFiniteAutomataStructure structure, TestsInput input)
        {
            var result = new TestsEvaluationResult();
            var testCase = input.words.Trim().Split("\n");
            bool wordsCasesStarted = false;
            foreach (var tc in testCase)
            {
                if (!String.IsNullOrWhiteSpace(tc))
                {
                    string caseToCheck,testGuess = "";

                    if (wordsCasesStarted)
                    {
                        if (tc.Equals(TestCase.END))
                        {
                            wordsCasesStarted = false;
                            continue;
                        }

                        var parsed = tc.Split(TestCase.WORDS_DELIMETER);

                        caseToCheck = parsed[0].Trim();
                        testGuess = parsed.Length > 1 ? parsed[1].Trim() : null;
                    }
                    else
                    {
                        var parsed = tc.Split(TestCase.CASE_DELIMETER);
                        caseToCheck = parsed[0].Trim();
                        testGuess = parsed.Length > 1 ? parsed[1].Trim() : null;

                        if (caseToCheck.Equals(TestCase.WORDS))
                        {
                            wordsCasesStarted = true;
                            continue;
                        }
                    }

                    result.ProcessTestCase(
                        structure,
                        wordsCasesStarted ? TestCase.WORDS : caseToCheck,
                        testGuess.Equals(TestCase.TEST_GUESS_TRUE) ? true : false,
                        caseToCheck);
                }
            }
            return result;
        }
        private static void ProcessTestCase(this TestsEvaluationResult result, IFiniteAutomataStructure structure, string testCase,bool testGuess,string word = "")
        {
            switch (testCase)
            {
                case Instruction.WORDS:
                    result.WordCheckerResults.Add(new WordCheckerResult(structure.WordExists(word),testGuess, word));
                    break;
                case Instruction.END:
                    break;
                default:
                    break;
            }
        }


    }
}
