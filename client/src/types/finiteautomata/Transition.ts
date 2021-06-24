import { DirectionValue } from "./DirectionValue";
import { State } from "./State";

export interface Transition {
  Id: number;
  From: State;
  To: State;
  Value: DirectionValue;
}
