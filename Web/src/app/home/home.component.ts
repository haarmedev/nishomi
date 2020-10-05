import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {CATEGORIESAPI, FEATUREDPRODUCTSAPI} from '../core/constants/constant';
import { SwiperOptions } from 'swiper';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private httpClient: HttpClient) {}
  //mySwiper:Swiper;
  categories=[];
  featuredproducts:[];

  config: SwiperOptions = {
    //autoplay: 6000,
	//pagination: '.prodpage',
	//paginationClickable: true,
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
			slidesPerView: 3,
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
	}
  };

  ngOnInit(): void {
    this.httpClient.get<any>(CATEGORIESAPI).subscribe((data: any)=>{
      console.log(data);
      this.categories = data.data;
    });
    this.httpClient.get<any>(FEATUREDPRODUCTSAPI).subscribe((data: any)=>{
      console.log(data);
      this.featuredproducts = data.data;
    })  
    //console.log("result"+this.categories);
  }
}
