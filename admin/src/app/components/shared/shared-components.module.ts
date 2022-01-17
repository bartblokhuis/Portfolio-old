import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FooterComponent } from "./footer/footer.component";
import { MainHeaderComponent } from "./main-header/main-header.component";
import { SidebarComponent } from "./sidebar/sidebar.component";
import { ContentHeaderComponent } from './content-header/content-header.component';
import { DatePrinterComponent } from './date-printer/date-printer.component';
import { PictureComponent } from './picture/picture.component';
import { FormsModule } from "@angular/forms";
import { QuillModule } from "ngx-quill";

@NgModule({
    declarations: [ FooterComponent, MainHeaderComponent, SidebarComponent, ContentHeaderComponent, DatePrinterComponent, PictureComponent ],
    imports: [
        CommonModule,
        FormsModule,
        QuillModule,
        RouterModule
    ],
    exports: [
        FooterComponent,
        MainHeaderComponent,
        SidebarComponent,
        ContentHeaderComponent,
        DatePrinterComponent,
        PictureComponent
    ]
})
export class SharedComponentsModule { }