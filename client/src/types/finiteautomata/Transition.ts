import { Letter } from "./Letter";
import { State } from "./State";

export interface Transition{
    From:State,
    To: State,
    Value: Letter
}