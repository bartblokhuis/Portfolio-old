import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemeSwitcherComponent } from './theme-switcher.component';



@NgModule({
  declarations: [
    ThemeSwitcherComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ThemeSwitcherComponent
  ]
})
export class ThemeSwitcherModule { }
