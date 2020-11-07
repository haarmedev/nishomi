import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CATEGORIESAPI,
  FEATUREDPRODUCTSAPI,
  IMAGE_ENDPOINT,
} from '../core/constants/constant';
import { SwiperOptions } from 'swiper';
import { ActivatedRoute, Router } from '@angular/router';
declare var $: any;
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private httpClient: HttpClient, private router: Router) {}
  mySwiper: SwiperOptions;
  categories = [];
  featuredproducts: [];
  imageurl: string;

  config: SwiperOptions = {
    //autoplay: 6000,
    //pagination: '.prodpage',
    //paginationClickable: true,
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
    //pagination: '.swiper-pagination',
    //paginationClickable: true,
    pagination: {
      el: '.bannpage',
      //type: 'bullets',
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
    //keyboardControl: true,
    //nextButton: '.swiper-button-next',
    //prevButton: '.swiper-button-prev',
  };

  ngOnInit(): void {
    this.imageurl = IMAGE_ENDPOINT;
    this.httpClient.get<any>(CATEGORIESAPI).subscribe((data: any) => {
      console.log(data);
      this.categories = data.data;
    });
    this.httpClient.get<any>(FEATUREDPRODUCTSAPI).subscribe((data: any) => {
      console.log(data);
      this.featuredproducts = data.data;
    });
    //console.log("result"+this.categories);
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      let myScript = document.createElement('script');
      myScript.setAttribute('src', './assets/js/home.js');
      document.body.appendChild(myScript);
    }, 500);
  }

  gotoDetails(id: any) {
    this.router.navigate(['/productdetails'], { queryParams: { id: id } });
  }
}
