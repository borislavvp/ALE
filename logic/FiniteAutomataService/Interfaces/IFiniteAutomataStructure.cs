using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IFiniteAutomataStructure
    {
        HashSet<IState> DFA { get; set; }
        string DFAInstructions { get; set; }
        string OriginalInstructions { get; set; }
        bool IsDFA { get; }
        bool IsFinite { get; }
        IAlphabet StructureAlphabet { get; set; }
        HashSet<IState> States { get; set; }
        IState GetInitialState();
        TestsEvaluationResult EvaluateTests(TestsInput input);
        bool WordExists(string word);
        void BuildStructureFromRegex(string regex);
        void BuildDFA();
        void GenerateDFAInstructions(IConfiguration configuration);
        void GenerateOriginalInstructions(IConfiguration configuration);
    }
}
