import { IAddress, IUserAddress } from "@/types/address";
import React, { FC } from "react";
import { Box, Header, Icon, Text } from "zmp-ui";

export const AddressItem: FC<{ address: IUserAddress }> = ({ address }) => {


    return (
        <Box className="bg-white p-4 mb-4 rounded-md shadow-sm flex flex-col gap-1">
            <div className="flex items-center gap-1">
                <div>{address.contactName}</div>
                |
                <div>{address.phoneNumber}</div>
            </div>
            <Text className="text-gray-500"
            >{address.fullAddress}</Text>
        </Box>
    )
}
