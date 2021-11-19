import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuillModule } from 'ngx-quill';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { RichTextEditorComponent } from './rich-text-editor/rich-text-editor.component';
import { DatePrinterComponent } from './date-printer/date-printer.component';

@NgModule({
    imports: [
        CommonModule,
        QuillModule.forRoot(),
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [
        RichTextEditorComponent,
        DatePrinterComponent
    ],
    exports: [
        RichTextEditorComponent,
        DatePrinterComponent
    ]
})

export class ComponentsModule {}