

export class User {
    public constructor(init?: Partial<User>) {
        Object.assign(this, init);
    }

    id: number;
    firstName: string;
    lastName: string;
    fullName: string;
    email: string;
    password: string;
    statusId: number;
    address:string;
    contactPhoneNumber: string;
    pictureUrl: string;
    pictureWebPath: string;
    guid: string;
    position:string;

}

export class ViewAddUser {
    public constructor(init?: Partial<User>) {
        Object.assign(this, init);
    }

    id: number;
    firstName: string;
    lastName: string;
    fullName: string;
    email: string;
    password: string;
    statusId: number;
}

export class TenantUser {
    public constructor(init?: Partial<TenantUser>) {
        Object.assign(this, init);
    }

    id: number;
    firstName: string;
    lastName: string;
    fullName: string;
}