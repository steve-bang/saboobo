import { IBaseType } from "./Common";

export interface IMediaType extends IBaseType
{
    name: string;
    size: number;
    contentType: string;
    type: string;
    url: string;
    createdAt: string;
}
