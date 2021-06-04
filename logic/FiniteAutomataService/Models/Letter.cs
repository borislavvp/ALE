using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class Letter: ILetter
    {
        public char Value { get; set; }
        public bool IsEpsilon => this.Value.Equals(Alphabet.EPSILON_LETTER.Value);
        public Letter(char Value)
        {
            this.Value = Value;
        }

        public override bool Equals(object obj)
        {
            return obj is ILetter letter && this.Value.Equals(letter.Value);
        }

        public override int GetHashCode()
        {
            int hashCode = -157375006;
            hashCode = hashCode * -1521134295 + this.Value;
            hashCode = hashCode * -1521134295 + this.Value;
            return hashCode;
        }

    }
}
