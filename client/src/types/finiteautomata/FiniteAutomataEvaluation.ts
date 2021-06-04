import { State } from "./State";
import { TestCasesEvaluation } from "./TestCasesEvaluation";
import { Transition } from "./Transition";

export interface GraphValues {
  States: State[];
  Transitions: Transition[];
}

export type GraphType = "DFA" | "Original";

export interface FiniteAutomataEvaluation {
  GraphVisible: GraphType
  DFAInstructionsID: string;
  DFA: GraphValues;
  Original: GraphValues;
  Processing: boolean;
  Testing: boolean;
  Tests: TestCasesEvaluation;
}
