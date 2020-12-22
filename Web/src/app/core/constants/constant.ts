import { environment } from "src/environments/environment";

export const API_ENDPOINT = environment.apiBaseUrl;
export const IMAGE_ENDPOINT = environment.imageBaseUrl;

export const CATEGORIESAPI = `${API_ENDPOINT}/api/Category`;
export const FEATUREDPRODUCTSAPI = `${API_ENDPOINT}/api/Product/FeaturedProducts`;
export const CATEGORYPRODUCTS = `${API_ENDPOINT}/api/Category/CategoryProducts`;
export const CUSTOMERREQUEST = `${API_ENDPOINT}/api/Product/SaveRequest`;
export const PRODUCTDETAILS = `${API_ENDPOINT}/api/Product`;
