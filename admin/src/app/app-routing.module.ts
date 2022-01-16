import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers/AuthGuard';
import { AdminComponent } from './layouts/admin/admin.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'about-me',
    pathMatch: 'full'
  },
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./modules/about-me/about-me.module').then(
          module => module.AboutMeModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./modules/messages/messages.module').then(
          module => module.MessagesModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./modules/projects/projects.module').then(
          module => module.ProjectsModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./modules/skill-groups/skill-groups.module').then(
          module => module.SkillGroupsModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./modules/settings/settings.module').then(
          module => module.SettingsModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./modules/blog-posts/blog-posts.module').then(
          module => module.BlogPostsModule
        ),
      },
      {
        path: '',
        loadChildren: () => import('./modules/pictures/pictures.module').then(
          module => module.PicturesModule
        ),
      }
    ],
    canActivate: [AuthGuard]
  },
  {
    path: '',
    loadChildren: () => import('./modules/user/user.module').then(
      module => module.UserModule
    )
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
