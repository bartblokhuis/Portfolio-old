export interface BlogComment {
    id: number,
    name: string,
    content: string,
    isAuthor: boolean,
    comments: BlogComment[],
};