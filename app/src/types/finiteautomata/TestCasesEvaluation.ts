export interface TestCasesEvaluation {
    IsDFA: {
        TestGuess:boolean,
        Answer:boolean
    },
    IsFinite: {
        TestGuess:boolean,
        Answer:boolean
    },
    WordCheckerResults: {
        Test:{
            TestGuess:boolean,
            Answer: boolean,
        }
        Word:string
    }[],
    AllPossibleWords:string[]
}