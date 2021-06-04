using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class TestInputAnswer
    {
        public bool TestGuess { get; set; }
        public bool Answer { get; set; }

        public TestInputAnswer(bool Input, bool Answer)
        {
            this.TestGuess = Input;
            this.Answer = Answer;
        }
    }
}
