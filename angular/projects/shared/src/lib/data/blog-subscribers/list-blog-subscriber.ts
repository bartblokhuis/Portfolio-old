export interface ListBlogSubscriber {
    id: string;
    name: string;
    emailAddress: string;
    createdAtUTC: Date;
    updatedAtUtc: Date;
    isDeleted: boolean;
}