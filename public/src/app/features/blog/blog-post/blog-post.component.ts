import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.scss']
})
export class BlogPostComponent implements OnInit {

  id: number | null = null;

  constructor(private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const idParam = this.activatedRoute.snapshot.paramMap.get("id");
    if(!idParam){
      this.router.navigate([`/`]);
      return;
    }

    const id = parseInt(idParam);
    if(Number.isNaN(id)){
      this.router.navigate([`blog`]);
      return;
    }
  }

}
