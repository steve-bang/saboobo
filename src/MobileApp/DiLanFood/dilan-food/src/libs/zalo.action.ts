import { PRIVATE_KEY, SECRET_KEY } from "@/constants/common";
import { IOrderType } from "@/types/order";
import CryptoJS from "crypto-js";
import { Payment } from "zmp-sdk";


const endpoint = "https://graph.zalo.me/v2.0/me/info";

// curl --location --request GET <https://graph.zalo.me/v2.0/me/info>
// --header access_token: <user_access_token>
// --header **code: <your token>**
// --header **secret_key: <your zalo app secret key>**
export const getCurrentLocation = async (accessToken: string, code: string) => {

    try {

        const response = await fetch(endpoint, {
            method: 'GET',
            headers: {
                access_token: accessToken,
                code: code,
                secret_key: SECRET_KEY
            }
        });

        if (!response.ok) {
            throw new Error(`Failed to fetch location: ${response.statusText}`);
        }

        const location = await response.json();

        return location;
    }
    catch (error) {
        console.error("Error fetching location:", error);
        return null;
    }
}

export const createOrder = async (order: IOrderType, fail: any) => {

    const mac = createMac(order);

    const { orderId } =  await Payment.createOrder({
        desc: order.desc,
        amount: order.amount,
        item: order.item.map((item) => ({
            id: item.id,
            amount: item.amount
        })),
        extradata: order.extradata,
        method: order.method,
        mac: mac,
        fail: fail
    });

    return orderId;

}

export const createMac = (params: any) => {

    console.log("Order data:", JSON.stringify(params));

    const dataMac = Object.keys(params)
        .sort() // Sort the keys of the 'params' object in ascending lexicographical order
        .map(
            (key) =>
                `${key}=${typeof params[key] === "object"
                    ? JSON.stringify(params[key])
                    : params[key]
                }`,
        ) // Map to an array of strings like 'key=value'
        .join("&"); // Join the array into a single string separated by "&"

    console.log("Data MAC:", dataMac);

    console.log("Private key:", PRIVATE_KEY);

    const mac = CryptoJS.HmacSHA256(dataMac, PRIVATE_KEY).toString(CryptoJS.enc.Hex);

    console.log("MAC:", mac);

    return mac;
}


