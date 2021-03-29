using FluentAssertions;
using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ExpressionService.Tests.Models
{
    class EdgeTests
    {
        [Test]
        public void Should_Create_Valid_Edge_With_Parent_And_Child()
        {
            var parent = new Mock<INode>().Object;
            var child = new Mock<INode>().Object;

            Edge edge = new Edge(child,parent);

            edge.Parent.Should().BeEquivalentTo(parent);
            edge.Child.Should().BeEquivalentTo(child);
        } 
        
        [Test]
        public void Equals_Should_Return_True_When_Two_Edges_Are_The_Same()
        {
            var parent = new Mock<INode>().Object;
            var child = new Mock<INode>().Object;

            Edge edge1 = new Edge(child,parent);
            Edge edge2 = new Edge(child,parent);

            edge1.Equals(edge2).Should().BeTrue();
        }
        
        [Test]
        public void Equals_Should_Return_False_When_Two_Edges_Are_Different()
        {
            var parent = new Mock<INode>().Object;
            var child = new Mock<INode>().Object;

            Edge edge1 = new Edge(child,parent);
            Edge edge2 = new Edge(new Mock<INode>().Object, parent);

            edge1.Equals(edge2).Should().BeFalse();
        }
    }
}
