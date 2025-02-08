'use server'

import { CookieKey } from "@/constants/CookieKey";
import { cookies } from "next/headers";
import { GetAccessTokenFromCookie } from "../HttpUtils";
import { IUserType } from "@/types/User";
import { IResponseApiType } from "@/types/Common";

export interface SignInParams {
    phoneNumber: string;
    password: string;
}

const apiUrl = process.env.NEXT_PUBLIC_API_URL as String;

export const signIn = async ({ phoneNumber, password }: SignInParams) => {
    try {
      const response = await fetch(`${apiUrl}/api/v1/auth/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          phoneNumber,
          password,
        }),
      })
  
      if (!response.ok) {
        throw new Error('Failed to sign in')
      }
  
      const authResult = await response.json()
  
      // Set the access token in cookies server-side
      ;(await cookies()).set(CookieKey.accessToken, authResult.data.accessToken, {
        expires: new Date(authResult.data.expiresIn),
        path: '/',
        sameSite: 'strict',
        httpOnly: true,
        secure: true,
      })
  
      // Return the result so the client can process it
      return authResult
    } catch (error) {
      throw new Error('Authentication failed')
    }
  }

export const SignOut = async () => {
    try {

        var accessTokenFromCookie = (await cookies()).get(CookieKey.accessToken);

        if (!accessTokenFromCookie) {
            return false;
        }

        (await cookies()).delete(CookieKey.accessToken);

        return true;
    }
    catch (error) {
        console.error(error);
    }
}

export const GetCurrentUser = async () => {
    try {
        return await GetUserById('me');
    }
    catch (error) {
        console.error(error);
    }
}

export const GetUserById = async (id: string) => {
    try {

        var accessTokenFromCookie = await GetAccessTokenFromCookie();

        const response = await fetch(`${apiUrl}/api/v1/users/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${accessTokenFromCookie}`
            }
        });



        if (!response.ok) {
            throw new Error('Failed to get current user');
        }

        var responseData : IResponseApiType<IUserType> = await response.json();

        return responseData.data;
    }
    catch (error) {
        console.error(error);
    }
}