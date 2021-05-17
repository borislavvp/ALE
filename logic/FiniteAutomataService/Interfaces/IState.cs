using System;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IState
    {
        bool Initial { get; set; }
        bool Final { get; set; }
        string Value { get; set; }
        IDerections Directions { get; set; }
    }
}
