import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactusComponent } from './contactus/contactus.component';
import { ProductsComponent } from './products/products.component';
import { StoryComponent } from './story/story.component';
import {ProductdetailsComponent} from './productdetails/productdetails.component';

const routes: Routes = [
  { path: '', component: HomeComponent  },
  { path: 'products', component: ProductsComponent  },
  { path: 'contactus', component: ContactusComponent },
  { path: 'story', component: StoryComponent },
  { path: 'productdetails', component: ProductdetailsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
