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
import { NgxImgZoomService } from 'ngx-img-zoom';
import { ActivatedRoute, Router } from '@angular/router';

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
  email: string;
  productId: string;
  constructor(
    private httpClient: HttpClient,
    public dialog: MatDialog,
    private ngxImgZoom: NgxImgZoomService,
    private route: ActivatedRoute
  ) {}
  product: {
    productCode:'',
    categoryName:'',
    name:'',
    cost:0,
    description:''
  };
  quantity: number=1;
  closeResult: string;
  isRequest: boolean;
  selectedproduct: [];
  imageurl: string;
  enableZoom: Boolean = true;
  showCustomsize:Boolean=false;
  selectedSize:string='';
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
    });
  }
  buyProduct(product) {
    console.log('d' + this.profileForm.value);
    var formData: any = new FormData();
    formData.append('ProductId', product.productId);
    formData.append('Name', this.profileForm.value.name);
    formData.append('Email', this.profileForm.value.cusmail);
    formData.append('ContactNumber', this.profileForm.value.phone);
    formData.append('Address', this.profileForm.value.address);
    formData.append('Message', this.profileForm.value.message);
    console.log('cus' + formData);
    this.httpClient.post(CUSTOMERREQUEST, formData).subscribe(
      (response: any) => {
        console.log(response);
        if (response.data) {
          this.isRequest = false;
          alert('success');
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

  onRequest() {
    this.isRequest = true;
  }

  changeQuantity(stat: any) {
    if (stat == 'increment') this.quantity = ++this.quantity;
    else{
      if(this.quantity!=1){
      this.quantity = --this.quantity;
      }
    } 
  }

  toggleCustomeSize(){
    this.showCustomsize=!this.showCustomsize;
  }

  selectSize(size){
    this.selectedSize=size;
  }
}
