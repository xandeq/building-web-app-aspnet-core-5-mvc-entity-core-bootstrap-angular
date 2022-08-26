export class LoginResults {
    constructor() { }
    token: string = '';
    expiration: Date = new Date();
}

export class LoginRequest {
    constructor() { }
    username: string = '';
    password: string = '';
}