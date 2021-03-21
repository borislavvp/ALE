using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.DTO
{
    public class ExpressionStructureDto
    {
        public TruthTableValues TruthTable { get; set; }
        public TruthTableValues SimplifiedTruthTable { get; set; }
        public string HexResult { get; set; }
        public List<INode> Nodes { get; set; }
        public List<IEdge> Edges { get; set; }
        public string Leafs { get; set; }
        public string InfixNotation { get; set; }

        public ExpressionStructureDto(ExpressionStructure structure)
        {
            this.TruthTable = structure.TruthTable.Value;
            this.SimplifiedTruthTable = structure.TruthTable.Simplify();
            this.HexResult = structure.TruthTable.HexResult;
            this.Nodes = structure.ExpressionTree.GetNodes();
            this.Edges = structure.ExpressionTree.GetEdges();
            this.Leafs = System.String.Join("", structure.ExpressionTree.GetLeafs());
            this.InfixNotation = structure.ToString();
        }
    }
}
