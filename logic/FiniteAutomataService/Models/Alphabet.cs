using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class Alphabet : IAlphabet
    {
        public HashSet<ILetter> Letters { get ; set ; }

        public static readonly ILetter EPSILON_LETTER = new Letter('_');

        public Alphabet()
        {
            this.Letters = new HashSet<ILetter>();
        }
    }
}
