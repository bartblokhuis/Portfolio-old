import { BaseEntity } from '.././BaseEntity';
import { MessageStatus } from './Message';

export interface UpdateMessage extends BaseEntity {
    messageStatus: MessageStatus
}