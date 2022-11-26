import { State } from "./State";
import { TestCasesEvaluation } from "./TestCasesEvaluation";
import { Transition } from "./Transition";

export interface GraphValues {
  States: State[];
  Transitions: Transition[];
}

export type GraphType = "DFA" | "Original";

export interface FiniteAutomataEvaluation {
  Valid:boolean,
  PredefinedInstructions: { title: string, src: () => Promise<any> }[],
  CurrentInstructionName:string,
  GraphVisible: GraphType
  DFAInstructions: string;
  CurrentInstructions: string;
  OriginalInstructions: string;
  DFA: GraphValues;
  Original: GraphValues;
  RegexMode: boolean;
  Processing: boolean;
  Testing: boolean;
  Tests: TestCasesEvaluation;
}
