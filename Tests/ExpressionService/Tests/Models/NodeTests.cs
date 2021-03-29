using FluentAssertions;
using logic.ExpressionService.Common.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ExpressionService.Tests.Models
{
    class NodeTests
    {
        [Test]
        public void Should_Create_Valid_Node_With_Passed_Value_And_Level_With_Default_Property_IsLeaf_Equal_To_False()
        {
            var value = 'A';
            var level = 1;

            Node node = new Node(value,level);

            node.Level.Should().Be(level);
            node.Value.Should().BeEquivalentTo(value);
            node.IsLeaf.Should().BeFalse();
        }
        
        [Test]
        public void Should_Create_Valid_Node_With_Passed_Value_Level_And_IsLeaf_Parameters()
        {
            var value = 'A';
            var level = 1;
            var IsLeaf = true;

            Node node = new Node(value,level, IsLeaf);

            node.Level.Should().Be(level);
            node.Value.Should().BeEquivalentTo(value);
            node.IsLeaf.Should().Be(IsLeaf);
        }

        [Test]
        public void ToString_Should_Return_Node_Value_Converted_To_String()
        {
            var value = 'A';
            var level = 1;

            Node node = new Node(value, level);

            node.ToString().Should().BeEquivalentTo($@"{value}");
        }

        [Test]
        public void Equals_Should_Return_True_When_Two_Nodes_Have_The_Same_Value()
        {
            var value = 'A';
            var level = 1;

            Node node1 = new Node(value, level);
            Node node2 = new Node(value, level);

            node1.Equals(node2).Should().BeTrue();
        }
        
        [Test]
        public void Equals_Should_Return_False_When_Two_Nodes_Have_Different_Value()
        {
            var value1 = 'A';
            var value2 = 'B';
            var level = 1;

            Node node1 = new Node(value1, level);
            Node node2 = new Node(value2, level);

            node1.Equals(node2).Should().BeFalse();
        }
    }
}
