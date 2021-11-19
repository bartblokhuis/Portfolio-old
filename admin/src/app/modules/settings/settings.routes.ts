import { Routes } from '@angular/router';

import { EmailSettingsComponent } from './email-settings/email-settings.component';
import { AuthGuard } from '../../helpers/AuthGuard';
import { SeoSettingsComponent } from './seo-settings/seo-settings.component';
import { GeneralSettingsComponent } from './general-settings/general-settings.component';

export const SettingsRoutes: Routes = [{
    path: '',
    canActivate: [AuthGuard],
    children: [{
        path: 'email-settings',
        component: EmailSettingsComponent
    }, {
        path: 'seo-settings',
        component: SeoSettingsComponent
    }, {
        path: 'general-settings',
        component: GeneralSettingsComponent
    }]
}];
