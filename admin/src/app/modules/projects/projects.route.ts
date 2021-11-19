import { Routes } from '@angular/router';
import { AuthGuard } from '../../helpers/AuthGuard';
import { AdminComponent } from '../../layouts/admin/admin.component';

import { ListProjectComponent } from './list-project/list-project.component';

export const ProjectRoutes: Routes = [{
    path: '',
    canActivate: [AuthGuard],
    children: [{
        path: 'projects',
        component: ListProjectComponent
    }]
}];
