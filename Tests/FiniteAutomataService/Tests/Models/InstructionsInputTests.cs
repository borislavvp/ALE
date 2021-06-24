using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.FiniteAutomataService.Tests.Models
{
    class InstructionsInputTests
    {
        private static readonly object[] ALPHABET_INSTRUCTION_WITH_EXPECTED_RESULT = {
            new object[] { "a",        new HashSet<ILetter>() { new Letter('a') } },
            new object[] { "ab",        new HashSet<ILetter>() { new Letter('a'), new Letter('b') } },
            new object[] { "abc",        new HashSet<ILetter>() { new Letter('a'), new Letter('b'), new Letter('c') } },
            new object[] { "abcd",        new HashSet<ILetter>() { new Letter('a'), new Letter('b'), new Letter('c'), new Letter('d') } },
            new object[] { "abcde",        new HashSet<ILetter>() { new Letter('a'), new Letter('b'), new Letter('c'), new Letter('d'), new Letter('e') } },
            new object[] { "abcdef",        new HashSet<ILetter>() { new Letter('a'), new Letter('b'), new Letter('c'), new Letter('d'), new Letter('e'), new Letter('f') }  },
            new object[] { "abcdefg",        new HashSet<ILetter>() { new Letter('a'), new Letter('b'), new Letter('c'), new Letter('d'), new Letter('e'), new Letter('f'), new Letter('g') } },
            new object[] { "abcdefgh",        new HashSet<ILetter>() { new Letter('a'), new Letter('b'), new Letter('c'), new Letter('d'), new Letter('e'), new Letter('f'), new Letter('g'), new Letter('h') } },
        };   
        private static readonly object[] STATES_INSTRUCTION_WITH_EXPECTED_RESULT = {
            new object[] { "a",        new HashSet<IState>() { new State(1,"a",true) } },
            new object[] { "a,b",        new HashSet<IState>() { new State(1,"a", true), new State(2,"b") } },
            new object[] { "a,b,c",        new HashSet<IState>() { new State(1,"a", true), new State(2,"b"), new State(3,"c") } },
        };

        [Test]
        [TestCaseSource(nameof(ALPHABET_INSTRUCTION_WITH_EXPECTED_RESULT))]
        public void Should_Genarate_Alphabet_Successfully_From_Instructions(string alphabet, HashSet<ILetter> expected)
        {
            InstructionsInput input = new InstructionsInput { instructions = "alphabet: " + alphabet + "\n" };
            Assert.AreEqual(input.PopulateAlphabet().Letters, expected);
        } 
        
        [Test]
        [TestCaseSource(nameof(ALPHABET_INSTRUCTION_WITH_EXPECTED_RESULT))]
        public void Should_Genarate_Stack_Successfully_From_Instructions(string alphabet, HashSet<ILetter> expected)
        {
            InstructionsInput input = new InstructionsInput { instructions = "stack: " + alphabet + "\n" };
            Assert.AreEqual(input.PopulateStack(), expected);
        }

        [Test]
        [TestCaseSource(nameof(STATES_INSTRUCTION_WITH_EXPECTED_RESULT))]
        public void Should_Genarate_States_Successfully_From_Instructions(string states, HashSet<IState> expected)
        {
            InstructionsInput input = new InstructionsInput { instructions = "states: " + states + "\n" };
            Assert.AreEqual(input.PopulateStates(), expected);
        }
    }
}
