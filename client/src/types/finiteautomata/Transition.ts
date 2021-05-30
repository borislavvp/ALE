import { Letter } from "./Letter";
import { State } from "./State";

export interface Transition {
  Id: number;
  From: State;
  To: State;
  Value: Letter;
}
