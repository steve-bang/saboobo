import { IBaseType } from "./Common";

export interface ICategoryType extends IBaseType  {
    merchantId: string;
    code: string;
    name: string;
    description: string;
    iconUrl: string;
    totalProduct: number;
    createdDate: string;
}

export interface CreateCategoryParams {
    code: string | null;
    name: string;
    description: string;
    iconUrl: string | null;
}

export interface UpdateCategoryParams extends CreateCategoryParams {

}