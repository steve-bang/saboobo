import { IBaseType } from "./Common";

export interface IProductType extends IBaseType {
    merchantId: string;
    categoryId: string;
    name: string;
    sku: string | null;
    description: string;
    price: number;
    urlImage: string;
    createdDate: string;
}

export interface IToppingType extends IBaseType {
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
    urlImage: string | null;
    toppings: IToppingType[];
}