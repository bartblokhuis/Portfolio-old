import { BaseBlog } from "./base-blog";

export interface ListBlog extends BaseBlog {
    id: number;
    createdAtUTC: Date;
    updatedAtUtc: Date;
}