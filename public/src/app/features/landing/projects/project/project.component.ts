import { DOCUMENT } from '@angular/common';
import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { Project } from 'src/app/data/project/Project';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  @Input() project: Project | null = null;
  @Output() onClose: EventEmitter<any> = new EventEmitter();
  baseUrl: string = environment.baseApiUrl;

  constructor(@Inject(DOCUMENT) private document: Document) { }


  ngOnInit(): void {
    console.log(this.project)

    
  }

  close() {
    var container = this.document.getElementsByClassName('pop-up-container')[0];
    container.classList.add('close');
    setTimeout(() => this.onClose.emit(), 300);
  }

}
