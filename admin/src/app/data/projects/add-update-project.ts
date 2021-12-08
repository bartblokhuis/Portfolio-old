import { Skill } from "../skills/skill";

export interface AddUpdateProject {
    title: string,
    description: string,
    imagePath: string,
    displayNumber: number,
    isPublished: boolean,
    githubUrl? :string,
    demoUrl?: string,
    id?: number
}