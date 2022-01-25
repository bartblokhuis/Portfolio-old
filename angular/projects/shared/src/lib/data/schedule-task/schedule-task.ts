import { BaseScheduleTask } from "./base-schedule-task";

export interface ScheduleTask extends BaseScheduleTask {
    id: number;
    lastStartUtc: Date | null;
    lastEndUtc: Date | null;
    lastSuccessUtc: Date | null;
}