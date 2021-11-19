import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { PageHeaderComponent } from './page-header/page-header.component';

@NgModule({
  declarations: [FooterComponent, HeaderComponent, SidebarComponent, PageHeaderComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    PageHeaderComponent
  ]
})
export class SharedModule { }
