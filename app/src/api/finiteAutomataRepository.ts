import { FiniteAutomataEvaluation } from "@/types/finiteautomata/FiniteAutomataEvaluation";
import { TestCasesEvaluation } from "@/types/finiteautomata/TestCasesEvaluation";
import axios from "axios";
const instance = axios.create({
  baseURL: import.meta.env.VITE_API_URL
});
export const finiteAutomataService = {
  evaluateInstructions(instructions: string): Promise<FiniteAutomataEvaluation> {
    return new Promise((resolve,reject) => {
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
          reject(err);
        });
    });
  },
  evaluateTestCases(value: string): Promise<TestCasesEvaluation> {
    return new Promise((resolve,reject) => {
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
          reject(err);
        });
    });
  },
  checkWord(word: string): Promise<boolean> {
    return new Promise((resolve,reject) => {
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
          reject(err);
        });
    });
  }
};
