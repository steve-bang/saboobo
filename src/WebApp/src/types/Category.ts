import { BaseType } from "./Common";

export interface CategoryType extends BaseType  {
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