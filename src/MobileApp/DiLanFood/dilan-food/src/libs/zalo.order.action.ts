import { PRIVATE_KEY, SECRET_KEY } from "@/constants/common";
import { IOrderType } from "@/types/order";
import CryptoJS from "crypto-js";
import { Payment } from "zmp-sdk";

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

    const mac = CryptoJS.HmacSHA256(dataMac, PRIVATE_KEY).toString(CryptoJS.enc.Hex);

    return mac;
}


