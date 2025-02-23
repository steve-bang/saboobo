import { profile } from "console";

export const Routes = {
  merchant: {
    page: () => `/`,
    cart: () => `/orders?tab=cart`,
    orders: () => `/orders?tab=orders`,
    info: () => `/info`,
    profile : () => `/profile`,
  },
}
