export class User {
    id: number;
    username: string;
    password: string;
    firstName: string;
    lastName: string;
    token?: string;
    expiration: string;
}

export class UserDetails {
    username: string;
    email: string;
}