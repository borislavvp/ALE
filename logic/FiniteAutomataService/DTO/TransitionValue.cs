using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class TransitionValue
    {
        public ILetter Letter { get; set; }
        public ILetter LetterToPush { get; set; }
        public ILetter LetterToPop { get; set; }
    }
}
