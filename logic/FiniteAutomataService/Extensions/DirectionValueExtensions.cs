using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Extensions
{
    public static class DirectionValueExtensions
    {
        public static DirectionValue GetDirectionValueByLetter(this HashSet<DirectionValue> directionValues, ILetter letter)
        {
            foreach (var value in directionValues)
            {
                if (value.Letter.Equals(letter))
                {
                    return value;
                }
            }
            return null;
        }
    }
}
