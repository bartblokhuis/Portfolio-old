import { SkillGroup } from './SkillGroup';

export enum SkillType {
    FrontEnd,
    BackEnd,
    Other
}
   
export interface Skill {
    name: string,
    iconPath: string,
    displayNumber: number,
    skillGroupId: number,
    skillGroup: SkillGroup,
    id: number
}

export interface CreateUpdateSkill {
    name: string,
    iconPath: string,
    displayNumber: number,
    skillGroupId: number,
    id: number
}
   