﻿using logic.ExpressionService.Common.Interfaces;
using logic.ExpressionService.Common.Models;
using logic.ExpressionService.Common.QMC;
using logic.ExpressionService.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.DTO
{
    public sealed class SimplifiedTruthTableValues
    {
        public TruthTableValues Values { get; set; }
        public char DontCareCharacter { get; set; }
    }
    public sealed class ExpressionStructureDto
    {
        public TruthTableValues TruthTable { get; set; }
        public SimplifiedTruthTableValues SimplifiedTruthTable { get; set; }
        public string HexResult { get; set; }
        public List<INode> Nodes { get; set; }
        public List<IEdge> Edges { get; set; }
        public string Leafs { get; set; }
        public string DNF { get; set; }
        public string SimplifiedDNF { get; set; }
        public string InfixNotation { get; set; }
        public string Nandify { get; set; }

        public ExpressionStructureDto(ExpressionStructure structure)
        {
            this.TruthTable = structure.TruthTable.Value;
            this.SimplifiedTruthTable = new SimplifiedTruthTableValues
            {
                Values = structure.TruthTable.Simplify(),
                DontCareCharacter = QuineMcCluskey.DONT_CARE
            };
            this.HexResult = structure.TruthTable.HexResult;
            this.Nodes = structure.ExpressionTree.GetNodes();
            this.Edges = structure.ExpressionTree.GetEdges();
            this.Leafs = System.String.Join(",", structure.ExpressionTree.GetLeafs());
            this.DNF = structure.TruthTable.NormalizeOriginal().Value;
            this.SimplifiedDNF = structure.TruthTable.NormalizeSimplified().Value;
            this.InfixNotation = structure.ToString();
            this.Nandify = structure.PrefixExpression.NandifyExpression();
        }
    }
}
