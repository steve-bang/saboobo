'use server'

import { IResponseApiType } from "@/types/Common";
import { CreateProductParams, IProductType } from "@/types/Product";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as string;

export interface ProductFilterParams {
  merchantId : string;
  categoryId ?: string;
  keyword ?: string;
  priceFrom ?: number;
  priceTo ?: number;
}

export const listProduct = async (
  filterParams: ProductFilterParams
) => {
  // Convert filterParams to a record of strings for URLSearchParams
  const queryObject: Record<string, string> = Object.fromEntries(
    Object.entries(filterParams)
      .filter(([, value]) => value !== undefined) // Remove undefined values
      .map(([key, value]) => [key, String(value)]) // Convert values to string
  );

  const queryString = new URLSearchParams(queryObject).toString();
  const urlRequest = `${apiUrl}/api/v1/products?${queryString}`;

  const response = await fetch(urlRequest);

  if (!response.ok) throw new Error("Failed to fetch products");

  const responseData: IResponseApiType<IProductType[]> = await response.json();

  return responseData.data;
};



export const createProduct = async (product: CreateProductParams) => {
  const response = await fetch(`${apiUrl}/api/v1/products`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(product),
  });

  if (!response.ok) throw new Error("Failed to create product");

  const responseData: IResponseApiType<string> = await response.json();

  return responseData.data;
}

export const getProductById = async (id: string) => {
  const response = await fetch(`${apiUrl}/api/v1/products/${id}`);

  if (!response.ok) throw new Error("Failed to fetch product");

  const responseData: IResponseApiType<IProductType> = await response.json();

  return responseData.data;
}

export const updateProduct = async (id: string, product: CreateProductParams) => {
  const response = await fetch(`${apiUrl}/api/v1/products/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(product),
  });

  if (!response.ok) throw new Error("Failed to update product");

  const responseData: IResponseApiType<string> = await response.json();

  return responseData.data;
}

export const deleteProduct = async (id: string) => {
  const response = await fetch(`${apiUrl}/api/v1/products/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) throw new Error("Failed to delete product");

  const responseData: IResponseApiType<boolean> = await response.json();

  return responseData.data;
}