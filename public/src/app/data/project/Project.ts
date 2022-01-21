import { BaseEntity } from '../BaseEntity';
import { Skill } from '../Skill';
import { Url } from '../url';
import { ProjectPicture } from './project-picture';

export interface Project {
    title: string,
    description: string,
    displayNumber: number,
    isPublished: boolean,
    skills? : Skill[],
    urls: Url[] | null,
    pictures: ProjectPicture[],
    id: number
}