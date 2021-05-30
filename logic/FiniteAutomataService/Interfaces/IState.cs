using logic.FiniteAutomataService.Models;
using System;
using System.Text;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IState : IComparable<IState>
    {
        int Id { get; set; }
        bool Initial { get; set; }
        bool Final { get; set; }
        string Value { get; }
        Directions Directions { get; set; }
    }
}
