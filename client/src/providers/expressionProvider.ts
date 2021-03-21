import { expressionService } from '@/api/expressionRepository';
import { ExpressionEvaluation } from '@/types/ExpressionEvaluation';
import { computed, reactive, ref } from '@vue/composition-api';

export default function() {
	const IS_EVALUATING = ref(false);
	const expression = ref('|(>(~(A),B)>(~(A),B))');
	const evaluation = reactive({ TruthTable: {}, SimplifiedTruthTable: {}, Nodes: [], Edges: [], Leafs: "", InfixNotation: '' } as ExpressionEvaluation);

	const evaluate = (): Promise<void> => {
		IS_EVALUATING.value = true;
		return new Promise((resolve) => {
			expressionService
				.evaluateExpression(expression.value)
                .then((result) => {
					evaluation.Nodes = result.Nodes;
					evaluation.Edges = result.Edges;
					evaluation.Leafs = result.Leafs;
					evaluation.InfixNotation = result.InfixNotation;
					evaluation.SimplifiedTruthTable = result.SimplifiedTruthTable;
					evaluation.TruthTable = result.TruthTable;
				})
				.catch((e) => console.log(e))
				.finally(() => {
					setTimeout(() => {
						IS_EVALUATING.value = false;
						resolve();
					},1000)
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
	};
}
