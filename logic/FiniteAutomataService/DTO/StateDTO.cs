﻿using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace logic.FiniteAutomataService.DTO
{
    public class StateDTO : IComparable<StateDTO>
    {
        public int Id { get; set; }
        public bool Initial { get; set; }
        public bool Final { get; set; }
        public string Value { get; set; }

        public static StateDTO FromModel(IState state)
        {
            return new StateDTO
            {
                Id = state.Id,
                Initial = state.Initial,
                Final = state.Final,
                Value = state.Value
            };
        }

        public int CompareTo([DisallowNull] StateDTO other)
        {
            return this.Initial 
                ? -1 
                : other.Initial 
                    ? 1 
                    : this.Final 
                        ? 1 
                        : other.Final 
                            ? -1 
                            : this.Id.CompareTo(other.Id);
        }
    }
}
