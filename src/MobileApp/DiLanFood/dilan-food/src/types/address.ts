import { IBaseType } from "./base";

export interface IAddress extends IBaseType {
    name: string;
    phoneNumber: string;
    address: string;
    country: string; // Vietnam
    province: string; // Tỉnh/Thành phố
    district: string; // Quận/Huyện
    ward: string; // Phường/Xã
    isDefault: boolean;
}


export interface IUserAddress extends IBaseType {
    contactName: string;
    phoneNumber: string;
    addressLine1: string;
    addressLine2: string;
    city: string;
    state: string;
    country: string;
    isDefault: boolean;
    fullAddress: string;
    updatedAt: string;
    createdAt: string;
}

export interface ICity {
    code: string;
    name: string;
    displayName: string;
}

// export interface IDistrict {
//     name: string;
//     code: number;
//     codename: string;
//     short_codename: string;
//     wards: IWard[];
// }

// export interface IWard {
//     name: string;
//     code: number;
//     codename: string;
//     short_codename: string;
// }