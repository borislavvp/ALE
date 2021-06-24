import { finiteAutomataService } from "@/api/finiteAutomataRepository"
import { FiniteAutomataEvaluation, GraphValues } from "@/types/finiteautomata/FiniteAutomataEvaluation"
import { TestCasesEvaluation } from "@/types/finiteautomata/TestCasesEvaluation"
import { computed, reactive } from "@vue/composition-api"
import { predefinedInstructions } from "./utils/predefinedInstructions"
import { withAutomataGraph } from "./withAutomataGraph"

const evaluation: FiniteAutomataEvaluation = reactive({
    PredefinedInstructions: predefinedInstructions,
    CurrentInstructionName:predefinedInstructions[0].title,
    GraphVisible:"Original",
    RegexMode:false,
    Processing:false,
    Testing: false,
    DFAInstructions:"",
    DFA: {} as GraphValues,
    CurrentInstructions:"",
    OriginalInstructions: "",
    Original:{} as GraphValues,
    Tests: {
        IsDFA: {
            TestGuess: false,
            Answer:false
        },
        IsFinite: {
            TestGuess: false,
            Answer:false
        },
        WordCheckerResults: [],
        AllPossibleWords:[],
    } as TestCasesEvaluation
})
export const withFiniteAutomataProvider = () => {

    const turnOnRegexMode = () => {
        evaluation.RegexMode = true;
    }

    const turnOffRegexMode = () => {
        evaluation.RegexMode = false;
    }

    const setInstrucitonsName = (instructionsName:string) => {
        evaluation.CurrentInstructionName = instructionsName;
    }

    const evaluate = (instructions: string) => {
        evaluation.Processing = true;
        return new Promise<void>((resolve, reject) => {
            finiteAutomataService.evaluateInstructions(instructions)
                .then(res => {
                    evaluation.Original.States = res.Original.States;
                    evaluation.Original.Transitions = res.Original.Transitions
                    evaluation.DFA.States = res.DFA?.States || [];
                    evaluation.DFA.Transitions = res.DFA?.Transitions || []
                    evaluation.DFAInstructions = res.DFAInstructions;
                    if (evaluation.RegexMode) {
                        evaluation.OriginalInstructions = res.OriginalInstructions;
                        evaluation.CurrentInstructions = res.OriginalInstructions;
                    }
                    setTimeout(() => {
                        evaluation.Processing = false;
                        resolve();
                    }, 500)
                }).catch(err => {
                    evaluation.Processing = false;
                    reject(err);
                })
        })
    }

    const checkWord = (word: string) => {
        return new Promise<boolean>((resolve, reject) => {
            finiteAutomataService.checkWord(word)
                .then(res => {
                    setTimeout(() => {
                        resolve(res);
                    }, 300)
                }).catch(err => {
                    reject();
                })
        })
    }
    const evaluateTests = (tests: string) => {
        console.log(tests)
        evaluation.Testing = true;
        return new Promise<void>((resolve, reject) => {
            finiteAutomataService.evaluateTestCases(tests)
                .then(res => {
                    evaluation.Tests.IsDFA = JSON.parse(JSON.stringify(res.IsDFA));
                    evaluation.Tests.IsFinite =  JSON.parse(JSON.stringify(res.IsFinite));
                    evaluation.Tests.WordCheckerResults = res.WordCheckerResults;
                    evaluation.Tests.AllPossibleWords = res.AllPossibleWords;
                    setTimeout(() => {
                        evaluation.Testing = false;
                        resolve();
                    }, 500)
                }).catch(() => {
                    evaluation.Testing = false;
                    reject();
                })
        })
    }

    const showDFA = () => {
        evaluation.GraphVisible = "DFA";
        evaluation.CurrentInstructions = evaluation.DFAInstructions;
        withAutomataGraph().showGraph(evaluation.DFA);
    }

    const showOriginal = () => {
        evaluation.GraphVisible = "Original";
        evaluation.CurrentInstructions = evaluation.OriginalInstructions;
        withAutomataGraph().showGraph(evaluation.Original);
    }

    const changeInstructions = (instructions: string) => {
        evaluation.CurrentInstructions = instructions;
        if (evaluation.GraphVisible === "DFA") {
            evaluation.GraphVisible = "Original"
        }
        evaluation.OriginalInstructions = instructions;
    }
   
    return {
        evaluation: computed(() => evaluation),
        canEvaluateInstructions: computed(() => evaluation.GraphVisible !== "DFA"),
        showDFA,
        showOriginal,
        evaluate,
        evaluateTests,
        checkWord,
        changeInstructions,
        setInstrucitonsName,
        turnOnRegexMode,
        turnOffRegexMode
    }
}