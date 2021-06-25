using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IAutomataStructure
    {
        HashSet<IState> DFA { get; set; }
        string DFAInstructions { get; set; }
        string OriginalInstructions { get; set; }
        bool IsDFA { get; }
        bool IsFinite { get; }
        IAlphabet StructureAlphabet { get; set; }
        HashSet<IState> States { get; set; }
        HashSet<ILetter> Stack { get; set; }
        IState GetInitialState();
        HashSet<IState> GetFinalStates();
        TestsEvaluationResult EvaluateTests(TestsInput input);
        bool CheckDFA(HashSet<IState> states);
        bool WordExists(string word);
        void BuildDFA();
        void GenerateDFAInstructions(IConfiguration configuration);
        void GenerateOriginalInstructions(IConfiguration configuration);
    }
}
