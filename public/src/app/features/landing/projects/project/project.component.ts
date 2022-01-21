import { DOCUMENT } from '@angular/common';
import { Component, EventEmitter, HostListener, Inject, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { Project } from 'src/app/data/project/Project';
import { Skill } from 'src/app/data/Skill';
import { environment } from 'src/environments/environment';
import { SwiperOptions } from 'swiper';


@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ProjectComponent implements OnInit {

  @Input() project: Project | null = null;
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  baseUrl: string = environment.baseApiUrl;

  constructor(@Inject(DOCUMENT) private document: Document) { }


  ngOnInit(): void {
  }

  close(event: any) {

    //If the path length is more than 9 it is a click inside the pop up.
    if(event.path.length > 9) {
      return;
    }

    var container = this.document.getElementsByClassName('pop-up-container')[0];
    container.classList.add('close');
    setTimeout(() => this.onClose.emit(), 300);
  }

  config: SwiperOptions = {
    pagination: { el: '.swiper-pagination', clickable: true },
    autoHeight: false,
    allowTouchMove: true,
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev'
    },
    loop: true
  };

  printSkill(skills: Skill[] | undefined): string {
    if(!skills) return "";
    return skills?.map(x => x.name).join(" / ") ?? ""
  }


}
