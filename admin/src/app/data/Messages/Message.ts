import { MessageStatus } from "./message-status";

export interface Message {
    firstName: string,
    lastName: string,
    email: string,
    messageContent: string,
    createdAtUTC: Date,
    updatedAtUtc?: Date,
    messageStatus: MessageStatus
    id: number
}

