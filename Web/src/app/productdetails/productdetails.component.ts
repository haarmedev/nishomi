import {
  Component,
  OnInit,
  AfterViewInit,
  ViewChild,
  ElementRef,
  OnDestroy,
} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CATEGORYPRODUCTS,
  CUSTOMERREQUEST,
  IMAGE_ENDPOINT,
  PRODUCTDETAILS,
} from '../core/constants/constant';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
declare var $: any;
@Component({
  selector: 'app-productdetails',
  templateUrl: './productdetails.component.html',
  styleUrls: ['./productdetails.component.css'],
})
export class ProductdetailsComponent implements OnInit, OnDestroy {
  profileForm = new FormGroup({
    acceptTAC: new FormControl(false, [Validators.requiredTrue]),
    name: new FormControl('', [Validators.required]),
    cusmail: new FormControl('', [Validators.required, Validators.pattern(/^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i)]),
    phone: new FormControl('', [Validators.required, Validators.minLength(7), Validators.maxLength(11)]),
    address: new FormControl('', [Validators.required]),
    message: new FormControl(''),
  });
  fullLength = new FormControl(0);
  sleeveLength = new FormControl(0);
  bust = new FormControl(0);
  hip = new FormControl(0);
  email: string;
  productId: string;
  showContactForm = false;
  orderClicked = false;
  submitted = false;
  constructor(
    private httpClient: HttpClient,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private translate: TranslateService) {
    this.langSubscription = this.translate.onLangChange.subscribe(() => { this.setLangKey(); });
  }
  langKey = '';
  currentLang: string;
  langSubscription: Subscription;
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
  isBuyNow: Boolean = false;
  ngOnInit(): void {
    this.setLangKey();
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
      setTimeout(() => this.initSlider(), 500);
      // for(let i=0;i<this.productDetails.images.length;i++){
      //   this.sliderHtml+='<div class="product-dec-small">'+
      //                   '<img src='+this.imageurl+this.productDetails.images[i].imageUrl+' alt=""></div>';

      //   this.zoomHtml+='<div class="easyzoom-style"><div class="easyzoom easyzoom--overlay">'+
      //             '<a href='+this.imageurl+this.productDetails.images[i].imageUrl+'>'+
      //             '<img src='+this.imageurl+this.productDetails.images[i].imageUrl+' alt=""></a></div>'+
      //             '<a class="easyzoom-pop-up img-popup popmag" href='+this.imageurl+this.productDetails.images[i].imageUrl+'></a></div>';
      // }
    });
    // $('.btncart').click(function () {
    //   $('html, body').animate(
    //     {
    //       scrollTop: $('.btncart').offset().top,
    //     },
    //     2000
    //   );
    // });
    // $(".btnbuy").click(function(){
    //   $(".proddesc").toggleClass("show");
    // });
    // $(".btncart").click(function(){
    //   $(".proddesc").toggleClass("show");
    // });
  }
  ngAfterViewInit(): void {}
  initSlider(): void {
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
    /*
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if(this.profileForm.value.name===''){
      Swal.fire('Name is Mandatory');
      return;
    }
    if (this.profileForm.value.cusmail === '') {
      Swal.fire('Email Id is Mandatory');
      return;
    } else {
      if (!testEmail.test(this.profileForm.value.cusmail)) {
        Swal.fire('Enter Valid Email Id');
        return;
      }
    }
    if(this.profileForm.value.phone===''){
      Swal.fire('Contact Number is Mandatory');
      return;
    }
    if(this.profileForm.value.address===''){
      Swal.fire('Address is Mandatory');
      return;
    }
    */
   this.submitted = true;
   if (this.profileForm.valid) {
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
            if(this.isBuyNow){
              Swal.fire('Thanks for placing this order. We will connect you shortly and deliver this product to  your address');
            }
            else{
              Swal.fire('Thanks for your interest on this product. We will connect you shortly to know more about your interest and help you with your purchase');
            }
            this.profileForm.reset();
            this.submitted = false;
            this.orderClicked = false;
          }
        },
        (error) => console.log(error)
      );
   }
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
    this.selectedSize = '';
    this.showCustomsize = !this.showCustomsize;
  }

  selectSize(size) {
    if (this.showCustomsize) {
      this.toggleCustomeSize();
    }
    this.selectedSize = size;
  }

  setBuyNow(status) {
    this.orderClicked = true;
    if (this.profileForm.controls.acceptTAC.value) {
      if (status == 'buy') {
        if (this.isBuyNow) {
          this.showContactForm = !this.showContactForm;
          this.isBuyNow = false;
        } else {
          if (this.showContactForm) {
            this.isBuyNow = true;
          } else {
            this.showContactForm = true;
            this.isBuyNow = true;
          }
          this.scrollDiv();
        }
      } else {
        if (this.isBuyNow) {
          this.isBuyNow = false;
          this.scrollDiv();
        } else {
          this.showContactForm = !this.showContactForm;
          this.isBuyNow = false;
          if (this.showContactForm) {
            this.scrollDiv();
          }
        }
      }
    }
  }
  scrollDiv() {
    $('html, body').animate(
      {
        scrollTop: $('.btncart').offset().top,
      },
      1000
    );
  }

  setLangKey(): void {
    this.currentLang = this.translate.currentLang;
    this.langKey = 'ar' === this.currentLang ? 'Ar' : '';
  }

  ngOnDestroy(): void {
    this.langSubscription.unsubscribe();
  }
}
