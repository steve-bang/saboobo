'use server'

import { BannerCreateParams, BannerUpdateParams, IBannerType } from "@/types/Banner";
import { IResponseApiType } from "@/types/Common";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as string;

export const listBannersByMerchantId = async (merchantId: string) => {

    const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/banners`);

    if (!response.ok) throw new Error("Failed to fetch banners");

    const responseData: IResponseApiType<IBannerType[]> = await response.json();

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

    const responseData: IResponseApiType<string> = await response.json();

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

    const responseData: IResponseApiType<boolean> = await response.json();

    return responseData.data;
};

export const deleteBanner = async (merchantId: string, id: string) => {

    const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/banners/${id}`, {
        method: "DELETE",
    });

    if (!response.ok) throw new Error("Failed to delete banner");

    const responseData: IResponseApiType<boolean> = await response.json();

    return responseData.data;
};
