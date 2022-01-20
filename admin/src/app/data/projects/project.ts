import { Skill } from "../skills/skill";
import { Url } from "../url";
import { ProjectPicture } from "./project-picture";

export interface Project {
    title: string,
    description: string,
    displayNumber: number,
    isPublished: boolean,
    skills? : Skill[],
    urls: Url[] | null,
    pictures: ProjectPicture[] | null,
    id: number
}