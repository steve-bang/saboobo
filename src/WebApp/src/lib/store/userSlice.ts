
"use client"

import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IUserType } from "@/types/User";

interface UserState {
    data: IUserType | null;
}

const initialState: UserState = {
    data: null,
};

// Create the slice
const userSlice = createSlice({
    name: "user",
    initialState,
    reducers: {
        setUser: (state, action: PayloadAction<IUserType>) => {
            state.data = action.payload;
        },
        clearUser: (state) => {
            state.data = null;
        }
    },
});

// Export actions
export const { setUser, clearUser } = userSlice.actions;
;

// Export the reducer to be used in the store
export const userReducer = userSlice.reducer;
