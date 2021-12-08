import { Skill } from "../skills/skill";

export interface Project {
    title: string,
    description: string,
    imagePath: string,
    displayNumber: number,
    isPublished: boolean,
    skills? : Skill[],
    githubUrl? :string,
    demoUrl?: string,
    id: number
}