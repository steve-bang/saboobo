import { IUserAuthType } from '@/types/user';
import { create } from 'zustand'
import { persist } from 'zustand/middleware';
import { storage } from '@/utils/storage'

export interface IUserStore {
    accessToken: string | null;
    expiresIn: Date | null;
    userId: string | null;
    isAuthenticated: boolean;

    login: (userData: IUserAuthType) => void;
    logout: () => void;
}

export const useUserStore = create(
    persist<IUserStore>(
      (set) => ({
        accessToken: null,
        expiresIn: null,
        userId: null,
        isAuthenticated: false,
  
        login: (userData : IUserAuthType) => set({
          accessToken: userData.accessToken,
          expiresIn: userData.expiresIn,
          userId: userData.userId,
          isAuthenticated: true,
        }),
  
        logout: () => set({
          accessToken: null,
          expiresIn: null,
          userId: null,
          isAuthenticated: false,
        })
      }),
      {
        name: 'user-storage', // unique name
        storage: storage, // (optional) by default the 'localStorage' is used
      }
    )
  );
  