import { finiteAutomataService } from "@/api/finiteAutomataRepository"
import { FiniteAutomataEvaluation, GraphValues } from "@/types/finiteautomata/FiniteAutomataEvaluation"
import { TestCasesEvaluation } from "@/types/finiteautomata/TestCasesEvaluation"
import { computed, reactive } from "@vue/composition-api"
import { withAutomataGraph } from "./withAutomataGraph"
import { withFileManager } from "./withFileManager"


const blobServiceClient = new BlobServiceClient(`https://${process.env.VUE_APP_BLOB_ACC}.blob.core.windows.net${process.env.VUE_APP_BLOB_SAS}`);
const containerClient = blobServiceClient.getContainerClient(process.env.VUE_APP_BLOB_PROJECTS_CONTAINER!);



const evaluation: FiniteAutomataEvaluation = reactive({
    GraphVisible:"Original",
    Processing:false,
    Testing: false,
    DFAInstructionsID:"",
    DFA:{} as GraphValues,
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
        WordCheckerResults:[]
    } as TestCasesEvaluation
})
export const withFiniteAutomataProvider = () => {

    const evaluate = (instructions: string) => {
        evaluation.Processing = true;
        return new Promise<void>((resolve, reject) => {
            finiteAutomataService.evaluateInstructions(instructions)
                .then(res => {
                    console.log(res)
                    evaluation.Original.States = res.Original.States;
                    evaluation.Original.Transitions = res.Original.Transitions
                    evaluation.DFA.States = res.DFA.States;
                    evaluation.DFA.Transitions = res.DFA.Transitions
                    evaluation.DFAInstructionsID = res.DFAInstructionsID;
                    setTimeout(() => {
                        evaluation.Processing = false;
                        resolve();
                    }, 500)
                }).catch(err => {
                    evaluation.Processing = false;
                    reject();
                })
        })
    }

    const checkWord = (word: string) => {
        return new Promise<boolean>((resolve, reject) => {
            finiteAutomataService.checkWord(word)
                .then(res => {
                    console.log(res)
                    setTimeout(() => {
                        resolve(res);
                    }, 300)
                }).catch(err => {
                    reject();
                })
        })
    }
    const evaluateTests = (words: string) => {
        evaluation.Testing = true;
        return new Promise<void>((resolve, reject) => {
            finiteAutomataService.evaluateTestCases(words)
                .then(res => {
                    console.log(res)
                    evaluation.Tests.IsDFA = JSON.parse(JSON.stringify(res.IsDFA));
                    evaluation.Tests.IsFinite =  JSON.parse(JSON.stringify(res.IsFinite));
                    evaluation.Tests.WordCheckerResults = res.WordCheckerResults;
                    console.log(evaluation);
                    setTimeout(() => {
                        evaluation.Testing = false;
                        resolve();
                    }, 500)
                }).catch(err => {
                    evaluation.Testing = false;
                    reject();
                })
        })
    }

    const showDFA = () => {
        console.log(evaluation.DFAInstructionsID)
        evaluation.GraphVisible = "DFA";
        withAutomataGraph().showGraph(evaluation.DFA);
    }

    const showOriginal = () => {
        evaluation.GraphVisible = "Original";
        withAutomataGraph().showGraph(evaluation.Original);
    }
   
    return {
        evaluation: computed(() => evaluation),
        showDFA,
        showOriginal,
        evaluate,
        evaluateTests,
        checkWord
    }
}