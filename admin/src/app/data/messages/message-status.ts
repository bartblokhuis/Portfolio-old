
export enum MessageStatus {
    Unread,
    Read,
    WaitingResponse,
    Closed
}

export const MessageStatusToLabelMapping = {
    [MessageStatus.Unread]: "Unread",
    [MessageStatus.Read]: "Read",
    [MessageStatus.WaitingResponse]: "Awaiting response",
    [MessageStatus.Closed]: "Closed",
  }