using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using logic.FiniteAutomataService.Models;

namespace logic.FiniteAutomataService
{
    public class FiniteAutomataService : IFiniteAutomataService
    {
        public IFiniteAutomataStructure structure { get; set; }

        public FiniteAutomataStructureDto EvaluateFromInstructions(InstructionsInput input)
        {
            this.structure = input.BuildStructure();
            return new FiniteAutomataStructureDto(this.structure);
        }
        
        public TestsEvaluationResultDTO EvaluateTestCases(TestsInput input)
        {
            return new TestsEvaluationResultDTO(this.structure.ProcessTestsEvaluation(input),structure.IsDFA,true);
        }
    }
}
