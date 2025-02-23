
export interface IOrderType {
    desc: string;
    item: IOrderItem[];
    method: string;
    amount : number;
    extradata : string;
}

export interface IOrderMethod {
    id: string;
    isCustom : boolean;
}

export interface IOrderItem {
    id: string;
    amount: string;
}