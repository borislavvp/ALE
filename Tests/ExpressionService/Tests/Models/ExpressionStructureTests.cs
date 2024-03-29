﻿using FluentAssertions;
using logic.ExpressionService.Common.Extensions;
using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ExpressionService.Tests.Models
{
    class ExpressionStructureTests
    {
        private static readonly string VALID_PREFIX = "|(=(A,B),&(C,A))";
        private static readonly string VALID_INFIX = "((A=B)|(C&A))";

        private static Mock<IPrefixExpression> _mockPrefixExpression;

        [SetUp]
        public void Setup()
        {
            _mockPrefixExpression = new Mock<IPrefixExpression>();
            _mockPrefixExpression.Setup(v => v.Value).Returns(VALID_PREFIX);
        }

        [Test]
        public void Should_Create_ExpressionStructure_With_Passed_Valid_Prefix_Expression()
        {
            ExpressionStructure structure = new ExpressionStructure(_mockPrefixExpression.Object);

            structure.PrefixExpression.Value.Should().BeEquivalentTo(VALID_PREFIX);
        }

        [Test]
        public void Should_Throw_NullReferenceException_When_Creating_ExpressionStructure_With_Passed_Null_Prefix_Expression()
        {
            Assert.Throws<NullReferenceException>(() => new ExpressionStructure(null));
        }

        [Test]
        public void ToString_Should_Return_InfixNotation_From_PrefixExpression()
        {
            ExpressionStructure structure = new ExpressionStructure(_mockPrefixExpression.Object);

            structure.ToString().Should().BeEquivalentTo(VALID_INFIX);
        }

        public void BuildExpressionTree_Should_Invoke_BuildingOfExpressionTree_On_Prefix_Expression(string prefixExpressionValue)
        {
            var mockTree = new Mock<IBinaryExpressionTree>();
            _mockPrefixExpression.Setup(p => p.BuildExpressionTree()).Returns(mockTree.Object);
            ExpressionStructure structure = new ExpressionStructure(_mockPrefixExpression.Object);

            structure.BuildExpressionTree();

            _mockPrefixExpression.Verify(p => p.BuildExpressionTree());
            structure.ExpressionTree.Should().BeEquivalentTo(mockTree.Object);
        }

        [Test]
        public void BuildTruthTable_Should_Create_TruthTable_From_ExpressionTree()
        {
            ExpressionStructure structure = new ExpressionStructure(_mockPrefixExpression.Object);

            structure.BuildExpressionTree();
            structure.BuildTruthTable();

            structure.TruthTable.Should().NotBeNull();
        }

        [Test]
        public void BuildTruthTable_Should_Throw_NullReferenceException_When_The_ExpressionTree_Is_Null()
        {
            ExpressionStructure structure = new ExpressionStructure(_mockPrefixExpression.Object);

            Assert.Throws<NullReferenceException>(() => structure.BuildTruthTable());
        }
    }
}
