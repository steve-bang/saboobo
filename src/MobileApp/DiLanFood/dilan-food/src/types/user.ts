
export interface IUserAuthType {
    userId : string;
    refreshToken: string;
    accessToken: string;
    expiresIn: Date;
}

/**
 * Prepare the user of the zalo data
 */
export interface IUserZaloType {
    id: string;
    name: string;
    avatar: string;
    phoneNumber : string;
}