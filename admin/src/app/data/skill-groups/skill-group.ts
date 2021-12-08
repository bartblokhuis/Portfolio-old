import { Skill } from "../skills/skill";

export interface SkillGroup{
    title: string,
    displayNumber: number,
    id: number,
    skills: Skill[]
}

export class ListSkillGroup implements SkillGroup {
    title: string;
    displayNumber: number;
    id: number;
    skills: Skill[];
    
    inEditMode: boolean = false;

    constructor(skillGroup: SkillGroup) {
        this.title = skillGroup.title;
        this.displayNumber = skillGroup.displayNumber;
        this.id = skillGroup.id;
        this.skills = skillGroup.skills;
    }
}