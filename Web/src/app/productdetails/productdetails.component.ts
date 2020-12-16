import {
  Component,
  OnInit,
  OnDestroy,
} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CUSTOMERREQUEST,
  IMAGE_ENDPOINT,
  PRODUCTDETAILS,
} from '../core/constants/constant';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
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
    size: new FormControl('', [Validators.required]),
    fullLength: new FormControl(''),
    sleeveLength: new FormControl(''),
    bust: new FormControl(''),
    hip: new FormControl(''),
  });
  email: string;
  productId: string;
  showContactForm = false;
  orderClicked = false;
  submitted = false;
  apiInprogress = false;
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
  quantity = 1;
  closeResult: string;
  isRequest: boolean;
  selectedproduct: [];
  imageurl: string;
  enableZoom = true;
  showCustomsize = false;
  sliderHtml = '';
  zoomHtml = '';
  isBuyNow = false;
  ngOnInit(): void {
    this.setLangKey();
    this.quantity = 1;
    this.imageurl = IMAGE_ENDPOINT;
    this.route.queryParams.subscribe((params) => {
      this.productId = params.id;
    });
    const fetchProductDetails = PRODUCTDETAILS + '?productId=' + this.productId;
    this.httpClient.get<any>(fetchProductDetails).subscribe((data: any) => {
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

  initSlider(): void {
    $('.easyzoom').easyZoom();

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

  buyProduct(): void {
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
      this.apiInprogress = true;
      const formData: any = new FormData();
      formData.append('ProductId', this.productId);
      formData.append('Name', this.profileForm.value.name);
      formData.append('Email', this.profileForm.value.cusmail);
      formData.append('ContactNumber', this.profileForm.value.phone);
      formData.append('Address', this.profileForm.value.address);
      formData.append('Message', this.profileForm.value.message);
      formData.append('IsOrder', this.isBuyNow ? true : false);

      const size = `Size: ${this.profileForm.value.size}`;
      const customSize = `Custom Size: FullLength: ${this.profileForm.value.fullLength} SleeveLength: ${this.profileForm.value.sleeveLength} Bust: ${this.profileForm.value.bust} Hip: ${this.profileForm.value.hip}`;
      formData.append('Size', this.showCustomsize ? customSize : size);

      this.httpClient.post(CUSTOMERREQUEST, formData).subscribe(
        (response: any) => {
          this.apiInprogress = false;
          if (response.data) {
            this.isRequest = false;
            const msg = `success.${this.isBuyNow ?  'order' : 'show_interest'}`;
            const trsnslatedMsg = this.translate.instant(msg);
            Swal.fire({
              title: trsnslatedMsg,
              icon: 'success',
            });
            this.selectSize('');
            this.profileForm.reset();
            this.submitted = false;
            this.orderClicked = false;
          }
        },
        (error) => {
          this.apiInprogress = false;
          Swal.fire({
            title: this.translate.instant('error.something_went_wrong'),
            icon: 'error',
          });
        }
      );
   } else {
     setTimeout(() => {
      const errorMsg = document.getElementsByClassName('error')[0];
      if (errorMsg) {
        $('html, body').animate({ scrollTop: errorMsg.parentElement.offsetTop }, 1000);
      }
     });
   }
  }
  selectProduct(pro): void {
    this.selectedproduct = pro;
  }

  changeQuantity(stat: any): void {
    if (stat === 'increment') {
      this.quantity = ++this.quantity;
    } else {
      if (this.quantity !== 1) {
        this.quantity = --this.quantity;
      }
    }
  }

  /**
   * @description To select a predefined size.
   * @param size Selected size.
   */
  selectSize(size: string): void {
    // set value for the size field.
    this.profileForm.get('size').setValue(size);
    this.profileForm.get('size').setValidators([Validators.required]);
    this.profileForm.get('size').updateValueAndValidity();
    // remove validators for custom size
    this.updateCustomSizeValidators(false);
  }

  /**
   * @description To enable custom size.
   */
  setCustomSize(): void {
    // reset value to empty string for the size field.
    this.profileForm.get('size').setValue('');
    this.profileForm.get('size').clearValidators();
    this.profileForm.get('size').updateValueAndValidity();
    // add validators for custom size
    this.updateCustomSizeValidators(true);
  }

  /**
   * To add/remove required validation for custom size.
   */
  updateCustomSizeValidators(isRequired: boolean = true): void {
    this.showCustomsize = isRequired;
    const customeSizeFields = ['fullLength', 'sleeveLength', 'bust', 'hip'];
    customeSizeFields.forEach((field) => {
      if (isRequired) {
        this.profileForm.get(field).setValidators([Validators.required]);
      } else {
        this.profileForm.get(field).clearValidators();
      }
      this.profileForm.get(field).updateValueAndValidity();
    });
  }

  /**
   * @description Returns whether form is valid to show the form.
   */
  isValidForOrderForm(): boolean {
    let isValid = true;
    const fields = ['fullLength', 'sleeveLength', 'bust', 'hip', 'size', 'acceptTAC'];
    fields.forEach((field) => {
      if (!this.profileForm.get(field).valid) {
        isValid = false;
      }
    });
    return isValid;
  }

  setBuyNow(status): void {
    this.orderClicked = true;
    if (this.profileForm.controls.acceptTAC.value) {
      if (status === 'buy') {
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
  scrollDiv(): void {
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
