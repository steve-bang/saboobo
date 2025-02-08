'use server'

import { BannerCreateParams, BannerUpdateParams, IBannerType } from "@/types/Banner";
import { ICategoryType, CreateCategoryParams, UpdateCategoryParams } from "@/types/Category";
import { IResponseApiType } from "@/types/Common";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as String;

export const listBannersByMerchantId = async (merchantId: string) => {

    const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/banners`);

    if (!response.ok) throw new Error("Failed to fetch banners");

    var responseData: IResponseApiType<IBannerType[]> = await response.json();

    return responseData.data;
};


export const createBanner = async (merchantId: string, data: BannerCreateParams) => {

    const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/banners`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    });

    if (!response.ok) throw new Error("Failed to create banner");

    var responseData: IResponseApiType<string> = await response.json();

    return responseData.data;
};

export const updateBanner = async (merchantId: string, data: BannerUpdateParams[]) => {
    const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/banners`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    });


    if (!response.ok) throw new Error("Failed to update banner");

    var responseData: IResponseApiType<boolean> = await response.json();

    return responseData.data;
};

export const deleteBanner = async (merchantId: string, id: string) => {

    const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/banners/${id}`, {
        method: "DELETE",
    });

    if (!response.ok) throw new Error("Failed to delete banner");

    var responseData: IResponseApiType<boolean> = await response.json();

    return responseData.data;
};
