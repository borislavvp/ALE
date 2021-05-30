using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class State : IState
    {
        public int Id { get; set; }
        public bool Initial { get; set; }
        public bool Final { get; set; }
        public string Value { get; }
        public Directions Directions { get; set; }

        public State(int Id, string Value, bool Initial)
        {
            this.Id = Id;
            this.Value = Value;
            this.Initial = Initial;
            this.Final = false;
            this.Directions = new Directions();
        } 
        
        public State(int Id, string Value, bool Initial,bool Final)
        {
            this.Id = Id;
            this.Value = Value;
            this.Initial = Initial;
            this.Final = Final;
            this.Directions = new Directions();
        }

        public int CompareTo([DisallowNull] IState other)
        {
            if (this.Id.Equals(other.Id))
            {
                return 0;
            }

            var currentContainsEpsilon = false;
            
            foreach (var item in this.Directions.Values)
            {
                if (item.Contains(Alphabet.EPSILON_LETTER))
                {
                    currentContainsEpsilon = true;
                }
            } 
            
            foreach (var otherItem in other.Directions.Values)
            {
                if (otherItem.Contains(Alphabet.EPSILON_LETTER))
                {
                    return currentContainsEpsilon 
                        ? this.Id.CompareTo(other.Id)
                        : 1;
                }
            }
            return this.Id.CompareTo(other.Id);
        }

    }
}
