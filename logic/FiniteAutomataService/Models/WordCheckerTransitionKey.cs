using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class WordCheckerTransitionKey
    {
        public IState From { get; set; }
        public int WithLetterIndex { get; set; }

        public WordCheckerTransitionKey(IState From, int WithLetterIndex)
        {
            this.From = From;
            this.WithLetterIndex = WithLetterIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is WordCheckerTransitionKey other && this.From.Equals(other.From) && this.WithLetterIndex.Equals(other.WithLetterIndex);
        }

        public override int GetHashCode()
        {
            int hashCode = -157375006;
            hashCode = hashCode * -1521134295 + this.WithLetterIndex + From.Id;
            hashCode = hashCode * -1521134295 + this.WithLetterIndex + From.Id;
            return hashCode;
        }
    }
}
