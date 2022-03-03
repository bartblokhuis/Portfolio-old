import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FooterComponent } from "./footer/footer.component";
import { MainHeaderComponent } from "./main-header/main-header.component";
import { SidebarComponent } from "./sidebar/sidebar.component";
import { ContentHeaderComponent } from './content-header/content-header.component';
import { FormsModule } from "@angular/forms";
import { LocalizationComponentsModule } from "../localizations/localization-components.module";

@NgModule({
    declarations: [ FooterComponent, MainHeaderComponent, SidebarComponent, ContentHeaderComponent ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        LocalizationComponentsModule
    ],
    exports: [
        FooterComponent,
        MainHeaderComponent,
        SidebarComponent,
        ContentHeaderComponent
    ]
})
export class SharedComponentsModule { }