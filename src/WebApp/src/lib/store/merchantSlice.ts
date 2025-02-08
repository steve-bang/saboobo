
"use client"

import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IMerchantType } from "@/types/Merchant";

interface MerchantState {
    merchant: IMerchantType;
}

const initialState: MerchantState = {
    merchant: {
        id: "",
        userId: "",
        name: "",
        description: "",
        emailAddress: "",
        phoneNumber: "",
        address: "",
        logoUrl: "",
        coverUrl: "",
        website: "",
        oaUrl: "",
        createdAt: "",
    }
};

// Create the slice
const merchantSlice = createSlice({
    name: "merchant",
    initialState,
    reducers: {
        setMerchant: (state, action: PayloadAction<IMerchantType>) => {
            state.merchant = action.payload;
        },
        clearMerchant: (state) => {
            state.merchant = initialState.merchant;
        }
    },
});

// Export actions
export const { setMerchant, clearMerchant } = merchantSlice.actions;

// Export selector for getting merchant data from the state
//export const selectMerchant = (state: { merchant: MerchantState }) => state.merchant.merchant;

// Export the reducer to be used in the store
export const merchantReducer = merchantSlice.reducer;
