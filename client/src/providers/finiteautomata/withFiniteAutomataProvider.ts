import { finiteAutomataService } from "@/api/finiteAutomataRepository"
import { FiniteAutomataEvaluation } from "@/types/finiteautomata/FiniteAutomataEvaluation"
import { State } from "@/types/finiteautomata/State"
import { computed, reactive } from "@vue/composition-api"
import { withAutomataGraph } from "./withAutomataGraph"
import { withFileManager } from "./withFileManager"

const evaluation: FiniteAutomataEvaluation = reactive({
    States: [],
    Transitions: []
})
export const withFiniteAutomataProvider = () => {

    const evaluate = (instructions: string) => {
        return new Promise<void>((resolve, reject) => {
            finiteAutomataService.evaluateInstructions(instructions)
                .then(res => {
                    console.log(res)
                    evaluation.States = res.States;
                    evaluation.Transitions = res.Transitions
                    resolve();
                }).catch(err => reject())
        })
    }

    const checkWords = (words: string) => {
        return new Promise<void>((resolve, reject) => {
            finiteAutomataService.evaluateWords(words)
                .then(res => {
                    console.log(res)
                    resolve();
                }).catch(err => reject())
        })
    }
   
    return {
        evaluation: computed(() => evaluation),
        evaluate,
        checkWords
    }
}