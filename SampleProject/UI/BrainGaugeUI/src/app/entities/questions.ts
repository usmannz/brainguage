

export class Questions {
    public constructor(init?: Partial<Questions>) {
        Object.assign(this, init);
    }

    id?: number; // Make id optional
    question: string;
    answer: string;
}



export class QuestionsAssignment {
    public constructor(init?: Partial<QuestionsAssignment>) {
        Object.assign(this, init);
    }

    id?: number; // Make id optional
    usersId: number;
    questionsId: number;
}

