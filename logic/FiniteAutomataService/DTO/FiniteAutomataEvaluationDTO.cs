using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class FiniteAutomataEvaluationDTO
    {
        public string DFAInstructionsID { get; set; }
        public FiniteAutomataStructureDto DFA { get; set; }
        public FiniteAutomataStructureDto Original { get; set; }

        public FiniteAutomataEvaluationDTO(IFiniteAutomataStructure structure)
        {
            this.DFA = structure.IsDFA ? null : new FiniteAutomataStructureDto(structure.DFA);
            this.Original = new FiniteAutomataStructureDto(structure.States);
            this.DFAInstructionsID = structure.DFAInstructionsID;
        }
    }
}
