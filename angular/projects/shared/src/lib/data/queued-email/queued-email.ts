export interface QueuedEmail {
    id: number,
    from: string;
    fromName: string;
    to: string;
    toName: string;
    subject: string;
    createdAtUTC: Date;
    sentTries: number;
    sentOnUtc: Date | null;
}