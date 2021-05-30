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
        public SortedSet<TransitionDTO> Transitions { get; set; }

        public FiniteAutomataStructureDto(IFiniteAutomataStructure structure)
        {
            this.Transitions = new SortedSet<TransitionDTO>();
            this.States = new HashSet<StateDTO>();
            int counter = 0;
            GetStatesAccordingToDirections(structure.GetInitialState());
            foreach (var state in structure.States)
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

        private void GetStatesAccordingToDirections(IState state)
        {
            this.States.Add(StateDTO.FromModel(state));
            foreach (var direction in state.Directions)
            {
                if (!this.States.Contains(StateDTO.FromModel(direction.Key)))
                {
                    this.States.Add(StateDTO.FromModel(direction.Key));
                    GetStatesAccordingToDirections(direction.Key);
                }
            }
        }
    }
}
