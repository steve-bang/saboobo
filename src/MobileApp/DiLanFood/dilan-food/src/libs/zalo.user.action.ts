import { getAccessToken, getPhoneNumber, getUserInfo } from "zmp-sdk/apis";
import { getZaloDataByAccessTokenAndCode } from "./zalo.location.action";
import { IUserZaloType } from "@/types/user";


export const getPhoneNumberUser = async ()
    : Promise<string | undefined> => {
    try {
        const { token } = await getPhoneNumber();

        if (token) {

            const accessToken = await getAccessToken({});

            const phone = await getZaloDataByAccessTokenAndCode(accessToken, token);

            return phone.data.number;
        }
    }
    catch (error) {
        console.error("Error fetching phone number:", error);
    }

    return undefined;
}

/**
 * Get current user info from Zalo
 * @returns The IUserZaloType object
 */
export const getCurrentUserInfoZalo = async ()
    : Promise<IUserZaloType | undefined> => {
    try {
        const { userInfo } = await getUserInfo({
            avatarType: "small",
            autoRequestPermission: true
        });

        console.log('userInfo', userInfo);

        const phoneNumber = await getPhoneNumberUser();

        console.log('phoneNumber', phoneNumber);

        if (!userInfo || !phoneNumber) {
            throw new Error("Failed to get user info");
        }

        return {
            id: userInfo.id,
            name: userInfo.name,
            avatar: userInfo.avatar,
            phoneNumber: phoneNumber
        };
    }
    catch (error) {
        throw new Error("Failed to get user info");
    }
}

