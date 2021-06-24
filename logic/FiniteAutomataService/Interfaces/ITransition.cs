using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface ITransition
    {
        int Id { get; set; }
        IState From { get; set; }
        IState To { get; set; }
        DirectionValue Value { get; set; }
    }
}
