using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class AutomataEvaluationDTO
    {
        public string DFAInstructions { get; set; }
        public string OriginalInstructions { get; set; }
        public AutomataStructureDto DFA { get; set; }
        public AutomataStructureDto Original { get; set; }

        public AutomataEvaluationDTO(IAutomataStructure structure)
        {
            this.DFA = structure.IsDFA ? null : new AutomataStructureDto(structure.DFA);
            this.Original = new AutomataStructureDto(structure.States);
            this.DFAInstructions = structure.DFAInstructions;
            this.OriginalInstructions = structure.OriginalInstructions;
        }
    }
}
