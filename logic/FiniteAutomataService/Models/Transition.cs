using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class Transition : ITransition
    {
        public IState From { get; set; }
        public IState To { get; set; }
        public ILetter Value { get; set; }
        public int Id { get; set; }

        public Transition(int Id, IState From, IState To, ILetter Value)
        {
            this.Id = Id;
            this.From = From;
            this.To = To;
            this.Value = Value;
        }
    }
}
