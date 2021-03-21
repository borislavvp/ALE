import { ExpressionEdge } from "./ExpressionEdge";
import { ExpressionNode } from "./ExpressionNode";
import { TruthTable } from "./TruthTable";

export interface ExpressionEvaluation {
    TruthTable: TruthTable;
    SimplifiedTruthTable: TruthTable;
    Nodes: ExpressionNode[];
    Edges :ExpressionEdge[];
    Leafs: String;
    InfixNotation:string;
}