import { SkillGroup } from "../skill-group";

export interface CreateSkillGroupCreatedEvent{
    skillGroup: SkillGroup,
    openNewSkillModal: boolean
}