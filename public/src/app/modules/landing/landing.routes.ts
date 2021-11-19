import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';

export const LandingRoutes: Routes = [{
    path: '',
    children: [{
        path: '',
        component: LandingComponent
    }]
}];
