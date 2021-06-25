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
    public static class AutomataStatesExtensions
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
        public static HashSet<IState> PopulateTransitions(
            this HashSet<IState> states,
            IAlphabet alphabet, 
            HashSet<ILetter> stack,
            InstructionsInput input)
        {
            var transitions = RegexHelper.Match(input.instructions, Instruction.TRANSITIONS).Split("\n");
            foreach (var transition in transitions)
            {
                if (String.IsNullOrWhiteSpace(transition)) continue;

                var transitionGroup = transition.Trim().Split(InstructionDelimeter.TRANSITION_FROM_TO);
                var transitionFromValues = transitionGroup[0].Trim().Split(InstructionDelimeter.TRANSITION_FROM_VALUES,2);
                var toStateValue = transitionGroup[1].Trim();

                var fromStateValue = transitionFromValues[0].Trim();
                var transitionLetter = new Letter(transitionFromValues[1].Trim().ToCharArray()[0]);

                var transitionValue = new DirectionValue
                { 
                     Letter = transitionLetter
                };


                var pushDownGroup = RegexHelper.Match(transitionFromValues[1], "(?<=\\[)([\\S\\s].*?)*(?=])").Trim();
                if (!String.IsNullOrWhiteSpace(pushDownGroup))
                {
                    var pushDownValues = pushDownGroup.Split(",");

                    var letterToPop = new Letter(pushDownValues[0].Trim().ToCharArray()[0]);
                    var letterToPush = new Letter(pushDownValues[1].Trim().ToCharArray()[0]);
                    if( (!letterToPop.IsEpsilon  && !stack.Contains(letterToPop)) ||
                        (!letterToPush.IsEpsilon && !stack.Contains(letterToPush))
                       )
                    {
                        throw new Exception("Invalid push down values!");
                    }
                    transitionValue.LetterToPop = letterToPop;
                    transitionValue.LetterToPush = letterToPush;
                }

                if (transitionLetter.IsEpsilon && !alphabet.Letters.Contains(transitionLetter))
                {
                    alphabet.Letters.Add(transitionLetter);
                }

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
                                        fromState.Directions[toState].Add(transitionValue);
                                    }
                                    else
                                    {
                                        fromState.Directions.Add(toState, new HashSet<DirectionValue>() { transitionValue });
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
                    if (direction.Value.Where(d => !d.Letter.IsEpsilon).Count() > 0)
                    {
                        statesInALoop.Add(direction.Key);
                        break;
                    }

                    foreach (var transition in transitionsTracker.Reverse())
                    {
                        if (transition.From.Equals(direction.Key))
                        {
                            if (!transition.Value.Letter.IsEpsilon &&
                                transition.To.CanReachStates(new HashSet<IState>() { currentState }))
                            {
                                statesInALoop.Add(direction.Key);
                            }
                            break;
                        }
                        if (!transition.Value.Letter.IsEpsilon &&
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
                    foreach (var value in direction.Value)
                    {
                        transitionsTracker.Add(new Transition(1, currentState, direction.Key, value));
                    }
                    PopulateStatesPartOfALoop(statesTracker, direction.Key, transitionsTracker, statesInALoop);
                }
            }
            statesTracker.Remove(currentState);
            return statesInALoop;
        }
        
        public static HashSet<string> GatherAllPossibleWords(
            this HashSet<IState> traversed,
            HashSet<string> words,
            string currentWord,
            IState state)
        {
            foreach (var direction in state.Directions)
            {
                if (!traversed.Contains(direction.Key))
                {
                    traversed.Add(direction.Key);
                    foreach (var value in direction.Value)
                    {
                        if (!value.Letter.IsEpsilon)
                        {
                            currentWord += value.Letter.Value;
                        }
                        GatherAllPossibleWords(traversed, words, currentWord, direction.Key);
                        currentWord = currentWord.Length > 0 ? currentWord[0..^1] : currentWord;
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
