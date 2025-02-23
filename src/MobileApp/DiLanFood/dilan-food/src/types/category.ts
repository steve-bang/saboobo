import { IBaseType } from "./base";

export interface Category {
  id: string;
  name: string;
  icon: string;
}


export interface ICategoryType extends IBaseType {
  name: string;
  code: string;
  totalProducts: number;
  iconUrl?: string;
  description?: string;
}