import { Pipe, PipeTransform } from '@angular/core';
import { IMAGE_ENDPOINT } from 'src/app/core/constants/constant';

@Pipe({ name: 'mainImageFilter' })
export class MainImageFilterPipe implements PipeTransform {
  transform(images: any[] = []): any {
    const mainImage = images.find(img => img.isMainImage);
    const normalImage = images[0];
    const image = mainImage || normalImage;
    const imageURL = image ? (`${IMAGE_ENDPOINT}${image.imageUrl}`) : 'assets/images/placeholder.png';
    return imageURL;
  }
}
