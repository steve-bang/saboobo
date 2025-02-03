import { BaseType } from "./Common";

export interface ProductType extends BaseType {
    merchantId: string;
    categoryId: string;
    name: string;
    sku: string | null;
    description: string;
    price: number;
    urlImage: string;
    createdDate: string;
}

export interface ToppingType extends BaseType {
    name: string;
    price: number;
}

export interface CreateProductParams {
    categoryId: string;
    merchantId: string;
    name: string;
    sku: string | null;
    description: string;
    price: number;
    urlImage: string;
    toppings: ToppingType[];
}