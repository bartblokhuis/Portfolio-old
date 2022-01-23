import { Routes } from "@angular/router";
import { AntiAuthGuard } from "../../helpers/AntiAuthGuard";
import { AuthGuard } from "../../helpers/AuthGuard";
import { AdminComponent } from "../../layouts/admin/admin.component";
import { AuthComponent } from "../../layouts/auth/auth.component";
import { LoginComponent } from "./login/login.component";
import { UserEditComponent } from "./user-edit/user-edit.component";

export const UserRoutes: Routes = [
    {
        path: '',
        component: AuthComponent,
        children: [
            {
                path: 'login',
                component: LoginComponent
            }
        ],
        canActivate: [AntiAuthGuard]
        
    },
    {
        path: '',
        component: AdminComponent,
        children: [
            {
                path: 'user/details',
                component: UserEditComponent
            }
        ],
        canActivate: [AuthGuard]
    }
]