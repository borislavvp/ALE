using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using logic.FiniteAutomataService.Constants;
using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Extensions;
using logic.FiniteAutomataService.Helpers;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models.TompsonConstruction;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic.FiniteAutomataService.Models
{
    public class FiniteAutomataStructure : IFiniteAutomataStructure
    {
        public HashSet<IState> DFA { get; set; }
        public string DFAInstructions { get; set; }
        public HashSet<IState> States { get; set; }
        public IAlphabet StructureAlphabet { get; set; }
        public bool IsDFA { get => this.CheckDFA();}
        public bool IsFinite { get => this.CheckFinite(); }
        public FiniteAutomataStructure(InstructionsInput input)
        {
            if (input.instructions.Contains("regex")) {
                this.StructureAlphabet = new Alphabet();
                this.States = new HashSet<IState>();
                this.BuildStructureFromRegex(input.GetRegex());
            }
            else
            {
                this.StructureAlphabet = input.PopulateAlphabet();
                this.States = input.PopulateStates()
                                   .MarkFinalStates(input)
                                   .PopulateTransitions(this.StructureAlphabet,input);
            }
        }

        public IState GetInitialState()
        {
            return this.States.Where(s => s.Initial).FirstOrDefault();
        } 
        public HashSet<IState> GetFinalStates()
        {
            return this.States.Where(s => s.Final).ToHashSet();
        } 

        private bool CheckDFA()
        {
            if (this.StructureAlphabet.Letters.Contains(Alphabet.EPSILON_LETTER))
            {
                return false;
            }
            foreach (var state in this.States)
            {
                var temp = new HashSet<ILetter>();
                foreach (var values in state.Directions.Values)
                {
                    foreach (var letter in values)
                    {
                        temp.Add(letter);
                    }
                }
                if (!temp.SetEquals(this.StructureAlphabet.Letters)){
                    return false;
                }
            }
            return true;
        }

        private bool CheckFinite()
        {
            var selfReferencedStates = this.GetInitialState().GetAllSelfReferencedStates();
            foreach (var state in selfReferencedStates)
            {
                if (state.CanReachStates(GetFinalStates()))
                    return false;
            }

            var statesInALoop = new HashSet<IState>().PopulateStatesPartOfALoop(GetInitialState(),new HashSet<Transition>(),new HashSet<IState>());
            
            foreach (var state in statesInALoop)
            {
                if (state.CanReachStates(GetFinalStates()))
                    return false;
            }

            return true;
        }

        public void GenerateDFAInstructions(IConfiguration configuration)
        {
            try
            {
                //string directoryPath = "./instructions/";
                //string instructionsID = Guid.NewGuid().ToString()+ ".txt";
                //string localFilePath = Path.Combine(directoryPath, instructionsID);

                string alphabet = "alphabet: ";
                foreach (var letter in this.StructureAlphabet.Letters)
                {
                    if (!letter.IsEpsilon)
                    {
                        alphabet += $@"{letter.Value}";
                    }
                }

                string states = "states: ";
                string finalStates = "final: ";
                foreach (var state in this.DFA)
                {
                    states += state.Value + ',';
                    if (state.Final)
                    {
                        finalStates += state.Value + ',';
                    }
                }
                states = states.Trim(',');
                finalStates = finalStates.Trim(',');

                string transitions = "transitions: ";
                transitions += Environment.NewLine;
                foreach (var state in this.DFA)
                {
                    foreach (var direction in state.Directions)
                    {
                        foreach (var letter in direction.Value)
                        {
                            transitions += @$"{state.Value},{letter.Value} --> {direction.Key.Value} ";
                            transitions += Environment.NewLine;
                        }
                    }
                }
                transitions = transitions += "end.";

                string instructions = alphabet
                    + Environment.NewLine
                    + states
                    + Environment.NewLine
                    + finalStates
                    + Environment.NewLine
                    + transitions
                    + Environment.NewLine;

                instructions += "---";
                instructions += Environment.NewLine;
                instructions += "dfa:y";
                instructions += Environment.NewLine;
                instructions += "finite:" + (this.IsFinite ? 'y' : 'n');

                //FileHelper.CreateFile(directoryPath, localFilePath, instructions);
                //await BlobHelper.PublishFile(configuration, instructionsID, localFilePath);

                //FileHelper.DeleteFiles(directoryPath);
                this.DFAInstructions = instructions;
            }

            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        public TestsEvaluationResult EvaluateTests(TestsInput input)
        {
            TestsEvaluationResult result = new TestsEvaluationResult();
            result.IsDFA = new TestInputAnswer(input.GetDFATestCase(), this.IsDFA);
            result.IsFinite = new TestInputAnswer(input.GetFiniteTestCase(), this.IsFinite);
            if (result.IsFinite.Answer)
            {
                result.AllPossibleWords = this.GetInitialState().GetAllPossibleWords();
            }
            var words = input.GetWordsTestCase();
            foreach (var wordTestCase in words)
            {
                if (String.IsNullOrWhiteSpace(wordTestCase)) continue;
                var wordWithGuess = wordTestCase.Split(TestCase.WORDS_DELIMETER);
                var word = wordWithGuess[0].Trim();
                var testGuess = wordWithGuess[1].Trim().Equals(TestCase.TEST_GUESS_TRUE);

                result.WordCheckerResults.Add(
                    new WordCheckerResult(
                        new TestInputAnswer(
                            testGuess,
                            this.WordExists(word)), word));
            }
            return result;
        }

        public bool WordExists(string word)
        {
            return this.GetInitialState().CheckWord(word);
        }

        public void BuildStructureFromRegex(string regex)
        {
            Stack<TompsonInitialFinalStatesHelperPair> processedValues = new Stack<TompsonInitialFinalStatesHelperPair>();

            var preparedRegex = regex.Trim().Replace("(", "").Replace(")", "").Replace(",", "");

            int statesIdCounter = 0;
            for (int i = preparedRegex.Length - 1; i >= 0; i--)
            {
                if (preparedRegex[i].IsTompsonRule())
                {
                    processedValues.Push(TompsonProcessor.ProcessRule(preparedRegex[i], processedValues, this, ref statesIdCounter));
                }
                else
                {
                    var newLetter = new Letter(preparedRegex[i]);
                    this.StructureAlphabet.Letters.Add(newLetter);

                    var newInitialState = new State(++statesIdCounter, "", true);
                    var newFinalState = new State(++statesIdCounter, "", false, true);

                    newInitialState.Directions.Add(newFinalState, new HashSet<ILetter>() { newLetter });

                    this.States.Add(newInitialState);
                    this.States.Add(newFinalState);

                    processedValues.Push(new TompsonInitialFinalStatesHelperPair()
                    {
                        CurrentFinal = newFinalState,
                        CurrentInitial = newInitialState
                    });
                }
            }
        }

        public void BuildDFA()
        {
            var newBuildedStates = new Dictionary<IState, HashSet<IState>>();

            var queue = new Queue<IState>();

            var sink = new State(-1, "Sink");

            bool isSinkNeeded = false;

            var initialClosures = this.GetInitialState().FindEpsilonClosures();

            var initialState = initialClosures.BuildNewState();
            initialState.Initial = true;

            newBuildedStates.Add(initialState, initialClosures);

            queue.Enqueue(initialState);

            while (queue.Count > 0)
            {
                var currentState = queue.Dequeue();
                var statesBuilder = new Dictionary<ILetter, HashSet<IState>>();

                foreach (var state in newBuildedStates[currentState])
                {
                    foreach (var direction in state.Directions)
                    {
                        var currentClosures = direction.Key.FindEpsilonClosures();
                        foreach (var letter in direction.Value)
                        {
                            if (letter.IsEpsilon)
                            {
                                continue;
                            }
                            if (statesBuilder.ContainsKey(letter))
                            {
                                foreach (var s in currentClosures)
                                {
                                    statesBuilder[letter].Add(s);
                                }
                            }
                            else
                            {
                                statesBuilder.Add(letter, new HashSet<IState>(currentClosures));
                            }
                        }
                    }
                }

                foreach (var letter in statesBuilder.Keys)
                {
                    var state = statesBuilder[letter].BuildNewState();
                    if (!newBuildedStates.ContainsKey(state))
                    {
                        queue.Enqueue(state);
                        newBuildedStates.Add(state, statesBuilder[letter]);
                    }
                    if (currentState.Directions.ContainsKey(state))
                    {
                        currentState.Directions[state].Add(letter);
                    }
                    else
                    {
                        currentState.Directions.Add(state, new HashSet<ILetter>() { letter });
                    }
                }
                foreach (var letter in this.StructureAlphabet.Letters)
                {
                    if (letter.IsEpsilon)
                    {
                        continue;
                    }
                    if (!statesBuilder.ContainsKey(letter))
                    {
                        if (currentState.Directions.ContainsKey(sink))
                        {
                            currentState.Directions[sink].Add(letter);
                        }
                        else
                        {
                            isSinkNeeded = true;
                            currentState.Directions.Add(sink, new HashSet<ILetter>() { letter });
                        }
                    }
                }
            }
            this.DFA = newBuildedStates.Keys.ToHashSet();
            if (isSinkNeeded)
            {
                this.DFA.Add(sink);
                foreach (var letter in this.StructureAlphabet.Letters)
                {
                    if (letter.IsEpsilon)
                    {
                        continue;
                    }
                    if (sink.Directions.ContainsKey(sink))
                    {
                        sink.Directions[sink].Add(letter);
                    }
                    else
                    {
                        sink.Directions.Add(sink, new HashSet<ILetter>() { letter });
                    }
                }
            }
        }

    }
}
