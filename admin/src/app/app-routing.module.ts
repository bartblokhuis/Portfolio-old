import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './helpers/AuthGuard';
import { AdminComponent } from './layouts/admin/admin.component';
import { AuthComponent } from './layouts/auth/auth.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
},
  {
    path: '',
    component: AdminComponent,
    canActivate: [AuthGuard],
    children: [
      { 
        path: '', 
        loadChildren: './modules/dashboard/dashboard.module#DashboardModule',
      }, 
      {
        path: '',
        loadChildren: './modules/about-me/about-me.module#AboutMeModule',
      }, 
      {
        path: '',
        loadChildren: './modules/skill-groups/skill-groups.module#SkillGroupsModule'
      }, 
      {
        path: '',
        loadChildren: './modules/projects/projects.module#ProjectsModule' 
      }, 
      {
        path: '',
        loadChildren: './modules/messages/messages.module#MessagesModule' 
      }, 
      {
        path: '',
        loadChildren: './modules/settings/settings.module#SettingsModule' 
      }
    ]
  },
  {
    path: '',
    component: AuthComponent,
    children: [{
      path: '',
      loadChildren: './modules/user/user.module#UserModule'
    }]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
