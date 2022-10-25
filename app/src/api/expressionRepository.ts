import { ExpressionEvaluation } from "@/types/expression/ExpressionEvaluation";
import axios from "axios";
const instance = axios.create({
  baseURL: import.meta.env.VITE_API_URL
});
export const expressionService = {
  evaluateExpression(expression: string): Promise<ExpressionEvaluation> {
    const param = encodeURIComponent(expression);
    return new Promise(resolve => {
      instance
        .post(`/api/expression/evaluate?expression=${param}`)
        .then(result => {
          if (result.status === 200) {
            resolve(result.data);
          } else {
            resolve({} as ExpressionEvaluation);
          }
        })
        .catch(err => {
          console.log(err);
          resolve({} as ExpressionEvaluation);
        });
    });
  }
};
