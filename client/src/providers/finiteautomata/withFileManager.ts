import { finiteAutomataService } from "@/api/finiteAutomataRepository";
import { readInstructions } from "./utils/readInstructions";

export function withFileManager() {
    const readFile = (file: File) => {
        return new Promise<string>((resolve, reject) => {
                const fileReader = new FileReader();
            
                fileReader.onload = function (event) {
                    if (event.target?.result) {
                        const lines = `${event.target.result}`.split('\n');
                        console.log(lines)
                        finiteAutomataService.evaluateInstructions(`${event.target.result}`);
                        // readInstructions(lines);
                        resolve(`${event.target.result}`);
                    }
                };
                fileReader.onerror = function (event) {
                    reject();
                };
    
                fileReader.readAsText(file);
            })
    }
    return {
        readFile
    }
}