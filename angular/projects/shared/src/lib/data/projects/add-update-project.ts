export interface AddUpdateProject {
    title: string,
    description: string,
    displayNumber: number,
    isPublished: boolean,
    githubUrl? :string,
    demoUrl?: string,
    id?: number
}