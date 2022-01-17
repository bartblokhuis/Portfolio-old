import { Picture } from "../common/picture";
import { BaseBlog } from "./base-blog";

export interface EditBlog extends BaseBlog {
    id: number;
    content: string | null;
    metaTitle: string | null;
    metaDescription: string | null;
    bannerPicture: Picture | null;
    bannerPictureId: number | null;
    thumbnail: Picture | null;
    thumbnailId: number | null;
}