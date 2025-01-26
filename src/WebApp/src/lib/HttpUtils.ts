import { CookieKey } from "@/constants/CookieKey";
import { cookies } from "next/headers";

export const GetAccessTokenFromCookie = async () => {
    try {
        var accessTokenFromCookie = (await cookies()).get(CookieKey.accessToken);
        
        if (!accessTokenFromCookie) {
            return '';
        }

        return accessTokenFromCookie.value
    }
    catch (error) {
        console.error(error);
    }
}