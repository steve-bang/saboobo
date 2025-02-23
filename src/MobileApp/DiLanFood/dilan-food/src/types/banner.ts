import { IBaseType } from "./base"

export interface IBannerType extends IBaseType {
    name: string;
    imageUrl: string;
    link?: string;
    order: number;
}