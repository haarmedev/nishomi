import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CATEGORIESAPI,
  FEATUREDPRODUCTSAPI,
  IMAGE_ENDPOINT,
} from '../core/constants/constant';
import { SwiperOptions } from 'swiper';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';

declare var $: any;
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, OnDestroy, AfterViewInit {
  constructor(private httpClient: HttpClient, private router: Router, private translate: TranslateService) {
    this.langSubscription = this.translate.onLangChange.subscribe(() => { this.setLangKey(); });
  }
  langKey = '';
  mySwiper: SwiperOptions;
  categories = [];
  featuredproducts: [];
  imageurl: string;
  currentLang: string;
  langSubscription: Subscription;

  config: SwiperOptions = {
    // autoplay: 6000,
    // pagination: '.prodpage',
    // paginationClickable: true,
    pagination: {
      el: '.prodpage',
      type: 'bullets',
      clickable: true,
    },
    breakpoints: {
      640: {
        slidesPerView: 1,
        spaceBetween: 15,
        slidesPerGroup: 1,
      },
      768: {
        slidesPerView: 2,
        spaceBetween: 10,
        slidesPerGroup: 2,
      },
      1024: {
        slidesPerView: 5,
        spaceBetween: 10,
        slidesPerGroup: 3,
      },
      1920: {
        slidesPerView: 5,
        spaceBetween: 15,
        slidesPerGroup: 4,
        centeredSlides: true,
        loop: true,
        loopFillGroupWithBlank: true,
      },
    },
  };

  catconfig: SwiperOptions = {
    // pagination: '.swiper-pagination',
    // paginationClickable: true,
    pagination: {
      el: '.bannpage',
      // type: 'bullets',
      clickable: true,
    },
    effect: 'coverflow',
    loop: true,
    centeredSlides: true,
    slidesPerView: 'auto',
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    // coverflow: {
    // 	rotate: 0,
    // 	stretch: 100,
    // 	depth: 100,
    // 	modifier: 2.5,
    // 	slideShadows : false,
    // },
    // keyboardControl: true,
    // nextButton: '.swiper-button-next',
    // prevButton: '.swiper-button-prev',
  };

  ngOnInit(): void {
    this.imageurl = IMAGE_ENDPOINT;
    this.httpClient.get<any>(CATEGORIESAPI).subscribe((data: any) => {
      this.categories = data.data;
    });
    this.httpClient.get<any>(FEATUREDPRODUCTSAPI).subscribe((data: any) => {
      this.featuredproducts = data.data;
      setTimeout(() => {
        const sliderScript = document.createElement('script');
        sliderScript.setAttribute('src', './assets/js/home.js');
        document.body.appendChild(sliderScript);
      });
    });
    this.setLangKey();
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      const myScript = document.createElement('script');
      myScript.setAttribute('src', './assets/js/home.js');
      document.body.appendChild(myScript);
    }, 500);
  }

  setLangKey(): void {
    this.currentLang = this.translate.currentLang;
    this.langKey = 'ar' === this.currentLang ? 'Ar' : '';
  }

  gotoDetails(id: any): void {
    this.router.navigate(['/productdetails'], { queryParams: { id } });
  }

  gotoCategory(id: any): void {
    this.router.navigate(['/products'], { queryParams: { id } });
  }

  ngOnDestroy(): void {
    this.langSubscription.unsubscribe();
  }
}
