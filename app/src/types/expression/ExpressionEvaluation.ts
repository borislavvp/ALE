import { ExpressionEdge } from "./ExpressionEdge";
import { ExpressionNode } from "./ExpressionNode";
import { SimplifiedTruthTable } from "./SimplifiedTruthTable";
import { TruthTable } from "./TruthTable";

export interface ExpressionEvaluation {
  TruthTable: TruthTable;
  SimplifiedTruthTable: SimplifiedTruthTable;
  Nodes: ExpressionNode[];
  Edges: ExpressionEdge[];
  HexResult: string;
  Leafs: string;
  DNF: string;
  SimplifiedDNF: string;
  InfixNotation: string;
  Nandify: string;
}
