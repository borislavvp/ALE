import { FiniteAutomataEvaluation } from "@/types/finiteautomata/FiniteAutomataEvaluation";
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
  evaluateWords(words: string): Promise<FiniteAutomataEvaluation> {
    return new Promise(resolve => {
      instance
        .post('/api/finiteautomata/words',{words})
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
  }
};
