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
      if (lang || window.innerWidth > 767) {
        this.changeLangage(lang || 'en');
      } else if (window.innerWidth <= 767) {
        this.commonService.initLang();
        this.showLanguageSelector = true;
      }
      // if (window.innerWidth <= 767) {
      //   if (lang) {
      //     this.changeLangage(lang);
      //   } else {
      //     this.commonService.initLang();
      //     this.showLanguageSelector = true;
      //   }
      // } else {
      //   this.changeLangage(lang ? lang : 'en');
      // }
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
