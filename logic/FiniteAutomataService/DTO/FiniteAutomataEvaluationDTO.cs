using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class FiniteAutomataEvaluationDTO
    {
        public string DFAInstructions { get; set; }
        public string OriginalInstructions { get; set; }
        public FiniteAutomataStructureDto DFA { get; set; }
        public FiniteAutomataStructureDto Original { get; set; }

        public FiniteAutomataEvaluationDTO(IFiniteAutomataStructure structure)
        {
            this.DFA = structure.IsDFA ? null : new FiniteAutomataStructureDto(structure.DFA);
            this.Original = new FiniteAutomataStructureDto(structure.States);
            this.DFAInstructions = structure.DFAInstructions;
            this.OriginalInstructions = structure.OriginalInstructions;
        }
    }
}
