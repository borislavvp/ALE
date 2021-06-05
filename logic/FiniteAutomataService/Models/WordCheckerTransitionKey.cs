using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class WordCheckerTransitionKey
    {
        public IState From { get; set; }
        public int letterChecked { get; set; }

        public WordCheckerTransitionKey(IState From, int letterChecked)
        {
            this.From = From;
            this.letterChecked = letterChecked;
        }

        public override bool Equals(object obj)
        {
            return obj is WordCheckerTransitionKey other && this.From.Equals(other.From) && this.letterChecked.Equals(other.letterChecked);
        }

        public override int GetHashCode()
        {
            int hashCode = -157375006;
            hashCode = hashCode * -1521134295 + this.letterChecked + From.Id;
            hashCode = hashCode * -1521134295 + this.letterChecked + From.Id;
            return hashCode;
        }
    }
}
