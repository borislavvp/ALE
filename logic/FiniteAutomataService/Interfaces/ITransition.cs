using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface ITransition
    {
        IState From { get; set; }
        IState To { get; set; }
        ILetter Value { get; set; }
    }
}
