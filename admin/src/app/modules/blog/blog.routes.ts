import { Routes } from "@angular/router";
import { AddBlogComponent } from "./add-blog/add-blog.component";
import { ListBlogComponent } from "./list-blog/list-blog.component";

export const BlogRoutes: Routes = [{
    path: 'blog',
    component: ListBlogComponent
},
{
    path: 'blog/new',
    component: AddBlogComponent
}]