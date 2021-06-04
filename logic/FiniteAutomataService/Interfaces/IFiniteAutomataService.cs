using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IFiniteAutomataService
    {
        IFiniteAutomataStructure structure { get; set; }
        Task<FiniteAutomataEvaluationDTO> EvaluateFromInstructions(IConfiguration configuration,InstructionsInput input);
        TestsEvaluationResult EvaluateTestCases(TestsInput input);
        bool CheckWord(string word);
    }
}
