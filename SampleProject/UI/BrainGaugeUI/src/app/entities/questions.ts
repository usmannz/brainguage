

export class Questions {
    public constructor(init?: Partial<Questions>) {
        Object.assign(this, init);
    }

    id?: number; // Make id optional
    question: string;
    description:string;
    option1: string;
    option2: string;
    option3: string;
    option4: string;
    option5: string;
    isMockExam: boolean;
    isDemo: boolean;
    categoriesId: number;
    pictureWebPath: any;
    file:any;
    correctAnswer:number;
}



export class QuestionsAssignment {
    public constructor(init?: Partial<QuestionsAssignment>) {
        Object.assign(this, init);
    }

    id?: number; // Make id optional
    usersId: number;
    questionsId: number;
}

