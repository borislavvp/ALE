using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class TransitionDTO : IComparable<TransitionDTO>
    {
        public int Id { get; set; }
        public StateDTO From { get; set; }
        public StateDTO To { get; set; }
        public TransitionValue Value { get; set; }

        public TransitionDTO(int Id,StateDTO From, StateDTO To, DirectionValue Value)
        {
            this.Id = Id;
            this.From = From;
            this.To = To;
            this.Value = new TransitionValue
            {
                Letter = Value.Letter.IsEpsilon ? new Letter('ε') : Value.Letter,
                LetterToPush = Value.LetterToPush != null 
                                ? Value.LetterToPush.IsEpsilon 
                                    ? new Letter('ε') 
                                    : Value.LetterToPush
                                : null,
                LetterToPop = Value.LetterToPop != null 
                                ? Value.LetterToPop.IsEpsilon 
                                    ? new Letter('ε') 
                                    : Value.LetterToPop
                                : null,
            };
        }
        public override bool Equals(object obj)
        {
            var current = @$"{this.From.Id}{this.To.Id}{this.From.Value}{this.To.Value}";
            return obj is TransitionDTO transition && current.Equals(@$"{transition.From.Id}{transition.To.Id}{transition.From.Value}{transition.To.Value}");
        }

        public override int GetHashCode()
        {
            int hashCode = -157375006;
            hashCode = hashCode * -1521134295 + this.Id;
            hashCode = hashCode * -1521134295 + this.Id;
            return hashCode;
        }
        public int CompareTo([DisallowNull] TransitionDTO other)
        {
            var current = @$"{this.From.Id}{this.To.Id}{this.From.Value}{this.To.Value}";
            var toCompareTransition = @$"{other.From.Id}{other.To.Id}{other.From.Value}{other.To.Value}";
            return current.CompareTo(toCompareTransition);
        }
    }
}
