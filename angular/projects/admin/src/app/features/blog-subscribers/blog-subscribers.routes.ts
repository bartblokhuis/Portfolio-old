import { Routes } from "@angular/router";
import { ListBlogSubscriberComponent } from "./list-blog-subscriber/list-blog-subscriber.component";

export const BlogSubscriberRoutes: Routes = [{
    path: 'blog/subscribers',
    component: ListBlogSubscriberComponent
}]