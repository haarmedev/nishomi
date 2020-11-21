import { Pipe, PipeTransform } from '@angular/core';
import { formatCurrency, getCurrencySymbol } from '@angular/common';
import { TranslateService } from '@ngx-translate/core';

@Pipe({
  name: 'aedcurrency',
  pure: false,
})
export class AEDcurrencyPipe implements PipeTransform {
  constructor(private translate: TranslateService) {}
  transform(
    value: number,
    currencyCode: string = 'aed',
    display: 'code' | 'symbol' | 'symbol-narrow' | string | boolean = 'symbol',
    digitsInfo: string = '0.2-2',
    locale: string = 'en-US'
  ): string | null {
    return formatCurrency(
      value,
      locale,
      getCurrencySymbol(`${this.translate.instant(currencyCode)} `, 'wide'),
      currencyCode,
      digitsInfo
    );
  }
}
