'use server'

import { ICategoryType, CreateCategoryParams } from "@/types/Category";
import { IResponseApiType } from "@/types/Common";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as string;

export const listCategoryByMerchantId = async (merchantId: string) => {

  console.log("Fetching categories...");
  console.log("Merchant ID", merchantId);

  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories`);

  if (!response.ok) throw new Error("Failed to fetch categories");

  const responseData: IResponseApiType<ICategoryType[]> = await response.json();

  return responseData.data;
};

export const getCategoryById = async (merchantId: string, id: string) => {
  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories/${id}`);

  if (!response.ok) throw new Error("Failed to fetch category");

  const data: IResponseApiType<ICategoryType> = await response.json();

  return data.data;

};

export const createCategory = async (merchantId: string, data: CreateCategoryParams) => {
  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Failed to create category");

  const responseData: IResponseApiType<string> = await response.json();

  return responseData.data;
};

export const updateCategory = async (merchantId: string, id: string, data: CreateCategoryParams) => {
  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Failed to update category");

  const responseData: IResponseApiType<ICategoryType> = await response.json();

  return responseData.data;
};

export const deleteCategory = async (merchantId: string, id: string) => {

  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) throw new Error("Failed to delete category");

  const responseData: IResponseApiType<boolean> = await response.json();

  return responseData.data;
};
