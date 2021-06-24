using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models.TompsonConstruction
{
    public class TompsonInitialFinalStatesHelperPair
    {
        public IState CurrentInitial { get; set; }
        public IState CurrentFinal { get; set; }
        public IState CurrentExtendedConnection { get; set; }
    }
}
