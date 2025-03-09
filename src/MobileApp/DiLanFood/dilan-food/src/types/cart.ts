import { IBaseType } from "./base";
import { Product } from "./product";

export type SelectedOptions = Record<string, string | string[]>;

export interface CartItem {
  product: Product;
  options: SelectedOptions;
  quantity: number;
}

export type Cart = CartItem[];

///////

export interface IShippingAddress {
  name: string; // name of the recipient
  phoneNumber: string; // phone number
  addressDetail : string; // address detail (e.g. street, building, etc.)
}


export interface ICart extends IBaseType {
  customerId : string;
  totalPrice : number;
  createdAt : string;
  updatedAt : string;
  items : ICartItem[] | [];
}

export interface ICartItem extends IBaseType {
  productId : string;
  productName : string;
  productImage : string;
  quantity : number;
  unitPrice : number;
  totalPrice : number;
  notes : string;
}