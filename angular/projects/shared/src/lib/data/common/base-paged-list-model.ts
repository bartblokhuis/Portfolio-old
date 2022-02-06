export interface BasePagedListModel<T> {
    data: T[];
    draw: string;
    recordsFiltered: number;
    recordsTotal: number;
}