using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class WordCheckerResult
    {
        public bool Answer { get; set; }
        public bool TestGuess { get; set; }
        public string Word { get; set; }

        public WordCheckerResult(bool Answer, bool TestGuess, string Word)
        {
            this.Answer = Answer;
            this.TestGuess = TestGuess;
            this.Word = Word;
        }
    }
}
