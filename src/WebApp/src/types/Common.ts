export interface BaseType {
    id: string;
}

export interface ResponseApiType<T> {
    success: boolean;
    httpStatus: number;
    data: T;
}