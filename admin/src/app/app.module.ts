import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { QuillModule } from 'ngx-quill';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedComponentsModule } from './components/shared/shared-components.module';
import { ErrorInterceptor } from './helpers/error-interceptor';
import { JwtInterceptor } from './helpers/jwt-interceptor';
import { AdminComponent } from './layouts/admin/admin.component';
import { AuthComponent } from './layouts/auth/auth.component';
import { AboutMeModule } from './modules/about-me/about-me.module';
import { BlogPostsModule } from './modules/blog-posts/blog-posts.module';
import { MessagesModule } from './modules/messages/messages.module';
import { PicturesModule } from './modules/pictures/pictures.module';
import { ProjectsModule } from './modules/projects/projects.module';
import { SettingsModule } from './modules/settings/settings.module';
import { SkillGroupsModule } from './modules/skill-groups/skill-groups.module';
import { UserModule } from './modules/user/user.module';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    AuthComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AboutMeModule,
    SkillGroupsModule,
    MessagesModule,
    ProjectsModule,
    UserModule,
    SettingsModule,
    PicturesModule,
    BlogPostsModule,
    SharedComponentsModule,
    QuillModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
