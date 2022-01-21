import { Routes } from "@angular/router";
import { AddBlogPostComponent } from "./add-blog-post/add-blog-post.component";
import { EditBlogPostComponent } from "./edit-blog-post/edit-blog-post.component";
import { ListBlogPostComponent } from "./list-blog-post/list-blog-post.component";

export const BlogPostsRoutes: Routes = [{
    path: 'blog',
    component: ListBlogPostComponent
},
{
    path: 'blog/new',
    component: AddBlogPostComponent
},
{
    path: 'blog/edit/:id',
    component: EditBlogPostComponent
}]