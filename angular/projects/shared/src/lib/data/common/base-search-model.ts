export interface BaseSearchModel {
    page: number;
    pageSize: number;
    availablePageSizes: string;
    draw: string;
    start: number;
    length: number;
}