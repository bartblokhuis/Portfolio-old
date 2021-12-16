import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SEOResolver } from './resolvers/SEOResolver';

const routes: Routes = [
    {
    path: '',
    pathMatch: 'full',
    component: AppComponent
  },
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
