import { BaseBlog } from "./base-blog";

export interface CreateBlog extends BaseBlog {
    content: string | null;
    metaTitle: string | null;
    metaDescription: string | null;
}