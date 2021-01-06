import { Component, OnInit } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser';
import { META } from '../core/constants/constant';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css']
})
export class BlogsComponent implements OnInit {

  constructor(
    private titleService: Title,
    private metaTagService: Meta,
  ) { }

  ngOnInit(): void {
    this.titleService.setTitle(META.BLOGS.TITLE);
    this.metaTagService.updateTag(
      { name: 'description', content: META.BLOGS.DESC },
    );
  }

}
