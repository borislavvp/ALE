using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models.TompsonConstruction
{
    public static class TompsonProcessor
    {
        public static bool IsTompsonRule(this char value)
        {
            return value switch
            {
                '*' => true,
                '.' => true,
                '|' => true,
                _ => false,
            };
        }
        public static TompsonInitialFinalStatesHelperPair ProcessRule(
            char rule, 
            Stack<TompsonInitialFinalStatesHelperPair> processedValues,
            IFiniteAutomataStructure structure,
            ref int latestId)
        {
            switch (rule)
            {
                case TompsonRules.REPETITION:
                    return ProcessRepetitionRule(processedValues, structure, ref latestId);
                case TompsonRules.CONCATENATION:
                    return ProcessConcatenationRule(processedValues, structure, ref latestId);
                case TompsonRules.CHOICE:
                    return ProcessChoiceRule(processedValues, structure, ref latestId);
                default:
                    return null;
            }
        }

        private static TompsonInitialFinalStatesHelperPair ProcessRepetitionRule(
            Stack<TompsonInitialFinalStatesHelperPair> processedValues,
            IFiniteAutomataStructure structure,
            ref int latestId)
        {
            var pairToProcess = processedValues.Pop();
            pairToProcess.CurrentFinal.Final = false;
            if (pairToProcess.CurrentExtendedConnection != null && pairToProcess.CurrentFinal.Directions.ContainsKey(pairToProcess.CurrentExtendedConnection))
            {
                structure.States.Remove(pairToProcess.CurrentExtendedConnection);
                pairToProcess.CurrentFinal.Directions.Remove(pairToProcess.CurrentExtendedConnection);
            }
            var newFinalState = new State(++latestId, "", false);

            newFinalState.Directions.Add(pairToProcess.CurrentInitial,
                new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });
            pairToProcess.CurrentFinal.Directions.Add(newFinalState,
                new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });
            pairToProcess.CurrentInitial.Directions.Add(newFinalState,
                new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            var newExtendedConnection = new State(++latestId, "", false, true);
            newFinalState.Directions.Add(newExtendedConnection,
                new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            structure.States.Add(newExtendedConnection);
            structure.States.Add(newFinalState);

            return new TompsonInitialFinalStatesHelperPair()
            {
                CurrentFinal = newFinalState,
                CurrentInitial = pairToProcess.CurrentInitial,
                CurrentExtendedConnection = newExtendedConnection
            };
        } 
        private static TompsonInitialFinalStatesHelperPair ProcessConcatenationRule(
            Stack<TompsonInitialFinalStatesHelperPair> processedValues,
            IFiniteAutomataStructure structure,
            ref int latestId)
        {
            var pairToConcatFrom = processedValues.Pop();
            var pairToConcatTo = processedValues.Pop();
            pairToConcatFrom.CurrentFinal.Final = false;
            pairToConcatTo.CurrentInitial.Initial = false;

            if (pairToConcatFrom.CurrentExtendedConnection != null && pairToConcatFrom.CurrentFinal.Directions.ContainsKey(pairToConcatFrom.CurrentExtendedConnection))
            {
                structure.States.Remove(pairToConcatFrom.CurrentExtendedConnection);
                pairToConcatFrom.CurrentFinal.Directions.Remove(pairToConcatFrom.CurrentExtendedConnection);
            }

            pairToConcatFrom.CurrentFinal.Directions.Add(pairToConcatTo.CurrentInitial,
               new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            if (pairToConcatTo.CurrentExtendedConnection == null || !pairToConcatTo.CurrentFinal.Directions.ContainsKey(pairToConcatTo.CurrentExtendedConnection))
            {
                var newExtendedConnection = new State(++latestId, "", false, true);
                structure.States.Add(newExtendedConnection);
                pairToConcatTo.CurrentFinal.Final = false;
                pairToConcatTo.CurrentFinal.Directions.Add(newExtendedConnection,
                   new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });
                pairToConcatTo.CurrentExtendedConnection = newExtendedConnection;
            }

            return new TompsonInitialFinalStatesHelperPair()
            {
                CurrentFinal = pairToConcatTo.CurrentFinal,
                CurrentInitial = pairToConcatFrom.CurrentInitial,
                CurrentExtendedConnection = pairToConcatTo.CurrentExtendedConnection
            };
        }
        
        private static TompsonInitialFinalStatesHelperPair ProcessChoiceRule(
            Stack<TompsonInitialFinalStatesHelperPair> processedValues,
            IFiniteAutomataStructure structure,
            ref int latestId)
        {
            var pair1 = processedValues.Pop();
            var pair2 = processedValues.Pop();

            var newInitialState = new State(++latestId, "", true);
            pair1.CurrentInitial.Initial = false;
            pair1.CurrentFinal.Final = false;

            pair2.CurrentInitial.Initial = false;
            pair2.CurrentFinal.Final = false;

            var newExtendedConnection = new State(++latestId, "", false,true);

            if (pair1.CurrentExtendedConnection != null && pair1.CurrentFinal.Directions.ContainsKey(pair1.CurrentExtendedConnection))
            {
                structure.States.Remove(pair1.CurrentExtendedConnection);
                pair1.CurrentFinal.Directions.Remove(pair1.CurrentExtendedConnection);
            }
            pair1.CurrentFinal.Directions.Add(newExtendedConnection,
                new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            if (pair2.CurrentExtendedConnection != null && pair2.CurrentFinal.Directions.ContainsKey(pair2.CurrentExtendedConnection))
            {
                structure.States.Remove(pair2.CurrentExtendedConnection);
                pair2.CurrentFinal.Directions.Remove(pair2.CurrentExtendedConnection);
            }
            pair2.CurrentFinal.Directions.Add(newExtendedConnection,
                new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            newInitialState.Directions.Add(pair1.CurrentInitial,
                new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });
            newInitialState.Directions.Add(pair2.CurrentInitial,
                new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            structure.States.Add(newExtendedConnection);
            structure.States.Add(newInitialState);
            return new TompsonInitialFinalStatesHelperPair()
            {
                CurrentFinal = newExtendedConnection,
                CurrentInitial = newInitialState,
                CurrentExtendedConnection = newExtendedConnection
            };
        }
    }
}
