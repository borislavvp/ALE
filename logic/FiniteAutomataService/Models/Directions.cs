using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class Directions : SortedDictionary<IState, HashSet<ILetter>>, ICloneable
    {
        public Directions() : base() { }

        public object Clone()
        {
            Directions clone = new Directions();
            foreach (KeyValuePair<IState, HashSet<ILetter>> kvp in this)
            {
                clone.Add(kvp.Key, kvp.Value.ToHashSet());
            }
            return clone;
        }
    }
}
