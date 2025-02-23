import { SERVER_URL } from "@/constants/common";
import { IBannerType } from "@/types/banner";
import { IResponseApiType } from "@/types/base";
import { IMerchantType } from "@/types/merchant";


export const getMerchantById = async (merchantCode: string): Promise<IMerchantType | null> => {
  try {


    const response = await fetch(`${SERVER_URL}/api/v1/merchants/${merchantCode}`);


    if (!response.ok) {
      throw new Error(`Failed to fetch merchant: ${response.statusText}`);
    }

    const apiResult: IResponseApiType<IMerchantType> = await response.json();

    return apiResult.data;

  } catch (error) {
    console.error("Error fetching merchant:", error);
    return null;
  }
};

export const getBanners = async (merchantId: string): Promise<IBannerType[]> => {
  try {
    const response = await fetch(`${SERVER_URL}/api/v1/merchants/${merchantId}/banners`);

    if (!response.ok) {
      throw new Error(`Failed to fetch banners: ${response.statusText}`);
    }

    const apiResult: IResponseApiType<IBannerType[]> = await response.json();

    return apiResult.data;
  } catch (error) {
    console.error("Error fetching banners:", error);
    return [];
  }
}
