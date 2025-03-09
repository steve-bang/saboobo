import { SERVER_URL } from "@/constants/common";
import { IResponseApiType } from "@/types/base";
import { ICart, IShippingAddress } from "@/types/cart";

export interface ICartItemProps {
    productId: string;
    productName: string;
    productImage: string;
    quantity: number;
    price: number;
    notes?: string;
}

export interface IPlaceOrderProps {
    shippingAddress: IShippingAddress;
    notes: string; // notes for order
    estimatedDeliveryTimeFrom: string; // hh:mm
    estimatedDeliveryTimeTo: string; // hh:mm
    estimatedDeliveryDate: string; // yyyy-mm-dd
}

export const createCart = async (
    accessToken: string
): Promise<IResponseApiType<ICart>> => {

    const resUrl = `${SERVER_URL}/api/v1/carts`;

    const response = await fetch(resUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${accessToken}`
        }
    });

    const apiResult: IResponseApiType<ICart> = await response.json();

    return apiResult;
}



export const addItemToCart = async (
    accessToken: string, cartId: string, item: ICartItemProps
): Promise<IResponseApiType<ICart>> => {

    const payload = [item];

    const response = await fetch(`${SERVER_URL}/api/v1/carts/${cartId}/items`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${accessToken}`
        },
        body: JSON.stringify(payload)
    });

    const apiResult: IResponseApiType<ICart> = await response.json();

    return apiResult;
}

export const updateItemsToCart = async (
    accessToken: string, cartId: string, items: ICartItemProps[]
): Promise<IResponseApiType<ICart>> => {

    const response = await fetch(`${SERVER_URL}/api/v1/carts/${cartId}/items`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${accessToken}`
        },
        body: JSON.stringify(items)
    });

    const apiResult: IResponseApiType<ICart> = await response.json();

    return apiResult;
}

export const removeItemToCart = async (
    accessToken: string, cartId: string, itemIds: string[]
): Promise<IResponseApiType<ICart>> => {

    const payload = {
        cartItemIds: itemIds
    }

    const response = await fetch(`${SERVER_URL}/api/v1/carts/${cartId}/items`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${accessToken}`
        },
        body: JSON.stringify(payload)
    });

    const apiResult: IResponseApiType<ICart> = await response.json();

    return apiResult;
}

export const placeOrder = async (
    accessToken: string, cartId: string, orderInfo: IPlaceOrderProps
): Promise<IResponseApiType<any>> => {

    const response = await fetch(`${SERVER_URL}/api/v1/carts/${cartId}/place-order`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${accessToken}`
        },
        body: JSON.stringify(orderInfo)
    });

    const apiResult: IResponseApiType<any> = await response.json();

    return apiResult;
}
