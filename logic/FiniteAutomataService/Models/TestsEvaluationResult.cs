using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class TestsEvaluationResult
    {
        public TestInputAnswer IsDFA { get; set; }
        public HashSet<string> AllPossibleWords { get; set; }
        public TestInputAnswer IsFinite { get; set; }
        public List<WordCheckerResult> WordCheckerResults { get; set; }
        public TestsEvaluationResult()
        {
            this.WordCheckerResults = new List<WordCheckerResult>();
        }
    }
}
