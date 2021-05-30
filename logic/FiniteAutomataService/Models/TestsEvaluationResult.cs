using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class TestsEvaluationResult
    {
        public List<WordCheckerResult> WordCheckerResults { get; set; }
        public TestsEvaluationResult()
        {
            this.WordCheckerResults = new List<WordCheckerResult>();
        }
    }
}
