using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IFiniteAutomataStructure
    {
        IAlphabet Alphabet { get; set; }
        HashSet<IState> States { get; set; }
    }
}
