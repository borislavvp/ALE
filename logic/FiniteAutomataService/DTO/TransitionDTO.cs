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
        public ILetter Value { get; set; }

        public TransitionDTO(int Id,StateDTO From, StateDTO To, ILetter Value)
        {
            this.Id = Id;
            this.From = From;
            this.To = To;
            this.Value = Value.IsEpsilon ? new Letter('ε'): Value;
        }
        public int CompareTo([DisallowNull] TransitionDTO other)
        {
            return (this.From.Id-this.To.Id).CompareTo(other.From.Id - other.To.Id) == 0 ?
                this.From.CompareTo(other.From) :
                (this.From.Id-this.To.Id).CompareTo(other.From.Id - other.To.Id) ;
        }
    }
}
