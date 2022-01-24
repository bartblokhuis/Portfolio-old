export interface UpdateQueuedEmail {
    id: number;
    from: string;
    fromName: string;
    to: string;
    toName: string;
    subject: string;
    sentTries: number;
    body: string
}