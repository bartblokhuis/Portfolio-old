import { Routes } from "@angular/router";

export const ConfigurationRoutes: Routes = [
    {
        path: '',
        loadChildren: () => import('./settings/settings.module').then(
          module => module.SettingsModule
        ),
      },
]