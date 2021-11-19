import { BaseEntity } from '../BaseEntity';

export enum MessageStatus {
    Unread,
    Read,
    WaitingResponse,
    Closed
  }
  
export interface Message extends BaseEntity {
    firstName: string,
    lastName: string,
    email: string,
    messageContent: string,
    createdAtUTC: Date,
    updatedAtUtc?: Date,
    messageStatus: MessageStatus
}

export const MessageStatusToLabelMapping = {
    [MessageStatus.Unread]: "Unread",
    [MessageStatus.Read]: "Read",
    [MessageStatus.WaitingResponse]: "Awaiting response",
    [MessageStatus.Closed]: "Closed",
  }