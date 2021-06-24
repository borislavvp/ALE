using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface ILetter
    {
        char Value { get; set; }
        bool IsEpsilon{ get; }
    }
}
