import { Component, OnInit } from '@angular/core';
import { Title, Meta } from '@angular/platform-browser';
import { META } from '../core/constants/constant';

@Component({
  selector: 'app-contactus',
  templateUrl: './contactus.component.html',
  styleUrls: ['./contactus.component.css']
})
export class ContactusComponent implements OnInit {

  constructor(
    private titleService: Title,
    private metaTagService: Meta,
  ) { }

  ngOnInit(): void {
    this.titleService.setTitle(META.CONTACT_US.TITLE);
    this.metaTagService.updateTag(
      { name: 'description', content: META.CONTACT_US.DESC },
    );
  }

}
