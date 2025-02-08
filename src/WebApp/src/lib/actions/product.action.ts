'use server'

import { IResponseApiType } from "@/types/Common";
import { CreateProductParams, IProductType } from "@/types/Product";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as String;

export const listProduct = async () => {
  const response = await fetch(`${apiUrl}/api/v1/products`);

  if (!response.ok) throw new Error("Failed to fetch products");

  var responseData: IResponseApiType<IProductType[]> = await response.json();

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

  var responseData: IResponseApiType<string> = await response.json();

  return responseData.data;
}

export const getProductById = async (id: string) => {
  const response = await fetch(`${apiUrl}/api/v1/products/${id}`);

  if (!response.ok) throw new Error("Failed to fetch product");

  var responseData: IResponseApiType<IProductType> = await response.json();

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

  var responseData: IResponseApiType<string> = await response.json();

  return responseData.data;
}

export const deleteProduct = async (id: string) => {
  const response = await fetch(`${apiUrl}/api/v1/products/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) throw new Error("Failed to delete product");

  var responseData: IResponseApiType<boolean> = await response.json();

  return responseData.data;
}