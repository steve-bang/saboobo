import { SERVER_URL } from "@/constants/common";
import { IResponseApiType } from "@/types/base";
import { IProductType } from "@/types/product";


export interface IProductFilterParams {
    merchantId: string;
    categoryId?: string;
    keyword?: string;
}

export const getProducts = async (
    filter: IProductFilterParams
): Promise<IProductType[] | null> => {
    try {

        // Build the query string
        const query = new URLSearchParams();
        query.set("merchantId", filter.merchantId);

        if (filter.categoryId) {
            query.set("categoryId", filter.categoryId);
        }

        if (filter.keyword) {
            query.set("keyword", filter.keyword);
        }

        // Fetch the products
        const response = await fetch(`${SERVER_URL}/api/v1/products?${query.toString()}`);


        if (!response.ok) {
            throw new Error(`Failed to fetch products: ${response.statusText}`);
        }

        const apiResult: IResponseApiType<IProductType[]> = await response.json();

        return apiResult.data;

    } catch (error) {
        console.error("Error fetching products:", error);
        return null;
    }
}

export const getProductById = async (
    productId: string
): Promise<IProductType | null> => {
    try {
        const response = await fetch(`${SERVER_URL}/api/v1/products/${productId}`);

        if (!response.ok) {
            throw new Error(`Failed to fetch product: ${response.statusText}`);
        }

        const apiResult: IResponseApiType<IProductType> = await response.json();

        return apiResult.data;

    } catch (error) {
        console.error("Error fetching product:", error);
        return null;
    }
}