import { environment } from 'src/environments/environment';

export const API_ENDPOINT = environment.apiBaseUrl;
export const IMAGE_ENDPOINT = environment.imageBaseUrl;

export const CATEGORIESAPI = `${API_ENDPOINT}/api/Category`;
export const FEATUREDPRODUCTSAPI = `${API_ENDPOINT}/api/Product/FeaturedProducts`;
export const CATEGORYPRODUCTS = `${API_ENDPOINT}/api/Category/CategoryProducts`;
export const CUSTOMERREQUEST = `${API_ENDPOINT}/api/Product/SaveRequest`;
export const PRODUCTDETAILS = `${API_ENDPOINT}/api/Product`;

export const META = {
  HOME: { TITLE: 'Designer Abaya | Online Abaya | Nishomi Abayas', DESC: 'Nishomi Designer Abayas brings the Trendiest Modest Wear at affordable prices. Shop stylish, elegant Abaya designs collection from wide range of variety.' },
  PRODUCTS: { TITLE: 'Our Products | Hanayen abayas | abaya shop | Nishomi Abayas', DESC: 'Nishomi Abayas shop combines original designs and meticulously chosen fabric to instantly speak the language of elegance, luxury, and modest fashion.' },
  CONTACT_US: { TITLE: 'Stay in touch | online abaya in uae| designer abaya | Nishomi Abayas', DESC: 'At Nishomi abayas you can shop stylish, elegant abaya designs collection via online and offline. We deliver the product worldwide.' },
  STORY: { TITLE: 'Our story | Dubai abaya shop | Abayas | Nishomi Abayas', DESC: 'Nishomi abayas is one of the best abaya shop in Dubai. Everything we do begin with the customer and the customer offering.' },
  BLOGS: { TITLE: 'Abayas in Dubai | Hanayen abayas | Nishomi Abayas', DESC: 'Nishomi Abayas in Dubai provides latest Collection of Abayas for Women vai online and offline. Buy Top varieties of Hanayen abayas, party wear abayas from Nishomi.' },
  TERMS: { TITLE: 'Abaya UAE | Online abaya in UAE | Nishomi Abayas', DESC: 'Buy wide range of premium quality designer abayas from Nishomi abayas UAE. We also provide online services and deliver worldwide.' },
  PRIVACY: { TITLE: 'Privacy Policy | abaya uae | dubai online abaya | Nishomi Abayas', DESC: 'Latest Collection of Abayas for Women is available online at Nishomi Designer Abaya shop in uae.' },
};
