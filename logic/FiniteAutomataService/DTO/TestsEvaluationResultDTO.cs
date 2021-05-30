using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class TestsEvaluationResultDTO
    {
        public bool IsDFA { get; set; }
        public bool IsFinite { get; set; }
        public List<WordCheckerResult> WordCheckerResults {get;set;}
        public TestsEvaluationResultDTO(TestsEvaluationResult Result,bool IsDFA, bool IsFinite)
        {
            this.WordCheckerResults = Result.WordCheckerResults;
            this.IsDFA = IsDFA;
            this.IsFinite = IsFinite;
        }
    }
}
