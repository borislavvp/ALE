import { expressionService } from "../../api/expressionRepository";
import { ExpressionEvaluation } from "../../types/expression/ExpressionEvaluation";
import { TruthTable } from "../../types/expression/TruthTable";
import { computed, reactive, ref } from "vue";

interface Evaluation extends ExpressionEvaluation {
  TableToShow: { value: TruthTable; type: "original" | "simplified" | "" };
}
const evaluation = reactive<Evaluation>({
  TruthTable: {},
  SimplifiedTruthTable: { Values: {}, DontCareCharacter: "" },
  Nodes: [],
  Edges: [],
  Leafs: "",
  HexResult: "",
  DNF: "",
  SimplifiedDNF: "",
  InfixNotation: "",
  Nandify: "",
  TableToShow: { value: {}, type: "" }
});

export function expressionProvider() {
  const IS_EVALUATING = ref(false);
  const expression = ref("|(>(~(A),B)>(~(a),C))");
  const expressionEvaluated = ref("");

  const ShowSimplifiedTable = () => {
    evaluation.TableToShow.value = evaluation.SimplifiedTruthTable.Values;
    evaluation.TableToShow.type = "simplified";
  };
  const ShowOriginalTable = () => {
    evaluation.TableToShow.value = evaluation.TruthTable;
    evaluation.TableToShow.type = "original";
  };

  const evaluate = (): Promise<void> => {
    IS_EVALUATING.value = true;
    return new Promise(resolve => {
      expressionService
        .evaluateExpression(expression.value)
        .then(result => {
          expressionEvaluated.value = expression.value;
          evaluation.Nodes = result.Nodes;
          evaluation.Edges = result.Edges;
          evaluation.Leafs = result.Leafs;
          evaluation.HexResult = result.HexResult;
          evaluation.DNF = result.DNF;
          evaluation.SimplifiedDNF = result.SimplifiedDNF;
          evaluation.Nandify = result.Nandify;
          evaluation.InfixNotation = result.InfixNotation;
          evaluation.SimplifiedTruthTable = result.SimplifiedTruthTable;
          evaluation.TruthTable = result.TruthTable;
          evaluation.TableToShow = {
            value: result.TruthTable,
            type: "original"
          };
        })
        .catch(e => console.log(e))
        .finally(() => {
          setTimeout(() => {
            IS_EVALUATING.value = false;
            resolve();
          }, 1000);
        });
    });
  };

  const setExpression = (input: string) => {
    expression.value = input;
  };

  return {
    IS_EVALUATING: computed(() => IS_EVALUATING.value),
    expression: computed(() => expression.value),
    evaluation: computed(() => evaluation),
    evaluate,
    setExpression,
    ShouldEvaluate: computed(
      () => expression.value !== expressionEvaluated.value
    ),
    ShowSimplifiedTable,
    ShowOriginalTable,
    DontCareCharacter: computed(
      () => evaluation.SimplifiedTruthTable.DontCareCharacter
    )
  };
}
