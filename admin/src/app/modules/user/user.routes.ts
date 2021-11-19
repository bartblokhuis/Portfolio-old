import { Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { EditComponent } from './edit/edit.component';
import { AuthComponent } from '../../layouts/auth/auth.component';
import { AdminComponent } from '../../layouts/admin/admin.component';

export const UserRoutes: Routes = [{
    path: '',
    component: AuthComponent,
    children: [{
        path: 'login',
        component: LoginComponent
    }]},
    {
        path: '',
        component: AdminComponent,
        children: [{
            path: 'user/details',
            component: EditComponent
        }]
    }
];
