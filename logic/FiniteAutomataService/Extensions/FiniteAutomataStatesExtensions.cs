using logic.FiniteAutomataService.Constants;
using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Helpers;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace logic.FiniteAutomataService.Extensions
{
    public static class FiniteAutomataStatesExtensions
    {
        public static HashSet<IState> MarkFinalStates(this HashSet<IState> states, InstructionsInput input)
        {
            var result = RegexHelper.Match(input.instructions, Instruction.FINAL_STATES);
            var finalStates = result.Trim().Split(InstructionDelimeter.FINAL_STATES_VALUES);
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
            return states;
        }
        public static HashSet<IState> PopulateTransitions(this HashSet<IState> states,IAlphabet alphabet, InstructionsInput input)
        {
            var transitions = RegexHelper.Match(input.instructions, Instruction.TRANSITIONS).Split("\n");
            foreach (var transition in transitions)
            {
                if (String.IsNullOrWhiteSpace(transition)) continue;

                var transitionGroup = transition.Trim().Split(InstructionDelimeter.TRANSITION_FROM_TO);
                var transitionFromValues = transitionGroup[0].Trim().Split(InstructionDelimeter.TRANSITION_FROM_VALUES);
                var toStateValue = transitionGroup[1].Trim();

                var fromStateValue = transitionFromValues[0].Trim();
                var transitionLetter = new Letter(transitionFromValues[1].Trim().ToCharArray()[0]);

                if (transitionLetter.IsEpsilon && !alphabet.Letters.Contains(transitionLetter))
                    alphabet.Letters.Add(transitionLetter);

                if (alphabet.Letters.Contains(transitionLetter))
                {
                    foreach (var fromState in states)
                    {
                        if (fromState.Value.Equals(fromStateValue))
                        {
                            foreach (var toState in states)
                            {
                                if (toState.Value.Equals(toStateValue))
                                {
                                    if (fromState.Directions.ContainsKey(toState))
                                    {
                                        fromState.Directions[toState].Add(transitionLetter);
                                    }
                                    else
                                    {
                                        fromState.Directions.Add(toState, new HashSet<ILetter>() { transitionLetter });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return states;
        }
        public static HashSet<IState> PopulateStatesPartOfALoop(this HashSet<IState> statesTracker,
                                                                IState currentState,
                                                                HashSet<Transition> transitionsTracker,
                                                                HashSet<IState> statesInALoop)
        {
            statesTracker.Add(currentState);
            foreach (var direction in currentState.Directions)
            {
                if (direction.Key.Equals(currentState))
                {
                    continue;
                }
                if (statesTracker.Contains(direction.Key))
                {
                    if (!direction.Value.Contains(Alphabet.EPSILON_LETTER))
                    {
                        statesInALoop.Add(direction.Key);
                        break;
                    }

                    foreach (var transition in transitionsTracker.Reverse())
                    {
                        if (transition.From.Equals(direction.Key))
                        {
                            break;
                        }
                        if (!transition.Value.Equals(Alphabet.EPSILON_LETTER) &&
                            !transition.From.Equals(currentState) &&
                            transition.From.CanReachStates(new HashSet<IState>() { direction.Key }))
                        {

                            statesInALoop.Add(direction.Key);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var letter in direction.Value)
                    {
                        transitionsTracker.Add(new Transition(1, currentState, direction.Key, letter));
                    }
                    PopulateStatesPartOfALoop(statesTracker, direction.Key, transitionsTracker, statesInALoop);
                }
            }
            return statesInALoop;
        }
        
        public static HashSet<string> GatherAllPossibleWords(this HashSet<IState> traversed,
            HashSet<string> words, string currentWord, IState state)
        {
            foreach (var direction in state.Directions)
            {
                if (!traversed.Contains(direction.Key))
                {
                    traversed.Add(direction.Key);
                    foreach (var letter in direction.Value)
                    {
                        if (!letter.IsEpsilon)
                        {
                            currentWord += letter.Value;
                        }
                        GatherAllPossibleWords(traversed, words, currentWord, direction.Key);
                    }
                }
            }
            if (state.Final)
            {
                words.Add(currentWord);
            }
            traversed.Remove(state);
            return words;
        }
        public static IState BuildNewState(this HashSet<IState> states)
        {
            int newId = 0;
            string newValue = "(";
            foreach (var state in states)
            {
                newId += state.Id;
                newValue += state.Value + "-";
            }

            return new State(newId, newValue.Trim('-')+")", false, states.Where(s => s.Final).Count() > 0);
        }
    }
}
