import { Skill } from "../skills/skill";
import { Url } from "../url";

export interface Project {
    title: string,
    description: string,
    imagePath: string,
    displayNumber: number,
    isPublished: boolean,
    skills? : Skill[],
    urls: Url[] | null,
    id: number
}