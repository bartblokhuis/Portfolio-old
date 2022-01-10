import { Picture } from "../common/picture";
import { BaseBlog } from "./base-blog";

export interface ListBlog extends BaseBlog {
    id: number;
    createdAtUTC: Date;
    updatedAtUtc: Date;
    thumbnail: Picture | null;
}