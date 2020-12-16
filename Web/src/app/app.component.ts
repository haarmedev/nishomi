import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { CommonService } from './shared/common.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  showLanguageSelector = false;

  constructor(
    private router: Router,
    private commonService: CommonService,
    private activatedRoute: ActivatedRoute) {
    this.activatedRoute.queryParams.subscribe(params => {
      const lang = params.lang ? params.lang : localStorage.getItem('lang');
      debugger
      if (window.innerWidth <= 767 && !lang) {
        this.showLanguageSelector = true;
      }
      this.changeLangage(lang ? lang : 'en');
    });
  }

  ngOnInit(): void {
    this.scrollToPageTop();
  }

  /**
   * @description To change application language.
   * @param lang Selected language.
   */
  changeLangage(lang: string): void {
    this.commonService.changeLangage(lang);
  }

  /**
   * @description To scroll to page top on page navigation.
   */
  scrollToPageTop(): void {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        window.scrollTo(0, 0);
      }
      return;
    });
  }
}
