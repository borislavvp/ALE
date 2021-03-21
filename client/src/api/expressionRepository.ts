import { ExpressionEvaluation } from "@/types/ExpressionEvaluation";
import axios from "axios"
const instance = axios.create({baseURL:"http://localhost:44390"})
export const expressionService = {
    evaluateExpression(expression: string): Promise<ExpressionEvaluation>{
      const param = encodeURIComponent(expression);
        return new Promise(resolve => {
            instance.post(`/api/evaluate?expression=${param}`)
            .then(result => {
                if (result.status === 200) {
                    console.log(result.data)
                    resolve(result.data)
                }else{
                    resolve({} as ExpressionEvaluation)
                }
            })
            .catch(err => {
                console.log(err);
                resolve({} as ExpressionEvaluation)
            })
        })
    }
}