import { Routes } from "@angular/router";
import { BlogListComponent } from "./blog-list/blog-list.component";
import { BlogPostComponent } from "./blog-post/blog-post.component";

export const BlogRoutes: Routes = [
    {
        path: 'blog',
        component: BlogListComponent,
        pathMatch: 'full'
    },
    {
        path: 'blog/:id',
        component: BlogPostComponent,
        pathMatch: 'full'
    },
]