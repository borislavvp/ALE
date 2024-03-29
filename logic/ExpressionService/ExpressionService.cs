﻿using logic.ExpressionService.Common.DTO;
using logic.ExpressionService.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService
{
    public static class ExpressionService
    {
        public static ExpressionStructureDto EvaluateExpression(string expressionValue)
        {
            ExpressionStructure structure = new ExpressionStructure(new PrefixExpression(expressionValue));
            structure.BuildExpressionTree();
            structure.BuildTruthTable();
            return new ExpressionStructureDto(structure);
        }
    }
}
