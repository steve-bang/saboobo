import { IBaseType } from "./Common";

export interface IUserType extends IBaseType {
    name: string | null;
    phoneNumber: string;
    email: string | null;
    avatarUrl: string | null;
    lastLoginAt: string | null;
    createdAt: string;
}