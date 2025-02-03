'use server'

import { ResponseApiType } from "@/types/Common";
import { CreateProductParams, ProductType } from "@/types/Product";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as String;

export const listProduct = async () => {
  const response = await fetch(`${apiUrl}/api/v1/products`);
  
    if (!response.ok) throw new Error("Failed to fetch products");

    var responseData : ResponseApiType<ProductType[]> = await response.json();

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

  var responseData : ResponseApiType<string> = await response.json();

  return responseData.data;
}