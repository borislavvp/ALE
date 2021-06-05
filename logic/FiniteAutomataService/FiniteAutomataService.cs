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
    public class FiniteAutomataService : IFiniteAutomataService
    {
        public IFiniteAutomataStructure structure { get; set; }

        public FiniteAutomataEvaluationDTO EvaluateFromInstructions(IConfiguration configuration,InstructionsInput input)
        {
            this.structure = new FiniteAutomataStructure(input);
            if (!structure.IsDFA)
            {
                structure.BuildDFA();
                structure.GenerateDFAInstructions(configuration);
            }
            return new FiniteAutomataEvaluationDTO(this.structure);
        }
        
        public TestsEvaluationResult EvaluateTestCases(TestsInput input)
        {
            return this.structure.EvaluateTests(input);
        }

        public bool CheckWord(string word)
        {
            return this.structure.WordExists(word);
        }
    }
}
