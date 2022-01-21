import { Skill } from './Skill';

export interface SkillGroup{
    title: string,
    displayNumber: number,
    id: number,
    skills: Skill[]
}