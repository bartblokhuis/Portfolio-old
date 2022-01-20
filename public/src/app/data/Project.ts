import { BaseEntity } from './BaseEntity';
import { Skill } from './Skill';
import { Url } from './url';

export interface Project extends BaseEntity {
    title: string,
    description: string,
    imagePath: string,
    displayNumber: number,
    isPublished: boolean,
    skills? : Skill[],
    urls: Url[] | null
}
