import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
    path: '',
    pathMatch: 'full',
    children: [
      { 
        path: '', 
        loadChildren: () => import('./modules/landing/landing.module').then(
          module => module.LandingModule
        )
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
  ]
})
export class AppRoutingModule { }
