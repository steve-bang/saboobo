"use client"

import { combineReducers, configureStore } from "@reduxjs/toolkit";
import { merchantReducer } from "./merchantSlice";
import { userReducer } from "./userSlice";
import storage from 'redux-persist/lib/storage';
import { persistReducer, persistStore } from "redux-persist";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { createSelector } from 'reselect';

// Check if we are in the browser
const isBrowser = typeof window !== "undefined";

// Combine all reducers
const rootReducer = combineReducers({ 
  user: userReducer,
  merchant: merchantReducer,
})

const persistConfig = {
  key: "root",
  storage: storage,
  whitelist: ['user', 'merchant'],
};

// Only persist if in the client-side environment
const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
  reducer: persistedReducer,
  devTools: process.env.NODE_ENV !== "development",
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [
          "persist/PERSIST", 
          "persist/REHYDRATE",
          "persist/PURGE",
          "persist/FLUSH",
        ],
      },
    }),
});

// Infer the type of makeStore
export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch

export const persistor = persistStore(store);
export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

