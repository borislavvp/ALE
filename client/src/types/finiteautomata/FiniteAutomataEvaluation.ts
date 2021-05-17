import { State } from "./State";
import { Transition } from "./Transition";

export interface FiniteAutomataEvaluation{
    States: State[],
    Transitionts:Transition[]
}