import { Routes } from '@angular/router';

import { AboutMeComponent } from './about-me/about-me.component';
import { AuthGuard } from '../../helpers/AuthGuard';

export const AboutMeRoutes: Routes = [{
    path: '',
    canActivate: [AuthGuard],
    children: [{
        path: 'about-me',
        component: AboutMeComponent
    }]
}];
