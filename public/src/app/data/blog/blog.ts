import { BaseBlog } from "./base-blog";

export interface Blog extends BaseBlog {
    content: string;
    id: number;
    createdAtUtc: Date;
    updatedAtUtc: Date
}