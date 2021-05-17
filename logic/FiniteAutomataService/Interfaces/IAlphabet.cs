using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IAlphabet
    {
        IList<ILetter> Letters { get; set; }
    }
}
