import { Routes } from "@angular/router";
import { ApiSettingsComponent } from "./api-settings/api-settings.component";
import { BlogSettingsComponent } from "./blog-settings/blog-settings.component";
import { EmailSettingsComponent } from "./email-settings/email-settings.component";
import { GeneralSettingsComponent } from "./general-settings/general-settings.component";
import { PublicSiteSettingsComponent } from "./public-site-settings/public-site-settings.component";
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
            },
            {
                path: 'blog-settings',
                component: BlogSettingsComponent
            },
            {
                path: 'public-site-settings',
                component: PublicSiteSettingsComponent
            },
            {
                path: 'api-settings',
                component: ApiSettingsComponent
            }
        ]
    }
]