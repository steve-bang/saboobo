
export interface BaseType {
    id: string;
}

export interface ResponseApiType<T> {
    success: boolean;
    httpStatus: number;
    data: T;
}

export interface MerchantType extends BaseType {
    userId: string;
    name: string;
    description: string | null;
    emailAddress: string | null;
    phoneNumber: string;
    address: string | null;
    logoUrl: string | undefined;
    coverUrl: string | undefined;
    website: string | undefined;
    oaUrl: string | undefined;
    createdAt: string;
}

export interface MerchantForm {
    name: string;
    description: string | null;
    emailAddress: string | null;
    phoneNumber: string | null;
    address: string | null;
    logoUrl: string | undefined;
    coverUrl: string | undefined;
    website: string | undefined;
    oaUrl: string | undefined;
}