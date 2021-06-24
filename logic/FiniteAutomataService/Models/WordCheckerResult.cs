using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class WordCheckerResult
    {
        public TestInputAnswer Test { get; set; }
        public string Word { get; set; }

        public WordCheckerResult(TestInputAnswer Test, string Word)
        {
            this.Test = Test;
            this.Word = Word;
        }
    }
}
