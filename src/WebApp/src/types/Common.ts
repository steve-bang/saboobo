export interface IBaseType {
    id: string;
}

export interface IResponseApiType<T> {
    success: boolean;
    httpStatus: number;
    data: T;
}

export interface IFileType {
    file: File;
    url: string;
}