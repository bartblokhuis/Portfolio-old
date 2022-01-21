export interface CreateBlogComent {
    name: string;
    email: string | null;
    content: string;
    blogPostId: number | null;
    parentCommentId: number | null;
}
