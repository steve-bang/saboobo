import { MyAddress } from "@/pages/address/my-address";
import { profile } from "console";

export const Routes = {
  merchant: {
    page: () => `/`,
    cart: () => `/orders?tab=cart`,
    orders: () => `/orders?tab=orders`,
    info: () => `/info`,
    profile : () => `/profile`,
    signIn: () => `/sign-in`,
  },
  address : {
    myAddress: () => `/my-address`,
    editAddress: () => `/edit-address`,
    addNewAddress: () => `/add-new-address`,
    pickAddress: () => `/pick-address`,
  },
  auth : {
    signIn: () => `/sign-in`,
    signUp: () => `/sign-up`,
  }
}
