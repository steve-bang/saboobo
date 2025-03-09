export interface IBaseType {
    id: string;
}

export interface IResponseApiType<T> {
    success: boolean;
    httpStatus: number;
    data?: T;
    error?: IErrorType;

}

export interface IErrorType {
    code: string;
    message: string;
    description: string;
}
