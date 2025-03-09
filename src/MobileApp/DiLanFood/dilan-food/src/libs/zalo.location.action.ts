import { SECRET_KEY } from "@/constants/common";

const endpoint = "https://graph.zalo.me/v2.0/me/info";

// curl --location --request GET <https://graph.zalo.me/v2.0/me/info>
// --header access_token: <user_access_token>
// --header **code: <your token>**
// --header **secret_key: <your zalo app secret key>**
export const getZaloDataByAccessTokenAndCode = async (accessToken: string, code: string) => {

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