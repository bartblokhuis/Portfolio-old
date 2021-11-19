import { Routes } from '@angular/router';

import { ListSkillGroupComponent } from './list-skill-group/list-skill-group.component';
import { AdminComponent } from '../../layouts/admin/admin.component';
import { AuthGuard } from '../../helpers/AuthGuard';

export const SkillGroupRoutes: Routes = [{
    path: '',
    canActivate: [AuthGuard],
    children: [{
        path: 'skills',
        component: ListSkillGroupComponent
    }]
}];
