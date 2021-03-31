using NUnit.Framework;
using logic.ExpressionService.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;

namespace Tests.ExpressionService.Tests.Extensions
{
    class HelperExtensionsTests
    {
        [Test]
        [TestCase(">", true)]
        [TestCase("|", true)]
        [TestCase("=", true)]
        [TestCase("&", true)]
        [TestCase("~", true)]
        [TestCase("@", false)]
        [TestCase("!", false)]
        [TestCase("$", false)]
        [TestCase("%", true)]
        [TestCase("^", false)]
        [TestCase("(", false)]
        [TestCase(")", false)]
        [TestCase("_", false)]
        [TestCase("-", false)]
        [TestCase("+", false)]
        [TestCase("`", false)]
        public void Should_Return_Whether_Charracter_Is_Operator(char character, bool expected)
        {
            character.IsOperator().Should().Be(expected);
        } 
        
        [Test]
        [TestCase(Operators.Implication,">")]
        [TestCase(Operators.Disjunction,"|")]
        [TestCase(Operators.Biimplication,"=")]
        [TestCase(Operators.Conjunction,"&")]
        [TestCase(Operators.Negation,"~")]
        [TestCase(Operators.Nand,"%")]
        public void Should_Return_The_Value_Coresponding_To_The_Passed_Operator(Operators op, string expected)
        {
            op.OperatorValue().Should().Be(expected);
        } 
        
        [Test]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        public void Should_Return_Whether_Number_Is_Boolean_True_False(int number, bool expected)
        {
            number.ToBool().Should().Be(expected);
        }

  
        [Test]
        public void CountSortNodes_Should_Sort_List_Of_Nodes_From_A_To_z()
        {
            var nodeA = new Node('A', 1);
            var nodea = new Node('a', 1);
            var nodeB = new Node('B', 1);
            var nodeb = new Node('b', 1);
            var nodeC = new Node('C', 1);

            var listToSort = new List<INode>() { nodeA, nodea, nodeB, nodeb, nodeC };
            var expectedResult = new List<INode>() { nodeA, nodeB, nodeC, nodea, nodeb };

            HelperExtensions.CountSortNodes(listToSort).Should().BeEquivalentTo(expectedResult);
        }



    }
}
