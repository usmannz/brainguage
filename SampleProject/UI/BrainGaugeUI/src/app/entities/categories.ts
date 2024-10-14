

export class Categories {
    public constructor(init?: Partial<Categories>) {
        Object.assign(this, init);
    }

    id?: number; // Make id optional
    name: string;
}

