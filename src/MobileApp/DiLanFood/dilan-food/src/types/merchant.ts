import { IBaseType } from "./base";

export interface IMerchantType extends IBaseType {
    name: string;
    address: string;
    emailAddress: string;
    phoneNumber: string;
    logoUrl: string;
    coverUrl: string;
    description: string;
}