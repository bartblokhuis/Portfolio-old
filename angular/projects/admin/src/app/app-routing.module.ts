import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers/AuthGuard';
import { AdminComponent } from './layouts/admin/admin.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./features/dashboard/dashboard.module').then(
          module => module.DashboardModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/about-me/about-me.module').then(
          module => module.AboutMeModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/messages/messages.module').then(
          module => module.MessagesModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/projects/projects.module').then(
          module => module.ProjectsModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/skill-groups/skill-groups.module').then(
          module => module.SkillGroupsModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/settings/settings.module').then(
          module => module.SettingsModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/blog-posts/blog-posts.module').then(
          module => module.BlogPostsModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/blog-subscribers/blog-subscribers.module').then(
          module => module.BlogSubscribersModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/pictures/pictures.module').then(
          module => module.PicturesModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./features/email-queue/email-queue.module').then(
          module => module.EmailQueueModule
        ),
      }
    ],
    canActivate: [AuthGuard]
  },
  {
    path: '',
    loadChildren: () => import('./features/user/user.module').then(
      module => module.UserModule
    )
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
