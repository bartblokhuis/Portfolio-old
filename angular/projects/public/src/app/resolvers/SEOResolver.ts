import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { SettingsService } from '../services/settings/settings.service';

@Injectable()
export class SEOResolver implements Resolve<any> {

  private result: any;
  constructor(private settingsService: SettingsService, private title: Title) { }

  resolve(route: ActivatedRouteSnapshot,state: RouterStateSnapshot) {
      this.title.setTitle("Test")
  }
}