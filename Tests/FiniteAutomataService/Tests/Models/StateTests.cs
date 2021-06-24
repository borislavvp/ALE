using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.FiniteAutomataService.Helpers;

namespace Tests.FiniteAutomataService.Tests.Models
{
    class StateTests
    {
        private static readonly object[] VALID_STATES = StatesHelpers.VALID_STATES;
        private static readonly object[] EPSILON_CLOSURES = StatesHelpers.MockEpsillonClosures();
        private static readonly object[] EPSILON_SELF_REFERENCED_STATES = StatesHelpers.MockSelfReferencedStatesWithLetter();
        private static readonly object[] SELF_REFERENCED_STATES = StatesHelpers.MockSelfReferencedStatesWithLetter();
        private static readonly IState abcdefgh_CONSEQUENT_STATES_FINAL_cfh = StatesHelpers.Generate_abcdefgh_ConsequentStatesWithFinal_cfh();
        private static readonly object[] abcdefgh_WORDS_CHECK = {
            new object[] { "a",        false},
            new object[] { "ab",        false},
            new object[] { "abc",        true },
            new object[] { "abcd",        false},
            new object[] { "abcde",        false},
            new object[] { "abcdef",        true },
            new object[] { "abcdefg",        false},
            new object[] { "abcdefgh",        true },
            new object[] { "abcdefgha",        false },
            new object[] { "abcdefghab",        false },
            new object[] { "abcdefghabc",        true },
            new object[] { "abcdefghabcd",        false },
            new object[] { "abcdefghabcde",        false },
            new object[] { "abcdefghabcdef",        true },
            new object[] { "abcdefghabcdefg",        false },
            new object[] { "abcdefghabcdefgh",        true },
        };

        [Test]
        [TestCaseSource(nameof(VALID_STATES))]
        public void Should_Add_Direction_To_State_And_Succesfully_Return_That_Can_Reach_It(IState state)
        {
            IState current = new State(0, "State0");
            current.Directions.Add(state, new HashSet<DirectionValue>() { new DirectionValue {Letter = new Letter('a') } });
            Assert.IsTrue(current.CanReachStates(new HashSet<IState>() { state }));
        }
        [Test]
        [TestCaseSource(nameof(VALID_STATES))]
        public void Should_Return_That_Can_NOT_Reach_Passed_State_That_Is_Not_Added_As_Direction(IState state)
        {
            IState current = new State(0, "State0");
            Assert.IsFalse(current.CanReachStates(new HashSet<IState>() { state }));
        }
        
        [Test]
        [TestCaseSource(nameof(EPSILON_CLOSURES))]
        public void Should_Return_Successfully_The_According_Number_Of_Epsilon_Closures(IState state,int expectedResult)
        {
            Assert.AreEqual(state.FindEpsilonClosures().Count, expectedResult);
        } 
        
        [Test]
        [TestCaseSource(nameof(EPSILON_SELF_REFERENCED_STATES))]
        public void Should_Return_Successfully_The_According_Number_Of_Self_Referenced_States_For_Only_Epsilons
            (IState state,int expectedResult)
        {
            Assert.AreEqual(state.GetAllSelfReferencedStates().Count, expectedResult);
        }
        
        [Test]
        [TestCaseSource(nameof(SELF_REFERENCED_STATES))]
        public void Should_Return_Successfully_The_According_Number_Of_Self_Referenced_States_For_Normal_Letters
            (IState state,int expectedResult)
        {
            Assert.AreEqual(state.GetAllSelfReferencedStates().Count, expectedResult);
        }

        [Test]
        [TestCaseSource(nameof(abcdefgh_WORDS_CHECK))]
        public void Should_Return_Successfully_Whether_Word_Exists_In_Predefined_States
            (string word,bool expectedResult)
        {
            Assert.AreEqual(abcdefgh_CONSEQUENT_STATES_FINAL_cfh.CheckWord(word), expectedResult);
        }

    }
}
