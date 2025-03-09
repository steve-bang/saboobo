import { SERVER_URL } from "@/constants/common";
import { IResponseApiType } from "@/types/base";
import { IUserAuthType, IUserZaloType } from "@/types/user";


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
    registerParams: IRegisterParams,
): Promise<any> => {

    const response = await fetch(`${SERVER_URL}/api/v1/auth/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(registerParams),
    });



    const apiResult: IResponseApiType<IUserAuthType> = await response.json();

    return apiResult;
}

export const login = async (
    loginParams: ILoginParams
): Promise<IResponseApiType<IUserAuthType>> => {

    const response = await fetch(`${SERVER_URL}/api/v1/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(loginParams),
    });


    const apiResult: IResponseApiType<IUserAuthType> = await response.json();

    return apiResult;
}

export const loginWithZalo = async (
    merchantId: string,
    userInfo: IUserZaloType
): Promise<IResponseApiType<IUserAuthType>> => {

    // Init payload
    const payload = {
        merchantId: merchantId,
        metadata: JSON.stringify(userInfo),
        provider: "Zalo"
    }

    // Call API
    const response = await fetch(`${SERVER_URL}/api/v1/auth/login/external-provider`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
    });

    const apiResult: IResponseApiType<IUserAuthType> = await response.json();

    // Return result
    return apiResult;
}