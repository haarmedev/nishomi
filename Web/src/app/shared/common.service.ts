import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class CommonService {

  constructor(private translate: TranslateService, @Inject(DOCUMENT) private document: Document) { }

  initLang() {
    const defaultLang = 'en';
    this.translate.setDefaultLang(defaultLang);
    this.translate.use(defaultLang);
    this.changeCssFile(defaultLang);
  }

  /**
   * @description To change application language.
   * @param lang Selected language.
   */
  changeLangage(lang: string): void {
    const htmlTag: any = this.document.getElementsByTagName('html')[0] as HTMLHtmlElement;
    htmlTag.dir = lang === 'ar' ? 'rtl' : 'ltr';
    this.document.documentElement.lang = lang;
    this.translate.setDefaultLang(lang);
    this.translate.use(lang);
    localStorage.setItem('lang', lang);
    this.changeCssFile(lang);
  }

  /**
   * @description To change the css file(ltr or rtl styles) based on selected language.
   * @param lang Selected language.
   */
  changeCssFile(lang: string): void {
    const headTag = this.document.getElementsByTagName('head')[0] as HTMLHeadElement;
    const existingLink = this.document.getElementById('langCss') as HTMLLinkElement;
    const bundleName = lang === 'ar' ? 'arabicStyles.css' : 'englishStyles.css';
    if (existingLink) {
       existingLink.href = bundleName;
    } else {
       const newLink = this.document.createElement('link');
       newLink.rel = 'stylesheet';
       newLink.type = 'text/css';
       newLink.id = 'langCss';
       newLink.href = bundleName;
       headTag.appendChild(newLink);
    }
  }

}
