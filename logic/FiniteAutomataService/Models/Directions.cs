using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class Directions : SortedDictionary<IState, HashSet<DirectionValue>>
    {
        public Directions() : base() { }
    }
}
