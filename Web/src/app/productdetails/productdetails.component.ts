import {
  Component,
  OnInit,
  AfterViewInit,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CATEGORYPRODUCTS,
  CUSTOMERREQUEST,
  IMAGE_ENDPOINT,
  PRODUCTDETAILS,
} from '../core/constants/constant';
import { FormGroup, FormControl } from '@angular/forms';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
declare var $: any;
@Component({
  selector: 'app-productdetails',
  templateUrl: './productdetails.component.html',
  styleUrls: ['./productdetails.component.css'],
})
export class ProductdetailsComponent implements OnInit {
  profileForm = new FormGroup({
    name: new FormControl(''),
    cusmail: new FormControl(''),
    phone: new FormControl(''),
    address: new FormControl(''),
    message: new FormControl(''),
  });
  fullLength = new FormControl(0);
  sleeveLength = new FormControl(0);
  bust = new FormControl(0);
  hip = new FormControl(0);
  email: string;
  productId: string;
  constructor(
    private httpClient: HttpClient,
    public dialog: MatDialog,
    private route: ActivatedRoute
  ) {}
  product: {
    productCode: '';
    categoryName: '';
    name: '';
    cost: 0;
    description: '';
    images: [];
  };
  productDetails: any = [];
  quantity: number = 1;
  closeResult: string;
  isRequest: boolean;
  selectedproduct: [];
  imageurl: string;
  enableZoom: Boolean = true;
  showCustomsize: Boolean = false;
  selectedSize: string = '';
  sliderHtml: string = '';
  zoomHtml: string = '';
  ngOnInit(): void {
    this.quantity = 1;
    this.imageurl = IMAGE_ENDPOINT;
    this.route.queryParams.subscribe((params) => {
      this.productId = params.id;
    });
    var fetchProductDetails = PRODUCTDETAILS + '?productId=' + this.productId;
    this.httpClient.get<any>(fetchProductDetails).subscribe((data: any) => {
      console.log(data);
      this.product = data.data;
      this.productDetails = data.data;
      // for(let i=0;i<this.productDetails.images.length;i++){
      //   this.sliderHtml+='<div class="product-dec-small">'+
      //                   '<img src='+this.imageurl+this.productDetails.images[i].imageUrl+' alt=""></div>';

      //   this.zoomHtml+='<div class="easyzoom-style"><div class="easyzoom easyzoom--overlay">'+
      //             '<a href='+this.imageurl+this.productDetails.images[i].imageUrl+'>'+
      //             '<img src='+this.imageurl+this.productDetails.images[i].imageUrl+' alt=""></a></div>'+
      //             '<a class="easyzoom-pop-up img-popup popmag" href='+this.imageurl+this.productDetails.images[i].imageUrl+'></a></div>';
      // }
    });
    $('.btncart').click(function () {
      $('html, body').animate(
        {
          scrollTop: $('.btncart').offset().top,
        },
        2000
      );
    });
    // var $easyzoom = $('.easyzoom').easyZoom();

    //     $('.pro-dec-big-img-slider').slick({
    //       slidesToShow: 1,
    //       slidesToScroll: 1,
    //       arrows: false,
    //       draggable: false,
    //       fade: false,
    //       asNavFor: '.product-dec-slider , .product-dec-slider-2',
    //     });

    // /*---------------------------------
    //     Product details slider 2 active
    //     -----------------------------------*/
    //     $('.product-dec-slider-2').slick({
    //       slidesToShow: 4,
    //       slidesToScroll: 1,
    //       vertical: true,
    //       asNavFor: '.pro-dec-big-img-slider',
    //       dots: false,
    //       focusOnSelect: true,
    //       fade: false,
    //       prevArrow: '<span class="pro-dec-icon pro-dec-prev"><i class="icofont-rounded-up"></i></span>',
    //       nextArrow: '<span class="pro-dec-icon pro-dec-next"><i class="icofont-rounded-down"></i></i></span>',
    //       responsive: [{
    //         breakpoint: 767,
    //         settings: {

    //         }
    //       },
    //       {
    //         breakpoint: 420,
    //         settings: {
    //           autoplay: true,
    //           slidesToShow: 2,
    //         }
    //       }
    //       ]
    //     });
  }
  ngAfterViewInit(): void {
    //this.slides=this.imageurl+this.productDetails.images[0].imageUrl;
    //alert(this.slides);
    var $easyzoom = $('.easyzoom').easyZoom();

    $('.pro-dec-big-img-slider').slick({
      slidesToShow: 1,
      slidesToScroll: 1,
      arrows: false,
      draggable: false,
      fade: false,
      asNavFor: '.product-dec-slider , .product-dec-slider-2',
    });

    /*---------------------------------
        Product details slider 2 active
        -----------------------------------*/
    $('.product-dec-slider-2').slick({
      slidesToShow: 4,
      slidesToScroll: 1,
      vertical: true,
      asNavFor: '.pro-dec-big-img-slider',
      dots: false,
      focusOnSelect: true,
      fade: false,
      prevArrow:
        '<span class="pro-dec-icon pro-dec-prev"><i class="icofont-rounded-up"></i></span>',
      nextArrow:
        '<span class="pro-dec-icon pro-dec-next"><i class="icofont-rounded-down"></i></i></span>',
      responsive: [
        {
          breakpoint: 767,
          settings: {},
        },
        {
          breakpoint: 420,
          settings: {
            autoplay: true,
            slidesToShow: 2,
          },
        },
      ],
    });

    $('.popmag').magnificPopup({
      type: 'image',
      gallery: {
        enabled: true,
      },
    });
  }
  buyProduct() {
    var formData: any = new FormData();
    formData.append('ProductId', this.productId);
    formData.append('Name', this.profileForm.value.name);
    formData.append('Email', this.profileForm.value.cusmail);
    formData.append('ContactNumber', this.profileForm.value.phone);
    formData.append('Address', this.profileForm.value.address);
    formData.append('Message', this.profileForm.value.message);
    if (
      this.fullLength.value > 0 &&
      this.sleeveLength.value > 0 &&
      this.bust.value > 0 &&
      this.hip.value > 0
    ) {
      formData.append(
        'Size',
        'Size: FullLength: ' +
          this.fullLength.value +
          ' SleeveLength: ' +
          this.sleeveLength.value +
          ' Bust: ' +
          this.bust.value +
          ' Hip: ' +
          this.hip.value
      );
    } else {
      formData.append('Size', 'Size: ' + this.selectedSize);
    }
    this.httpClient.post(CUSTOMERREQUEST, formData).subscribe(
      (response: any) => {
        console.log(response);
        if (response.data) {
          this.isRequest = false;
          Swal.fire(
            'Thank you ' +
              this.profileForm.value.name +
              ' for your interest on ' +
              this.product.name +
              '. We will contact you soon!!!'
          );
          this.profileForm.reset();
        }
      },
      (error) => console.log(error)
    );
  }
  selectProduct(pro) {
    console.log(pro);
    this.selectedproduct = pro;
  }

  changeQuantity(stat: any) {
    if (stat == 'increment') this.quantity = ++this.quantity;
    else {
      if (this.quantity != 1) {
        this.quantity = --this.quantity;
      }
    }
  }

  toggleCustomeSize() {
    this.selectedSize='';
    this.showCustomsize = !this.showCustomsize;
  }

  selectSize(size) {
    if (this.showCustomsize) {
      this.toggleCustomeSize();
    }
    this.selectedSize = size;
  }
}
