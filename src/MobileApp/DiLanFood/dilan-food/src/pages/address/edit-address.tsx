import { HeaderEditAddress } from "@/modules/profile/address/edit-address/components/header";
import React, { FC } from "react";
import { Page } from "zmp-ui";

export const EditAddress: FC = () => {
    return (
        <Page className="flex flex-col justify-center" >
            <HeaderEditAddress />
        </Page>
    )
}