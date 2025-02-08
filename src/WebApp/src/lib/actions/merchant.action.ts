'use server'

import { MerchantForm, IMerchantType } from "@/types/Merchant";
import { GetAccessTokenFromCookie as getAccessTokenFromCookie } from "../HttpUtils";
import { IResponseApiType } from "@/types/Common";

const apiUrl = process.env.NEXT_PUBLIC_API_URL as String;

export const getMerchantsByUserLogged = async () => {
    try {

        const accessTokenFromCookie = await getAccessTokenFromCookie();

        const response = await fetch(`${apiUrl}/api/v1/merchants`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${accessTokenFromCookie}`
            }
        })

        if (!response.ok) {
            console.error(response)
            throw new Error('Failed to get merchants')
        }

        const result: IResponseApiType<IMerchantType> = await response.json()

        return result.data;
    } catch (error) {
        console.error(error)
        throw new Error('Failed to get merchants')
    }
}

export const updateMerchant = async ( id : string, merchant: MerchantForm) => {
    try {
        const accessTokenFromCookie = await getAccessTokenFromCookie();

        const response = await fetch(`${apiUrl}/api/v1/merchants/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${accessTokenFromCookie}`
            },
            body: JSON.stringify(merchant)
        })

        if (!response.ok) {
            console.error(response)
            throw new Error('Failed to update merchant')
        }

        const result: IResponseApiType<IMerchantType> = await response.json()

        return result.data;
    } catch (error) {
        console.error(error)
        throw new Error('Failed to update merchant')
    }
}