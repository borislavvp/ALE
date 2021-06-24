using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
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
        bool CheckWord(string word);
        bool CanReachStates(HashSet<IState> stateToReach);
        HashSet<IState> FindEpsilonClosures();
        HashSet<string> GetAllPossibleWords();
        HashSet<IState> GetAllSelfReferencedStates();
    }
}
