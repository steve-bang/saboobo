'use server'

import { CategoryType, CreateCategoryParams, UpdateCategoryParams } from "@/types/Category";
import { ResponseApiType } from "@/types/Common";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as String;

export const listCategoryByMerchantId = async (merchantId : string) => {

  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories`);
  
    if (!response.ok) throw new Error("Failed to fetch categories");

    var responseData : ResponseApiType<CategoryType[]> = await response.json();

    return responseData.data;
};

export const getCategoryById = async (merchantId : string, id: string) => {
  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories/${id}`);

    if (!response.ok) throw new Error("Failed to fetch category");

    var data : ResponseApiType<CategoryType> = await response.json();

    return data.data;

};

export const createCategory = async (merchantId : string,  data : CreateCategoryParams) => {
  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!response.ok) throw new Error("Failed to create category");

  var responseData : ResponseApiType<string> = await response.json();
  
  return responseData.data;
};

export const updateCategory = async (merchantId : string, id: string, data : UpdateCategoryParams) => {
  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  console.log(response);

  if (!response.ok) throw new Error("Failed to update category");

  var responseData : ResponseApiType<CategoryType> = await response.json();

    return responseData.data;
};

export const deleteCategory = async (merchantId : string, id: string) => {

  const response = await fetch(`${apiUrl}/api/v1/merchants/${merchantId}/categories/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) throw new Error("Failed to delete category");

  var responseData : ResponseApiType<boolean> = await response.json();

    return responseData.data;
};
