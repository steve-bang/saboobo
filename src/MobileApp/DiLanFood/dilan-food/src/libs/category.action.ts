import { SERVER_URL } from "@/constants/common";
import { IResponseApiType } from "@/types/base";
import { ICategoryType } from "@/types/category";


export const getCategories = async ( merchantId: string ): Promise<ICategoryType[] | null> => {
  try {
    const response = await fetch(`${SERVER_URL}/api/v1/merchants/${merchantId}/categories`);

    if (!response.ok) {
      throw new Error(`Failed to fetch categories: ${response.statusText}`);
    }

    const apiResult : IResponseApiType<ICategoryType[]> = await response.json();

    return apiResult.data;

  } catch (error) {
    console.error("Error fetching categories:", error);
    return null;
  }
};