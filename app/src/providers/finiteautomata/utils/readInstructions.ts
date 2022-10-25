import { Instruction } from "@/types/finiteautomata/Instruction"

export const readInstructions = (instructions: string[]) => {
    instructions.forEach(instruction => {
        const splitted = instruction.split(":");
        console.log(splitted)
        const type = splitted[0];
        const value = splitted[1].trim();
        switch (type) {
            case Instruction.Alphabet:
                console.log(value);
                break;
            default:
                console.log("default")
        }
    });
}