using logic.FiniteAutomataService.Constants;
using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Extensions;
using logic.FiniteAutomataService.Helpers;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using logic.FiniteAutomataService.Models.TompsonConstruction;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tests.FiniteAutomataService.Tests
{
    public class WIP
    {
        [Test]
        public async Task WIP_TEST()
        {
            //InstructionsInput input = new InstructionsInput()
            //{
            //    instructions = "regex:|(|(a,b),|(c,d))"
            //    //instructions = "regex:*(|(.(*(c),a),n))"
            //};
            //var str = input.BuildStructure();
            //var asd = new SortedSet<IState>();
            //var st = new State(1, "ASD", true);
            //var sd = new State(4, "ASD", true);
            //asd.Add(st);
            //asd.Add(sd);
            //var states = "(?<=states:)(.*?)\n|\r|\r\n";
            //var alphabet = "(?<=alphabet:)(.*?)\n|\r|\r\n";
            //var final = "(?<=final:)(.*?)\n|\r|\r\n";
            //var transitions = @"(?<=transitions:)(\s.*?)*(?=end.)";
            ////st.Directions.Add(sd, new HashSet<ILetter>() { Alphabet.EPSILON_LETTER });
            var asd = new InstructionsInput { instructions = "alphabet: xyz\r\nstates: A1,B1,C1,A2,B2,C2\r\nfinal: C2\ntransitions:\nA1,_ --> A1\nA1,_ --> B1\nA1,_ --> C1\nB1,_ --> A1\nB1,_ --> B1\nB1,_ --> C1\nC1,_ --> A1\nC1,_ --> B1\nC1,_ --> C1\n\nA2,_ --> A2\nA2,_ --> B2\nA2,_ --> C2\nB2,_ --> A2\nB2,_ --> B2\nB2,_ --> C2\nC2,_ --> A2\nC2,_ --> B2\nC2,_ --> C2\n\nC1,x --> A2\nA2,y --> C1\nend.\n\n" };
            //var rg = new Regex(states);
            //var w = rg.Match(asd.instructions);
            //var we = RegexHelper.Match(asd.instructions,Instruction.TRANSITIONS);
            //var we = Regex.Match(asd.instructions,Instruction.TRANSITIONS).Captures[0].Value.Trim();
            //var statesBuilder = new Dictionary<ILetter, HashSet<IState>>
            //{
            //    { new Letter('a'), new HashSet<IState>() },
            //    { new Letter('b'), new HashSet<IState>() }
            //};
            //statesBuilder[new Letter('a')].Add(new State(1,"ASD"));
            //statesBuilder[new Letter('b')].Add(new State(3,"ASD"));
            //statesBuilder[new Letter('a')].Add(new State(2,"A21SD"));
            var s = new AutomataStructure(asd);
            //await s.GenerateDFAInstructions();
        }
    }
}
