import {
  Component,
  OnInit,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CATEGORYPRODUCTS, CUSTOMERREQUEST, IMAGE_ENDPOINT, META } from '../core/constants/constant';
import { Title, Meta } from '@angular/platform-browser';
import { SwiperOptions } from 'swiper';
import { SwiperComponent } from 'ngx-useful-swiper';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { ModalComponent } from '../modal/modal.component';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
declare var $: any;

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
})
export class ProductsComponent implements OnInit, OnDestroy {
  profileForm = new FormGroup({
    name: new FormControl(''),
    cusmail: new FormControl(''),
    phone: new FormControl(''),
    address: new FormControl(''),
    message: new FormControl(''),
  });

  email: string;
  constructor(
    private httpClient: HttpClient,
    public dialog: MatDialog,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private router: Router,
    private titleService: Title,
    private metaTagService: Meta,
    private translate: TranslateService) {
    this.langSubscription = this.translate.onLangChange.subscribe(() => { this.setLangKey(); });
  }
  category: [];
  product: [];
  closeResult: string;
  isRequest: boolean;
  showModal: boolean;
  selectedproduct: [];
  imageurl: string;
  routeId: string;
  langKey = '';
  currentLang: string;
  langSubscription: Subscription;

  config: SwiperOptions = {
    loop: false,
    centeredSlides: false,
    autoplay: false,
    breakpoints: {
      767: {
        slidesPerView: 1,
        spaceBetween: 0,
        slidesPerGroup: 1,
      },
      1024: {
        slidesPerView: 3,
        spaceBetween: 0,
        slidesPerGroup: 3,
      },
      1920: {
        slidesPerView: 3,
        spaceBetween: 0,
        slidesPerGroup: 3,
      },
    },
    navigation: {
      nextEl: '.colcar1-next',
      prevEl: '.colcar1-prev',
    },
    keyboard: {
      enabled: true,
      onlyInViewport: false,
    },
    // keyboardControl: true,
    // nextButton: '.colcar1-next',
    // prevButton: '.colcar1-prev',
  };
  configs: SwiperOptions = {
    loop: false,
    centeredSlides: false,
    autoplay: false,
    breakpoints: {
      767: {
        slidesPerView: 1,
        spaceBetween: 0,
        slidesPerGroup: 1,
      },
      1024: {
        slidesPerView: 3,
        spaceBetween: 0,
        slidesPerGroup: 3,
      },
      1920: {
        slidesPerView: 3,
        spaceBetween: 0,
        slidesPerGroup: 3,
      },
    },
    navigation: {
      nextEl: '.colcar2-next',
      prevEl: '.colcar2-prev',
    },
    keyboard: {
      enabled: true,
      onlyInViewport: false,
    },
    // keyboardControl: true,
    // nextButton: '.colcar1-next',
    // prevButton: '.colcar1-prev',
  };

  config1: SwiperOptions = {
    slidesPerView: 1,
    spaceBetween: 0,
    effect: 'fade',
    speed: 1500,
    // spaceBetween: 100,
    slidesPerGroup: 1,
    centeredSlides: true,
    // autoplay: 5000,
    loop: true,
    loopFillGroupWithBlank: true,
    // keyboardControl: true,
    // pagination: '.bannpage',
    // paginationClickable: true,
  };

  @ViewChild('usefulSwiper', { static: false }) usefulSwiper: SwiperComponent;

  open(content): void {
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (result) => {
          this.closeResult = `Closed with: ${result}`;
        },
        (reason) => {
          // this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
        }
      );
  }

  ngOnInit(): void {
    this.setLangKey();
    this.route.queryParams.subscribe((params) => {
      this.routeId = params.id;
    });
    this.imageurl = IMAGE_ENDPOINT;
    this.showModal = true;
    this.httpClient.get<any>(CATEGORYPRODUCTS).subscribe((data: any) => {
      this.category = data.data;
      setTimeout(() => {
        const selectedCategory = document.getElementById(this.routeId);
        if (selectedCategory) {
          // selectedCategory.scrollIntoView({ behavior: 'smooth' });
          $('html, body').animate({ scrollTop: selectedCategory.offsetTop - 90 }, 1000);
        }
      });
    });
    setTimeout(() => {
      const myScript = document.createElement('script');
      myScript.setAttribute('src', './assets/js/home.js');
      document.body.appendChild(myScript);
    }, 500);

    this.titleService.setTitle(META.PRODUCTS.TITLE);
    this.metaTagService.updateTag(
      { name: 'description', content: META.PRODUCTS.DESC },
    );
  }

  selectProduct(pro): void {
    this.selectedproduct = pro;
  }

  onRequest(): void {
    this.isRequest = true;
  }

  gotoDetails(id: any): void{
      this.router.navigate(['/productdetails'], { queryParams: { id } });
  }

  closeModal(): void {
    this.isRequest = false;
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      width: '300px',
      data: {},
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.email = result;
    });
  }

  setLangKey(): void {
    this.currentLang = this.translate.currentLang;
    this.langKey = 'ar' === this.currentLang ? 'Ar' : '';
  }

  ngOnDestroy(): void {
    this.langSubscription.unsubscribe();
  }
}
