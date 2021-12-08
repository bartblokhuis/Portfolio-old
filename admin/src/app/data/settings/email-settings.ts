export interface EmailSettings {
    email: string;
    displayName: string;
    host: string;
    port: number;
    username: string;
    password: string;
    enableSsl: boolean;
    useDefaultCredentials: boolean;
    sendTestEmailTo: string;
}