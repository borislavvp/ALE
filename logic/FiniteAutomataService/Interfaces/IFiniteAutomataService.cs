using logic.FiniteAutomataService.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IFiniteAutomataService
    {
        IFiniteAutomataStructure structure { get; set; }
        FiniteAutomataStructureDto EvaluateFromInstructions(InstructionsInput input);
        TestsEvaluationResultDTO EvaluateTestCases(TestsInput input);
    }
}
