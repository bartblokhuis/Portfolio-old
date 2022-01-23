export interface BreadcrumbItem {
    name: string,
    active: boolean,
    url?: string | null,
    routePath?: string | null
}