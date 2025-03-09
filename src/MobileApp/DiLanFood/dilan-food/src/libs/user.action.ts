import { SERVER_URL } from "@/constants/common";
import { IUserAddress } from "@/types/address";
import { IResponseApiType } from "@/types/base";

export interface IUserAddressProps {
    contactName: string;
    phoneNumber: string;
    addressLine1: string;
    addressLine2: string;
    city: string;
    state: string;
    country: string;
    isDefault: boolean;
}

export const addAddress = async (userId: string, address: IUserAddressProps) => {

    const url = `${SERVER_URL}/api/v1/users/${userId}/addresses`;

    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(address)
    });

    const newAddress: IResponseApiType<string> = await response.json();

    return newAddress.data;
}

export const getAddresses = async (userId: string) => {

    const url = `${SERVER_URL}/api/v1/users/${userId}/addresses`;

    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const newAddress: IResponseApiType<IUserAddress[]> = await response.json();

    console.log('response getAddress', newAddress);

    return newAddress.data;
}

export const deleteAddresses = async (userId: string, addressId: string) => {
    const response = await fetch(`${SERVER_URL}/api/v1/users/${userId}addresses/${addressId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        throw new Error(`Failed to add address: ${response.statusText}`);
    }
}