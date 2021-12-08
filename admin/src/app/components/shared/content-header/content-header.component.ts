import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { BreadcrumbItem } from 'src/app/data/breadcrumb-item';
import { BreadcrumbsService } from 'src/app/services/breadcrumbs/breadcrumbs.service';
import { ContentTitleService } from 'src/app/services/content-title/content-title.service';

@Component({
  selector: 'app-content-header',
  templateUrl: './content-header.component.html',
  styleUrls: ['./content-header.component.scss']
})
export class ContentHeaderComponent implements OnInit {

  title: string = '';
  breadcrumbs: BreadcrumbItem[] = [];

  constructor(private breadcrumbsService: BreadcrumbsService, private contentTitleService: ContentTitleService) {

    this.breadcrumbsService.breadcrumbItems.subscribe((breadcrumbs: BreadcrumbItem[]) => {
      this.breadcrumbs = breadcrumbs;
    });

    this.contentTitleService.title.subscribe((title: string) => {
      this.title = title;
    });
  }

  ngOnInit(): void {
  }

}
