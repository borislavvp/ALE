using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IAlphabet
    {
        HashSet<ILetter> Letters { get; set; }
    }
}
