using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using logic.FiniteAutomataService.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace logic.FiniteAutomataService
{
    public class AutomataService : IAutomataService
    {
        public IAutomataStructure structure { get; set; }

        public AutomataEvaluationDTO EvaluateFromInstructions(IConfiguration configuration,InstructionsInput input)
        {
            this.structure = new AutomataStructure(input);
            this.structure.GenerateOriginalInstructions(configuration);
            if (!structure.IsDFA)
            {
                structure.BuildDFA();
                structure.GenerateDFAInstructions(configuration);
            }
            return new AutomataEvaluationDTO(this.structure);
        }
        
        public TestsEvaluationResult EvaluateTestCases(TestsInput input)
        {
            return this.structure.EvaluateTests(input);
        }

        public bool CheckWord(string word)
        {
            return this.structure.WordExists(String.IsNullOrWhiteSpace(word) ? "" : word);
        }
    }
}
