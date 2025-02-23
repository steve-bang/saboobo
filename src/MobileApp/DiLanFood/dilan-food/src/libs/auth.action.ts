import { IResponseApiType } from "@/types/base";


const API_URL = import.meta.env.VITE_API_URL; 

export interface IRegisterParams {
    merchantId: string;
    phoneNumber: string;
    password: string;
    confirmPassword: string;
}

export interface ILoginParams {
    merchantId: string;
    phoneNumber: string;
    password: string;
}

export const register = async (
    registerParams: IRegisterParams
): Promise<object | null> => {
    try {
        const response = await fetch(`${API_URL}/api/v1/auth/register`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(registerParams),
        });

        if (!response.ok) {
            throw new Error(`Failed to register: ${response.statusText}`);
        }

        const apiResult: IResponseApiType<object> = await response.json();

        return apiResult.data;

    } catch (error) {
        console.error("Error registering:", error);
        return null;
    }
}

export const login = async (
    loginParams: ILoginParams
): Promise<object | null> => {
    try {
        const response = await fetch(`${API_URL}/api/v1/auth/login`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(loginParams),
        });

        if (!response.ok) {
            throw new Error(`Failed to login: ${response.statusText}`);
        }

        const apiResult: IResponseApiType<object> = await response.json();

        return apiResult.data;

    } catch (error) {
        console.error("Error logging in:", error);
        return null;
    }
}