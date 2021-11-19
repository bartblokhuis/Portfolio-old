import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AboutMe } from '../../../data/AboutMe';
import { AboutMeService } from '../../../services/about-me/about-me.service';

@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrls: ['./about-me.component.scss']
})
export class AboutMeComponent implements OnInit {

  public abouteMe: AboutMe = {title: "", content: ""};
  showSaveButton: boolean = false;

  public aboutMeForm = new FormGroup({
    title: new FormControl(''),
    content: new FormControl('')
  });

  constructor(private aboutMeService: AboutMeService, private toastr: ToastrService) {

    aboutMeService.getAboutMe().subscribe((aboutMe) => {
      this.aboutMeForm.controls.title.setValue(aboutMe.title);
      this.aboutMeForm.controls.content.setValue(aboutMe.content);
    });
  }

  ngOnInit(): void {
    this.showSaveButton = false;
  }

  changedAboutMe(): void {
    this.showSaveButton = true;
  }

  saveAboutMe(): void {
    this.aboutMeService.saveAboutMe(this.aboutMeForm.value).subscribe((aboutMe) => {
      this.abouteMe = aboutMe;
      this.toastr.success("Saved changes");
      this.showSaveButton = false;
    });
  }

}
