import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from '../shared/common.service';

declare var $: any;

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.css']
})
export class LanguageSelectorComponent implements AfterViewInit {

  @ViewChild("languageModal") languageModal: ElementRef;

  constructor(
    private modalService: NgbModal,
    private commonService: CommonService,
  ) { }

  ngAfterViewInit() {
    this.openPopup();
  }

  openPopup() {
    const options = { centered: true, ariaLabelledBy: 'language-modal' };
    this.modalService.open(this.languageModal, options).result.then(
      (lang) => this.changeLangage(lang),
      (lang) => this.changeLangage(lang),
    );
  }

  /**
   * @description To change application language.
   * @param lang Selected language.
   */
  changeLangage(lang: string = 'en'): void {
    this.commonService.changeLangage(lang);
  }
}
