import { Routes } from "@angular/router";
import { BlogListComponent } from "./blog-list/blog-list.component";

export const BlogRoutes: Routes = [{
    path: '',
    component: BlogListComponent,
    pathMatch: 'full'
}]