import { NgModule } from '@angular/core';
import { AEDcurrencyPipe } from './aed-currency.pipe';

@NgModule({
  declarations: [AEDcurrencyPipe],
  exports: [AEDcurrencyPipe]
})
export class PipesModule { }
