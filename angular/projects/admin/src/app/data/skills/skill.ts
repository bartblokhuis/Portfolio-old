import { SkillGroup } from "../skill-groups/skill-group";

export interface Skill {
    name: string,
    iconPath: string,
    displayNumber: number,
    skillGroupId: number,
    skillGroup: SkillGroup,
    id: number
}