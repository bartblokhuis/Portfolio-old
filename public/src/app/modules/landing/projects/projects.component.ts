import { Component, Input, OnInit } from '@angular/core';
import { Project } from 'src/app/data/Project';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {

  @Input() projects: Project[] | null = [];
  baseUrl = environment.baseApiUrl;

  constructor() { }

  ngOnInit(): void {
  }

}
