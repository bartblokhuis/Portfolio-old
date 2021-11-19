import { BaseEntity } from './BaseEntity';
import { Skill } from './Skill';

export interface Project extends BaseEntity {
    title: string,
    description: string,
    imagePath: string,
    displayNumber: number,
    isPublished: boolean,
    skills? : Skill[],
    githubUrl? :string,
    demoUrl?: string,
}

export interface UpdateProjectSkills {
    projectId: number,
    skillIds: number[]
}