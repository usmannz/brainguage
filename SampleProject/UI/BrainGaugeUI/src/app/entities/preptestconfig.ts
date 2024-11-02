

export class PrepTestConfig {
    public constructor(init?: Partial<PrepTestConfig>) {
        Object.assign(this, init);
    }

    id?: number; // Make id optional
    name: string;
    timeBox: number;
    totalQuestions: number;
    // unAttemptQuestions: boolean;
    // wrongAnswers: boolean;
    // allQuestions: boolean;
    questionCriteria:string;
    resultEnd: boolean;
}



export class SavePrepTestConfig {
    public constructor(init?: Partial<SavePrepTestConfig>) {
        Object.assign(this, init);
    }

    id?: number; // Make id optional
    name: string;
    timeBox: number;
    totalQuestions: number;
    // unAttemptQuestions: boolean;
    // wrongAnswers: boolean;
    // allQuestions: boolean;
    questionCriteria:string;
    resultEnd: boolean;
    categories: any[];

}