using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class FiniteAutomataStructureDto
    {
        public HashSet<StateDTO> States { get; set; }
        public HashSet<TransitionDTO> Transitions { get; set; }

        public FiniteAutomataStructureDto(HashSet<IState> states)
        {
            this.Transitions = new HashSet<TransitionDTO>();
            this.States = new HashSet<StateDTO>();
            int counter = 0;
            foreach (var state in states)
            {
                this.States.Add(StateDTO.FromModel(state));
            } 
            foreach (var state in states)
            {
                foreach (var direciton in state.Directions)
                {
                    foreach (var letter in direciton.Value)
                    {
                        this.Transitions.Add(new TransitionDTO(++counter,StateDTO.FromModel(state), StateDTO.FromModel(direciton.Key), letter));
                    }
                }
            }
        }
    }
}
