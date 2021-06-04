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
        public override bool Equals(object obj)
        {
            return obj is Transition transition && this.From.Equals(transition.From) && this.To.Equals(transition.To);
        }

        public override int GetHashCode()
        {
            int hashCode = -157375006;
            hashCode = hashCode * -1521134295 + this.From.Id + this.To.Id;
            hashCode = hashCode * -1521134295 + this.From.Id + this.To.Id;
            return hashCode;
        }
    }
}
