import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './components/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TransferHttpCacheModule } from '@nguniversal/common';
import { GlobalDirectivesModule } from './modules/directives/global-directives/global-directives.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule.withServerTransition({ appId: 'serverApp' }),
    AppRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    TransferHttpCacheModule,
    GlobalDirectivesModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
