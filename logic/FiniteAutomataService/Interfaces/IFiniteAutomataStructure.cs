using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IFiniteAutomataStructure
    {
        bool IsDFA { get; }
        IAlphabet StructureAlphabet { get; set; }
        SortedSet<IState> States { get; set; }
        IState GetInitialState();
    }
}
