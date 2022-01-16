import { Routes } from "@angular/router";
import { EmailSettingsComponent } from "./email-settings/email-settings.component";
import { GeneralSettingsComponent } from "./general-settings/general-settings.component";
import { SeoSettingsComponent } from "./seo-settings/seo-settings.component";

export const SettingRoutes: Routes = [
    {
        path: 'settings',
        children: [
            {
                path: 'general-settings',
                component: GeneralSettingsComponent
            },
            {
                path: 'seo-settings',
                component: SeoSettingsComponent
            },
            {
                path: 'email-settings',
                component: EmailSettingsComponent
            }
        ]
    }
]