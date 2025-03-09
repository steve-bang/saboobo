import { create } from "zustand";
import { IUserAddress } from "@/types/address";

interface AddressState {
    items: IUserAddress[] | null; // Array of IUserAddress
    setAddresses: (address: IUserAddress[]) => void; // Function to update the address
    clearAddress: () => void; // Function to clear the address
}

const useAddress = create<AddressState>((set) => ({
    items: null, // Initialize with an empty array
    setAddresses: (address) => set({ items: address }), // Set the address
    clearAddress: () => set({ items: [] }),
}));

export default useAddress;