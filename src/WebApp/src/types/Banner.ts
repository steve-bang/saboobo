import { IBaseType } from "./Common";

export interface IBannerType extends IBaseType {
    name: string;
    imageUrl: string;
    link: string;
    order: number;
}

export interface BannerCreateParams {
    name: string;
    imageUrl: string;
    link: string;
}

export interface BannerUpdateParams extends IBaseType {
    name: string;
    imageUrl: string;
    link: string;
}