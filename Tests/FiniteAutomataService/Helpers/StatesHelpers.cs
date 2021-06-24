using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.FiniteAutomataService.Helpers
{
    public static class StatesHelpers
    {
        private static IState STATE_1 = new State(1, "State1");
        private static IState STATE_2 = new State(2, "State2");
        private static IState STATE_3 = new State(3, "State3");
        private static IState STATE_4 = new State(4, "State4");

        private static void ResetStates()
        {
            STATE_1 = new State(1, "State1");
            STATE_2 = new State(2, "State2");
            STATE_3 = new State(3, "State3");
            STATE_4 = new State(4, "State4");
        }
        public static readonly object[] VALID_STATES = {
            new object[] { STATE_1 },
            new object[] { STATE_2 },
            new object[] { STATE_3 },
            new object[] { STATE_4 },
        };

        public static object[] MockEpsillonClosures()
        {
            ResetStates();

            //Closure 2 
            STATE_2.Directions.Add(
                STATE_1,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });

            //Closure 3 
            STATE_3.Directions.Add(
                STATE_1,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });
            
            STATE_3.Directions.Add(
                STATE_2,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });
            
            //Closure 4
            STATE_4.Directions.Add(
                STATE_1,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });
            
            STATE_4.Directions.Add(
                STATE_2,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });
            
            STATE_4.Directions.Add(
                STATE_3,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });

            return new object[4] {
                new object[] { STATE_1,1 },
                new object[] { STATE_2,2 },
                new object[] { STATE_3,3 },
                new object[] { STATE_4,4 },
            };
        }
        private static object[] GenerateMockSelfReferencedStatesWithLetter(ILetter letter)
        {
            ResetStates();

            //SelfReference 1
            STATE_1.Directions.Add(
                STATE_1,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter =letter} });
            
            //SelfReference 1
            STATE_2.Directions.Add(
                STATE_2,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} });
            
            //SelfReference 3
            STATE_3.Directions.Add(
                STATE_3,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} });
            
            //SelfReference 4
            STATE_4.Directions.Add(
                STATE_4,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} });

            //Connect the states 

            //STATE 1 - 3 Connections
            STATE_1.Directions.Add(
                STATE_2,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} }); 
            STATE_1.Directions.Add(
                STATE_3,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} }); 
            STATE_1.Directions.Add(
                STATE_4,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} });
            
            STATE_3.Directions.Add(
                STATE_2,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} });

            //STATE 2 - 2 Connections
            STATE_2.Directions.Add(
                STATE_1,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} }); 
            STATE_2.Directions.Add(
                STATE_3,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} }); 
            
            //STATE 3 - 1 Connection
            STATE_3.Directions.Add(
                STATE_4,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = letter} }); 
           

            return new object[4] {
                new object[] { STATE_1, letter.IsEpsilon ? 0 : 4 },
                new object[] { STATE_2, letter.IsEpsilon ? 0 : 3 },
                new object[] { STATE_3, letter.IsEpsilon ? 0 : 2 },
                new object[] { STATE_4, letter.IsEpsilon ? 0 : 1 },
            };
        }
        public static object[] MockSelfReferencedStatesWithOnlyEpsilons()
        {
            return GenerateMockSelfReferencedStatesWithLetter(Alphabet.EPSILON_LETTER);
        }
        public static object[] MockSelfReferencedStatesWithLetter()
        {
            return GenerateMockSelfReferencedStatesWithLetter(new Letter('a'));
        }
        public static IState Generate_abcdefgh_ConsequentStatesWithFinal_cfh()
        {
            ResetStates();
            IState STATE_5 = new State(5, "State5");
            IState STATE_6 = new State(6, "State6");
            IState STATE_7 = new State(7, "State7");
            IState STATE_8 = new State(8, "State8");
            STATE_1.Final = true;
            STATE_4.Final = true;
            STATE_7.Final = true;
            //A
            STATE_1.Directions.Add(
                STATE_2,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = new Letter('a')} });
            //AB
            STATE_2.Directions.Add(
                STATE_3,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = new Letter('b')} });
            //ABC
            STATE_3.Directions.Add(
                STATE_4,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = new Letter('c')} });
            //ABCD
            STATE_4.Directions.Add(
                STATE_5,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = new Letter('d')} });
            //ABCDE
            STATE_5.Directions.Add(
                STATE_6,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = new Letter('e')} });
            //ABCDEF
            STATE_6.Directions.Add(
                STATE_7,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = new Letter('f')} });
            //ABCDEFG
            STATE_7.Directions.Add(
                STATE_8,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = new Letter('g')} });
            //ABCDEFGH <- ...
            STATE_8.Directions.Add(
                STATE_1,
                new HashSet<DirectionValue>()
                { new DirectionValue { Letter = new Letter('h')} });

            return STATE_1;
        }
    }
}
