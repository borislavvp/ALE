using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using logic.ExpressionService.Common.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using System.Linq;

namespace Tests.ExpressionService.Tests.Extensions
{
    class ExpressionExtensionsTests
    {
        [Test]
        [TestCase("|(=(A,B),&(C,A))", "|=AB&CA")]
        [TestCase("|(>(~(A),B)>(~(a),>(~(C),A)))", "|>~AB>~a>~CA")]
        [TestCase(">(|(~(A),C)&(~(a),|(~(W),|(~(B),Q))))", ">|~AC&~a|~W|~BQ")]
        [TestCase("=(>(~(A),C)|(~(a),&(~(W),Q)))", "=>~AC|~a&~WQ")]
        [TestCase("=(>(~(A),C)|(~(>(~(a),e)),&(~(W),>(~(T),>(~(A),>(~(A),>(~(A),C)))))))", "=>~AC|~>~ae&~W>~T>~A>~A>~AC")]
        public void Should_Remove_Brackets_Commans_And_WhiteSpaces_From_Passed_Expression(string expressionValue,string expectedResult)
        {
            IExpression expression = new PrefixExpression(expressionValue);
            expression.ParseExpression().Value.Should().BeEquivalentTo(expectedResult);
        }
        
        [Test]
        [TestCase("|(=(A,B),&(C,A))", "((A=B)|(C&A))")]
        [TestCase("|(>(~(A),B)>(~(a),>(~(C),A)))", "((~(A)>B)|(~(a)>(~(C)>A)))")]
        [TestCase(">(|(~(A),C)&(~(a),|(~(W),|(~(B),Q))))", "((~(A)|C)>(~(a)&(~(W)|(~(B)|Q))))")]
        [TestCase("=(>(~(A),C)|(~(a),&(~(W),Q)))", "((~(A)>C)=(~(a)|(~(W)&Q)))")]
        public void Should_Transform_Passed_Prefix_Expression_To_Infix_Notation(string expressionValue,string expectedResult)
        {
            IPrefixExpression expression = new PrefixExpression(expressionValue);
            expression.TransformToInfix().Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        [TestCase("|(=(A,B),&(C,A))")]
        [TestCase("|(>(~(A),B)>(~(a),>(~(C),A)))")]
        [TestCase(">(|(~(A),C)&(~(a),|(~(W),|(~(B),Q))))")]
        [TestCase("=(>(~(A),C)|(~(a),&(~(W),Q)))")]
        public void BuildExpressionTree_Should_Create_ExpressionTree_From_PrefixExpression(string prefixExpressionValue)
        {
            IPrefixExpression prefixExpression = new PrefixExpression(prefixExpressionValue);

            var tree = prefixExpression.BuildExpressionTree();

            tree.GetNodes()
                .Select(n => n.Value)
                .Should()
                .BeEquivalentTo(prefixExpression.ParseExpression().Value);
        } 
        
        [Test]
        [TestCase("|(|(A,B),C)", "%(%(%(%(A,A),%(B,B)),%(%(A,A),%(B,B))),%(C,C))")]
        [TestCase("=(|(A,B),C))", "%(%(%(%(%(A,A),%(B,B)),%(%(A,A),%(B,B))),%(C,C)),%(%(%(A,A),%(B,B)),C))")]
        [TestCase(">(>(A,B),C))", "%(%(A,%(B,B)),%(C,C))")]
        public void Nandify_Should_Create_ExpressionTree_From_PrefixExpression(string prefixExpressionValue,string expected)
        {
            IPrefixExpression prefixExpression = new PrefixExpression(prefixExpressionValue);
            prefixExpression.NandifyExpression().Should().BeEquivalentTo(expected);
        }

    }
}
