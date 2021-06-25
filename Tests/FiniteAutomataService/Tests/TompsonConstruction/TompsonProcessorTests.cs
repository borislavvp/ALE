using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using logic.FiniteAutomataService.Models.TompsonConstruction;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.FiniteAutomataService.Tests.Models
{
    class TompsonProcessorTests
    {
        private static readonly object[] TOMPSON_RULES_CHECKER = {
            new object[] { TompsonRules.CONCATENATION, true },
            new object[] { TompsonRules.REPETITION, true },
            new object[] { TompsonRules.CHOICE, true },
            new object[] { 'a', false},
            new object[] { '@', false},
            new object[] { '#', false },
            new object[] { '$', false},
            new object[] { '%', false},
            new object[] { '!', false },
            new object[] { '&', false},
            new object[] { '(', false },
            new object[] { ')', false },
            new object[] { '-', false },
            new object[] { '=', false },
            new object[] { '+', false },
            new object[] { '_', false },
            new object[] { '\'', false },
            new object[] { '/', false },
            new object[] { '\\',false },
            new object[] { ']', false },
            new object[] { '[', false },
            new object[] { ',', false },
            new object[] { '`', false },
        };

        [Test]
        [TestCaseSource(nameof(TOMPSON_RULES_CHECKER))]
        public void Should_Determine_Successfully_Whether_Character_Is_Tompson_Rule(char rule,bool expectedResult)
        {
            Assert.AreEqual(TompsonProcessor.IsTompsonRule(rule), expectedResult);
        }

        [Test]
        public void Should_Process_Repetition_Rule_Successfully()
        {
            IAutomataStructure str = new AutomataStructure();
            int statesId = 1;
            IState initial = new State(statesId++, "1",true);
            IState state2 = new State(statesId, "2");
            Stack<TompsonInitialFinalStatesHelperPair> stack = new Stack<TompsonInitialFinalStatesHelperPair>();

            str.States.Add(initial);
            str.States.Add(state2);

            initial.Directions.Add(state2,
                        new HashSet<DirectionValue>() { new DirectionValue { Letter = new Letter('a') } });

            stack.Push(new TompsonInitialFinalStatesHelperPair()
            {
                CurrentFinal = state2,
                CurrentInitial = initial
            });

            var result = TompsonProcessor.ProcessRule(TompsonRules.REPETITION, stack, str,ref statesId);
            Assert.IsTrue(initial.Directions.ContainsKey(result.CurrentFinal));
            Assert.IsTrue(result.CurrentFinal.Directions.ContainsKey(initial));
            Assert.IsTrue(state2.Directions.ContainsKey(result.CurrentFinal));
        }
        [Test]
        public void Should_Process_Concatenation_Rule_Successfully()
        {
            IAutomataStructure str = new AutomataStructure();
            int statesId = 1;
            IState initial = new State(statesId++, "1",true);
            IState state2 = new State(statesId++, "2");
            
            IState initial2 = new State(statesId++, "3",true);
            IState state4 = new State(statesId, "4");
            Stack<TompsonInitialFinalStatesHelperPair> stack = new Stack<TompsonInitialFinalStatesHelperPair>();

            str.States.Add(initial);
            str.States.Add(state2);
            str.States.Add(initial2);
            str.States.Add(state4);

            initial.Directions.Add(state2,
                        new HashSet<DirectionValue>() { new DirectionValue { Letter = new Letter('a') } });
            
            initial2.Directions.Add(state4,
                        new HashSet<DirectionValue>() { new DirectionValue { Letter = new Letter('a') } });

            stack.Push(new TompsonInitialFinalStatesHelperPair()
            {
                CurrentFinal = state4,
                CurrentInitial = initial2
            });
            stack.Push(new TompsonInitialFinalStatesHelperPair()
            {
                CurrentFinal = state2,
                CurrentInitial = initial
            });

            var result = TompsonProcessor.ProcessRule(TompsonRules.CONCATENATION, stack, str,ref statesId);

            Assert.AreSame(result.CurrentInitial,initial);
            Assert.AreSame(result.CurrentFinal,state4);

            Assert.IsTrue(state2.Directions.ContainsKey(initial2));
        }
        [Test]
        public void Should_Process_Choice_Rule_Successfully()
        {
            IAutomataStructure str = new AutomataStructure();
            int statesId = 1;
            IState initial = new State(statesId++, "1",true);
            IState state2 = new State(statesId++, "2");
            
            IState initial2 = new State(statesId++, "3",true);
            IState state4 = new State(statesId, "4");
            Stack<TompsonInitialFinalStatesHelperPair> stack = new Stack<TompsonInitialFinalStatesHelperPair>();

            str.States.Add(initial);
            str.States.Add(state2);
            str.States.Add(initial2);
            str.States.Add(state4);

            initial.Directions.Add(state2,
                        new HashSet<DirectionValue>() { new DirectionValue { Letter = new Letter('a') } });
            
            initial2.Directions.Add(state4,
                        new HashSet<DirectionValue>() { new DirectionValue { Letter = new Letter('a') } });

            stack.Push(new TompsonInitialFinalStatesHelperPair()
            {
                CurrentFinal = state4,
                CurrentInitial = initial2
            });
            stack.Push(new TompsonInitialFinalStatesHelperPair()
            {
                CurrentFinal = state2,
                CurrentInitial = initial
            });

            var result = TompsonProcessor.ProcessRule(TompsonRules.CHOICE, stack, str,ref statesId);

            Assert.IsTrue(result.CurrentInitial.Directions.ContainsKey(initial));
            Assert.IsTrue(result.CurrentInitial.Directions.ContainsKey(initial2));

            Assert.IsTrue(state4.Directions.ContainsKey(result.CurrentFinal));
            Assert.IsTrue(state2.Directions.ContainsKey(result.CurrentFinal));

        }
    }
}
