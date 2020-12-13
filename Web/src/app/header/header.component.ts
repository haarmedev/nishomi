import { Component, OnDestroy, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { CommonService } from '../shared/common.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {

  switchLang: string; // 'en' or 'ar'
  showBurgerMenu: boolean;
  langSubscription: Subscription;

  constructor(private route: Router, private commonService: CommonService, private translate: TranslateService) {
    this.langSubscription = this.translate.onLangChange.subscribe((event: any) => { this.setSwitchLang(event.lang); });
  }

  ngOnInit(): void {
    this.setSwitchLang(this.translate.currentLang);
    this.handleBurgerMenu(false);
  }

  /**
   * @description To set the language text for language switch.
   * @param lang Selected language.
   */
  setSwitchLang(lang: string): void {
    this.switchLang = ('en' === lang) ? 'ar' : 'en';
  }

  /**
   * @description To handle burger menu click.
   */
  handleBurgerMenu(showBurgerMenu = !this.showBurgerMenu): void {
    this.showBurgerMenu = showBurgerMenu;
    this.showBurgerMenu ? document.body.classList.add('overflow') : document.body.classList.remove('overflow');
  }

  /**
   * @description To change the language.
   * @param lang Selected language.
   */
  changeLanguage(lang: string): void {
    this.commonService.changeLangage(lang);
    // this.setSwitchLang(lang);
  }

  myFunction(): void {
    this.route.navigate(['/story']);
  }

  ngOnDestroy(): void {
    this.langSubscription.unsubscribe();
  }

}
