import { Component, OnInit } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser';
import { META } from '../core/constants/constant';

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html',
  styleUrls: ['./story.component.css']
})
export class StoryComponent implements OnInit {

  constructor(
    private titleService: Title,
    private metaTagService: Meta,
  ) { }

  ngOnInit(): void {
    this.titleService.setTitle(META.STORY.TITLE);
    this.metaTagService.updateTag(
      { name: 'description', content: META.STORY.DESC },
    );
  }

}
