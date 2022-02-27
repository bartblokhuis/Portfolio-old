import { Routes } from "@angular/router";

export const LocalizationRoutes: Routes = [
    {
        path: '',
        loadChildren: () => import('./languages/languages.module').then(
          module => module.LanguagesModule
        ),
    },
]