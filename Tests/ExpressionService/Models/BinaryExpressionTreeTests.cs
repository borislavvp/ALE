using FluentAssertions;
using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ExpressionService.Models
{
    class BinaryExpressionTreeTests
    {
        private static readonly INode VALID_ROOT_NODE = new Node('|', 0, false);
        private static readonly INode VALID_LEFT_CHILD_NODE = new Node('a', 1, true);
        private static readonly IBinaryExpressionTree VALID_LEFT_CHILD = new BinaryExpressionTree(VALID_LEFT_CHILD_NODE);
        private static readonly INode VALID_RIGHT_CHILD_NODE = new Node('A', 1, true);
        private static readonly IBinaryExpressionTree VALID_RIGHT_CHILD = new BinaryExpressionTree(VALID_RIGHT_CHILD_NODE);

        private static readonly List<INode> EXPECTED_TRAVERSED_POSTORDERED = new List<INode>() { VALID_LEFT_CHILD_NODE, VALID_RIGHT_CHILD_NODE, VALID_ROOT_NODE };

        private static readonly List<INode> EXPECTED_NODES = new List<INode>() { VALID_ROOT_NODE, VALID_LEFT_CHILD_NODE, VALID_RIGHT_CHILD_NODE };

        private static readonly List<INode> EXPECTED_LEAFS = new List<INode>() { VALID_LEFT_CHILD_NODE, VALID_RIGHT_CHILD_NODE };

        private static readonly List<INode> EXPECTED_SORTED_LEAFS = new List<INode>() { VALID_RIGHT_CHILD_NODE, VALID_LEFT_CHILD_NODE };

        private static readonly List<IEdge> EXPECTED_EDGES = new List<IEdge>() {
            new Edge(VALID_LEFT_CHILD_NODE,VALID_ROOT_NODE),
            new Edge(VALID_RIGHT_CHILD_NODE,VALID_ROOT_NODE)
        };

        [Test]
        public void Should_Create_ExpressionTree_With_One_Node_Only()
        {
            IBinaryExpressionTree tree = new BinaryExpressionTree(VALID_ROOT_NODE);

            tree.CurrentNode.Should().Be(VALID_ROOT_NODE);
        }

        [Test]
        public void Should_Create_ExpressionTree_With_One_Node_And_Left_Child()
        {
            IBinaryExpressionTree tree = new BinaryExpressionTree(VALID_ROOT_NODE, VALID_LEFT_CHILD);

            tree.CurrentNode.Should().Be(VALID_ROOT_NODE);
            tree.LeftChild.Should().Be(VALID_LEFT_CHILD);
            tree.LeftChild.Parent.Should().Be(VALID_ROOT_NODE);
        }

        [Test]
        public void Should_Create_ExpressionTree_With_One_Node_Left_Child_And_Right_Child()
        {
            IBinaryExpressionTree tree = new BinaryExpressionTree(VALID_ROOT_NODE, VALID_LEFT_CHILD, VALID_RIGHT_CHILD);

            tree.CurrentNode.Should().Be(VALID_ROOT_NODE);
            tree.LeftChild.Should().Be(VALID_LEFT_CHILD);
            tree.LeftChild.Parent.Should().Be(VALID_ROOT_NODE);
            tree.RightChild.Should().Be(VALID_RIGHT_CHILD);
            tree.RightChild.Parent.Should().Be(VALID_ROOT_NODE);
        }

        [Test]
        public void GetNodes_Should_Return_List_Of_All_Tree_Nodes()
        {
            IBinaryExpressionTree tree = new BinaryExpressionTree(VALID_ROOT_NODE, VALID_LEFT_CHILD, VALID_RIGHT_CHILD);

            var nodes = tree.GetNodes();

            nodes.Should().ContainInOrder(EXPECTED_NODES);
        }

        [Test]
        public void GetEdges_Should_Return_List_Of_Relations_Between_Parent_And_Child()
        {
            IBinaryExpressionTree tree = new BinaryExpressionTree(VALID_ROOT_NODE, VALID_LEFT_CHILD, VALID_RIGHT_CHILD);

            var edges = tree.GetEdges();

            edges.Should().ContainInOrder(EXPECTED_EDGES);
        }

        [Test]
        public void GetLeafs_Should_Return_List_Of_All_The_Nodes_Without_Parents()
        {
            IBinaryExpressionTree tree = new BinaryExpressionTree(VALID_ROOT_NODE, VALID_LEFT_CHILD, VALID_RIGHT_CHILD);

            var leafs = tree.GetLeafs();

            leafs.Should().ContainInOrder(EXPECTED_LEAFS);
        }

        [Test]
        public void GetLeafsSortedA2z_Should_Return_Sorted_List_Of_All_The_Nodes_Without_Parents_From_A_to_z_()
        {
            IBinaryExpressionTree tree = new BinaryExpressionTree(VALID_ROOT_NODE, VALID_LEFT_CHILD, VALID_RIGHT_CHILD);

            var leafs = tree.GetLeafsSortedA2z();

            leafs.Should().ContainInOrder(EXPECTED_SORTED_LEAFS);
        }

        [Test]
        public void TraversePostOrdered_Should_Return_List_Of_All_The_Nodes_In_Post_Order()
        {
            IBinaryExpressionTree tree = new BinaryExpressionTree(VALID_ROOT_NODE, VALID_LEFT_CHILD, VALID_RIGHT_CHILD);

            var postOrdered = tree.TraversePostOrder();

            postOrdered.Should().ContainInOrder(EXPECTED_TRAVERSED_POSTORDERED);
        }
    }
}
