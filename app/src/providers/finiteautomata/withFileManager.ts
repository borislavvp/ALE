import { saveAs } from 'file-saver';
import { withFiniteAutomataProvider } from './withFiniteAutomataProvider';
export function withFileManager() {
    const readFile = (file: File) => {
        return new Promise<string>((resolve, reject) => {
                const fileReader = new FileReader();
                fileReader.onload = function (event) {
                    if (event.target?.result) {
                        withFiniteAutomataProvider().setInstrucitonsName(file.name.substr(0,file.name.length-4))
                        resolve(`${event.target.result}`);
                    }
                };
                fileReader.onerror = function (event) {
                    reject();
                };
                fileReader.readAsText(file,"UTF-8");
            })
    }
  
    function downloadFile(text: string) {
        const file = new File([text], "instructions.txt", {type: "text/plain;charset=utf-8"});
        saveAs(file);
    }
    
    return {
        downloadFile,
        readFile
    }
}