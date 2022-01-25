export interface BaseScheduleTask {
    name: string;
    seconds: number;
    type: string;
    enabled: boolean;
    stopOnError: boolean;
}