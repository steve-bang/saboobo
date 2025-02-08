"use client"


import { IMerchantType } from "@/types/Merchant";
import { createContext, useContext, useEffect, useMemo, useState } from "react";

interface MerchantProviderProps {
    children: React.ReactNode;
}

const merchantDefault: IMerchantType = {
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
};

// Create the context
const MerchantContext = createContext<{
    merchant: IMerchantType;
    setMerchant: (data: IMerchantType) => void;
} | undefined>(undefined);

// Custom hook to use the MerchantContext
export const useMerchantContext = (): {
    merchant: IMerchantType;
    setMerchant: (data: IMerchantType) => void;
} => {
    const context = useContext(MerchantContext);
    if (!context) {
        throw new Error('useMerchantContext must be used within a MerchantContextProvider');
    }
    return context;
};

export const MerchantContextProvider = ({ children }: MerchantProviderProps) => {
    const [merchant, setMerchant] = useState<IMerchantType>(() => {
        const storedMerchant = localStorage.getItem("merchant");
        return storedMerchant ? JSON.parse(storedMerchant) : merchantDefault;
    });

    // Whenever merchant data changes, save it to localStorage
    useEffect(() => {
        if (merchant.id && merchant !== merchantDefault) {
            localStorage.setItem("merchant", JSON.stringify(merchant));
        }
    }, [merchant]);

    const contextValue = useMemo(
        () => ({
            merchant,
            setMerchant,
        }),
        [merchant]
    );

    return (
        <MerchantContext.Provider value={contextValue}>
            {children}
        </MerchantContext.Provider>
    );
};
