
export const availablePageSizes: Array<string> = new Array<string>("10", "25", "50", "100")

export const baseDataTableOptions: object = {
    responsive: false,
    autoWidth: false,
    searching: false,
    orderMulti: false,
    ordering: false,
    serverSide: true,
    processing: true,
    lengthMenu: availablePageSizes
};