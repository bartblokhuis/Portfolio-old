import { Picture } from "../common/picture";
import { BaseBlog } from "./base-blog";
import { BlogComment } from "./comment";

export interface BlogPost extends BaseBlog {
    content: string | null;
    id: number;
    createdAtUtc: Date;
    updatedAtUtc: Date;
    metaTitle: string | null;
    metaDescription:  string | null;
    bannerPicture: Picture | null;
    thumbnail: Picture | null;
    comments: BlogComment[] | null;
}