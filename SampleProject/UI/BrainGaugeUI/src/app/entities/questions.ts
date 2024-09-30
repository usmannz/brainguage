

export class Questions {
    public constructor(init?: Partial<Questions>) {
        Object.assign(this, init);
    }

    id?: number; // Make id optional
    question: string;
    answer: string;
}

