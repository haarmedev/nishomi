import { NgModule } from '@angular/core';
import { AEDcurrencyPipe } from './aed-currency.pipe';
import { MainImageFilterPipe } from './main-image-filter.pipe';

@NgModule({
  declarations: [AEDcurrencyPipe, MainImageFilterPipe],
  exports: [AEDcurrencyPipe, MainImageFilterPipe]
})
export class PipesModule { }
