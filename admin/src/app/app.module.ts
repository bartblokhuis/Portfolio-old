import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS }  from "@angular/common/http";
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { JwtInterceptor } from './helpers/JwtInterceptor';
import { ErrorInterceptor } from './helpers/ErrorInterceptor';
import { AdminComponent } from './layouts/admin/admin.component';
import { AuthComponent } from './layouts/auth/auth.component';
import { SharedModule } from './components/shared/shared.module';
import { AboutMeModule } from './modules/about-me/about-me.module';
import { MessagesModule } from './modules/messages/messages.module';
import { ComponentsModule } from './components/components.module';
import { ProjectsModule } from './modules/projects/projects.module'; 
import { DashboardModule } from './modules/dashboard/dashboard.module';
import { UserModule } from './modules/user/user.module';
import { SkillGroupsModule } from './modules/skill-groups/skill-groups.module';
import { SettingsModule } from './modules/settings/settings.module';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    AuthComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    AppRoutingModule,
    UserModule,
    AboutMeModule,
    MessagesModule,
    ProjectsModule,
    DashboardModule,
    SkillGroupsModule,
    SettingsModule,
    BrowserModule,
    SharedModule,
    NgbModule,
    HttpClientModule,
    ComponentsModule,
    ToastrModule.forRoot()
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi: true
  },{
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class AppModule { }
