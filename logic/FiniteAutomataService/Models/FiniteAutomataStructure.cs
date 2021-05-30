using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class FiniteAutomataStructure : IFiniteAutomataStructure
    {
        public IAlphabet StructureAlphabet { get; set; }
        public SortedSet<IState> States { get; set; }
        public bool IsDFA { get => this.CheckDFA(); }

        public FiniteAutomataStructure()
        {
            this.StructureAlphabet = new Alphabet();
            this.States = new SortedSet<IState>();
        }

        public IState GetInitialState()
        {
            return this.States.Where(s => s.Initial).FirstOrDefault();
        } 

        private bool CheckDFA()
        {
            if (this.StructureAlphabet.Letters.Contains(Alphabet.EPSILON_LETTER))
            {
                return false;
            }
            foreach (var state in this.States)
            {
                foreach (var values in state.Directions.Values)
                {
                    if (!values.SetEquals(this.StructureAlphabet.Letters)){
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
