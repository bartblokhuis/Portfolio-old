import { BaseBlog } from "./base-blog";

export interface EditBlog extends BaseBlog {
    id: number;
    content: string
}