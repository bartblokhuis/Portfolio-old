import { Routes } from "@angular/router";
import { LanguagesListComponent } from "./languages-list/languages-list.component";

export const LanguageRoutes: Routes = [
    {
        path: 'languages',
        children: [
            {
                path: '',
                component: LanguagesListComponent,
            }
        ]
    }
]