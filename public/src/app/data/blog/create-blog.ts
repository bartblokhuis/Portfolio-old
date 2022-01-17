import { BaseBlog } from "./base-blog";

export interface CreateBlog extends BaseBlog {
    content: string;
}