import { SkillGroup } from './SkillGroup';

export interface Skill {
    name: string,
    iconPath: string,
    displayNumber: number,
    skillGroupId: number,
    skillGroup: SkillGroup,
    id: number
}