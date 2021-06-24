using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.FiniteAutomataService.Tests.Models
{
    class FiniteAutomataStructureTests
    {
        private static readonly object[] REGEX_LETTERS_DFA_FINITE = {
            new object[] { "*(a)",           1,false,false},
            new object[] { "|(a,b)",          2,false,true},
            new object[] { ".(a,b)",           2,false,true},
            new object[] { "*(|(a,b))",         2,false,false},
            new object[] { "*(.(a,b))",          2,false,false},
            new object[] { "*(*(*(*(a))))",       1,false,false},
            new object[] { "|(*(a),*(b))",         2,false,false},
            new object[] { ".(*(a),*(b))",          2,false,false},
            new object[] { ".(.(a,b),.(c,d))",       4,false,true},
            new object[] { "|(|(a,b),|(c,d))",        4,false,true},
            new object[] { "*(|(*(.(a,b)),c))",        3,false,false},
            new object[] { "|(.(.(a,b),.(c,d)),z)",     5,false,true},
            new object[] { ".(|(|(a,b),|(c,d)),z)",      5,false,true},
            new object[] { "*|(*(|(|(a,b),c)),.(e,f))",   5,false,false},
            new object[] { "|(*(|(*(|(a,b)),c)),|(e,f))",  5,false,false},
        };
        private static readonly object[] NON_DFA_REGEX = {
            new object[] { "*(a)" },
            new object[] { "|(a,b)" },
            new object[] { ".(a,b)" },
            new object[] { "*(|(a,b))" },
            new object[] { "*(.(a,b))" },
            new object[] { "*(*(*(*(a))))" },
            new object[] { "|(*(a),*(b))" },
            new object[] { ".(*(a),*(b))" },
            new object[] { ".(.(a,b),.(c,d))" },
            new object[] {"|(|(a,b),|(c,d))" },
            new object[] { "*(|(*(.(a,b)),c))" },
            new object[] { "|(.(.(a,b),.(c,d)),z)" },
            new object[] {".(|(|(a,b),|(c,d)),z)" },
            new object[] { "*|(*(|(|(a,b),c)),.(e,f))" },
            new object[] { "|(*(|(*(|(a,b)),c)),|(e,f))" },
        };
        [Test]
        public void Should_Return_The_First_Initial_State_Successfully()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            IState initial = new State(1, "1", true);
            str.States.Add(initial);
            str.States.Add(new State(2, "2", true));

            Assert.AreEqual(str.GetInitialState(), initial);
        }
        
        [Test]
        public void Should_Return_All_Final_States_Successfully()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            IState final1 = new State(1, "1", true,true);
            IState final2 = new State(2, "2", false,true);
            str.States.Add(final1);
            str.States.Add(final2);

            Assert.AreEqual(str.GetFinalStates(), new HashSet<IState>() { final1,final2});
        }

        [Test]
        public void Should_Return_That_Structure_Is_Not_DFA_If_Contains_Epsilon()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            str.StructureAlphabet.Letters.Add(Alphabet.EPSILON_LETTER);

            Assert.IsFalse(str.IsDFA);
        }

        [Test]
        public void Should_Return_That_Structure_Is_Not_DFA_If_There_Are_Directions_That_Dont_Include_All_Letters()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            ILetter letter = new Letter('s');
            IState state1 = new State(1, "1", true, true);
            IState state2 = new State(2, "2", false, true);
            state1.Directions.Add(state2, new HashSet<DirectionValue>() { new DirectionValue { Letter = letter } });

            str.States.Add(state1);
            str.States.Add(state2);
            str.StructureAlphabet.Letters.Add(letter);

            Assert.IsFalse(str.IsDFA);
        }

        [Test]
        public void Should_Return_That_Structure_Is_DFA_If_There_Are_Directions_That_Include_All_Letters()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            ILetter letter = new Letter('s');
            IState state1 = new State(1, "1", true, true);
            IState state2 = new State(2, "2", false, true);
            state1.Directions.Add(state2, new HashSet<DirectionValue>() { new DirectionValue { Letter = letter } });
            state2.Directions.Add(state1, new HashSet<DirectionValue>() { new DirectionValue { Letter = letter } });

            str.States.Add(state1);
            str.States.Add(state2);
            str.StructureAlphabet.Letters.Add(letter);

            Assert.IsTrue(str.IsDFA);
        }

        [Test]
        public void Should_Return_That_Structure_Is_Not_Finite_If_There_Are_Self_Referenced_States_That_Reach_Final_State()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            IState state1 = new State(1, "1", true, true);
            state1.Directions.Add(state1, new HashSet<DirectionValue>() { new DirectionValue { Letter = new Letter('s') } });

            str.States.Add(state1);

            Assert.IsFalse(str.IsFinite);
        }

        [Test]
        public void Should_Return_That_Structure_Is_Not_Finite_If_There_Are_States_In_A_Loop_With_Letters()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            IState initial = new State(1, "1", true);
            IState state2 = new State(2, "2", true);
            IState state3 = new State(3, "3", true);
            IState final = new State(4, "4", true, true);
            initial.Directions.Add(state2, new HashSet<DirectionValue>() { new DirectionValue { Letter = new Letter('s') } });
            state2.Directions.Add(state3, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });
            state3.Directions.Add(initial, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });
            state3.Directions.Add(final, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            str.States.Add(initial);
            str.States.Add(final);

            Assert.IsFalse(str.IsFinite);
        }
        [Test]
        public void Should_Return_That_Structure_Is_Finite_If_There_Are_States_In_A_Loop_With_Epsilon_Letters()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            IState initial = new State(1, "1", true);
            IState state2 = new State(2, "2", true);
            IState state3 = new State(3, "3", true);
            IState final = new State(4, "4", true, true);
            initial.Directions.Add(state2, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });
            state2.Directions.Add(state3, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });
            state3.Directions.Add(initial, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });
            state3.Directions.Add(final, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            str.States.Add(initial);
            str.States.Add(final);

            Assert.IsTrue(str.IsFinite);
        }
        [Test]
        public void Should_Generate_DFA_Instructions_For_Non_DFA_Structure_And_Verfiy_That_It_Is_DFA_After_Evaluation()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            ILetter letter = new Letter('s');
            str.StructureAlphabet.Letters.Add(letter);
            IState initial = new State(1, "1", true);
            IState state2 = new State(2, "2", true);
            IState state3 = new State(3, "3", true);
            IState final = new State(4, "4", true, true);
            initial.Directions.Add(state2, new HashSet<DirectionValue>() { new DirectionValue { Letter = letter } });
            state2.Directions.Add(state3, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER} });
            state3.Directions.Add(initial, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });
            state3.Directions.Add(final, new HashSet<DirectionValue>() { new DirectionValue { Letter = Alphabet.EPSILON_LETTER } });

            str.States.Add(initial);
            str.States.Add(final);

            str.GenerateDFAInstructions(new Mock<IConfiguration>().Object);
            IFiniteAutomataStructure DFA = new FiniteAutomataStructure(
                                                    new InstructionsInput { instructions = str.DFAInstructions });

            Assert.IsTrue(DFA.IsDFA);
        }
        
        [Test]
        public void Should_Generate_Instructions_Successfully()
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            ILetter letter = new Letter('s');
            str.StructureAlphabet.Letters.Add(letter);

            IState initial = new State(1, "1", true);
            IState final = new State(2, "2", false, true);

            initial.Directions.Add(final, new HashSet<DirectionValue>() { new DirectionValue { Letter = letter } });
          
            str.States.Add(initial);
            str.States.Add(final);

            str.GenerateOriginalInstructions(new Mock<IConfiguration>().Object);
            IFiniteAutomataStructure newStr = new FiniteAutomataStructure(
                                                    new InstructionsInput { instructions = str.OriginalInstructions });

            Assert.AreEqual(newStr.States,str.States);
        } 
        [Test]
        public void WordExists_Should_Call_Initial_State_Method_For_Checking_Word_Successfully()
        {
            string wordToCheck = "s";
            IFiniteAutomataStructure str = new FiniteAutomataStructure();
            Mock<IState> initial = new Mock<IState>();
            initial.Setup(s => s.Initial).Returns(true);

            str.States.Add(initial.Object);
            str.WordExists(wordToCheck);

            initial.Verify(s => s.CheckWord(wordToCheck));
        }
        
        [Test]
        [TestCaseSource(nameof(REGEX_LETTERS_DFA_FINITE))]
        public void Should_Build_Structure_From_Regex_AnD_Verify_Alphabet_DFA_And_Finite(string regex,int letters,bool dfa, bool finite)
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure(
                                            new InstructionsInput { instructions = "regex: " + regex + "\n"});

            Assert.AreEqual(str.StructureAlphabet.Letters.Count, letters);
            Assert.AreEqual(str.IsDFA, dfa);
            Assert.AreEqual(str.IsFinite, finite);
        }
        
        [Test]
        [TestCaseSource(nameof(NON_DFA_REGEX))]
        public void Should_Build_Structure_From_Regex_And_Build_DFA(string regex)
        {
            IFiniteAutomataStructure str = new FiniteAutomataStructure(
                                            new InstructionsInput { instructions = "regex: " + regex + "\n"});

            str.BuildDFA();

            Assert.IsTrue(str.CheckDFA(str.DFA));
        }
    }
}
