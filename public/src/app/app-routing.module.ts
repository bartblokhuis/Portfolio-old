import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
    {
      path: '',
      component: AppComponent,
      children: [
        {
          path: '',
          loadChildren: () => import('./features/landing/landing.module').then(
            module => module.LandingModule
          )
        },
        {
          path: 'blog',
          loadChildren: () => import('./features/blog/blog.module').then(
            module => module.BlogModule
          )
        }
      ]
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation: 'enabledBlocking'
})],
  exports: [RouterModule],
  providers: [
  ]
})
export class AppRoutingModule { }
