import { Component, OnInit } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser';
import { META } from '../core/constants/constant';

@Component({
  selector: 'app-blog1',
  templateUrl: './blog1.component.html',
  styleUrls: ['./blog1.component.css']
})
export class Blog1Component implements OnInit {

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
