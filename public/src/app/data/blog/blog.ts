import { Picture } from "../common/picture";
import { BaseBlog } from "./base-blog";

export interface Blog extends BaseBlog {
    content: string | null;
    id: number;
    createdAtUtc: Date;
    updatedAtUtc: Date;
    metaTitle: string | null;
    metaDescription:  string | null;
    bannerPicture: Picture | null;
    thumbnail: Picture | null;
}