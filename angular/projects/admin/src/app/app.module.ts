import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedComponentsModule } from './components/shared/shared-components.module';
import { ErrorInterceptor } from './helpers/error-interceptor';
import { JwtInterceptor } from './helpers/jwt-interceptor';
import { AdminComponent } from './layouts/admin/admin.component';
import { AuthComponent } from './layouts/auth/auth.component';
import { AboutMeModule } from './features/about-me/about-me.module';
import { BlogPostsModule } from './features/blog-posts/blog-posts.module';
import { MessagesModule } from './features/messages/messages.module';
import { PicturesModule } from './features/pictures/pictures.module';
import { ProjectsModule } from './features/projects/projects.module';
import { SettingsModule } from './features/settings/settings.module';
import { SkillGroupsModule } from './features/skill-groups/skill-groups.module';
import { UserModule } from './features/user/user.module';
import { RichTextEditorModule } from './components/rich-text-editor/rich-text-editor.module';
import { BlogSubscribersModule } from './features/blog-subscribers/blog-subscribers.module';
import { EmailQueueModule } from './features/email-queue/email-queue.module';
import { ScheduleTaskModule } from './features/schedule-task/schedule-task.module';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    AuthComponent
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
    BlogSubscribersModule,
    SharedComponentsModule,
    RichTextEditorModule,
    EmailQueueModule,
    ScheduleTaskModule
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
