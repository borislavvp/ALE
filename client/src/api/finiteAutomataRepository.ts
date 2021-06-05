import { FiniteAutomataEvaluation } from "@/types/finiteautomata/FiniteAutomataEvaluation";
import { TestCasesEvaluation } from "@/types/finiteautomata/TestCasesEvaluation";
import axios from "axios";
const instance = axios.create({
  baseURL:
    process.env.NODE_ENV === "development"
      ? "http://localhost:44390"
      : "https://ale-server.azurewebsites.net/"
});
export const finiteAutomataService = {
  evaluateInstructions(instructions: string): Promise<FiniteAutomataEvaluation> {
    return new Promise(resolve => {
      instance
        .post('/api/finiteautomata/evaluate',{instructions})
        .then(result => {
          if (result.status === 200) {
            resolve(result.data);
          } else {
            resolve({} as FiniteAutomataEvaluation);
          }
        })
        .catch(err => {
          console.log(err);
          resolve({} as FiniteAutomataEvaluation);
        });
    });
  },
  evaluateTestCases(value: string): Promise<TestCasesEvaluation> {
    return new Promise(resolve => {
      instance
        .post('/api/finiteautomata/tests',{value})
        .then(result => {
          if (result.status === 200) {
            resolve(result.data);
          } else {
            resolve({} as TestCasesEvaluation);
          }
        })
        .catch(err => {
          console.log(err);
          resolve({} as TestCasesEvaluation);
        });
    });
  },
  checkWord(word: string): Promise<boolean> {
    return new Promise(resolve => {
      instance
        .post(`/api/finiteautomata/check?word=${word}`)
        .then(result => {
          if (result.status === 200) {
            resolve(result.data);
          } else {
            resolve(false);
          }
        })
        .catch(err => {
          console.log(err);
          resolve(false);
        });
    });
  }
};
