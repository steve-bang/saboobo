import { PageContainer } from "@/components";
import { MyAddressPage } from "@/modules/profile/address/my-address/components";
import React, { FC } from "react";

export const MyAddress: FC = () => {

    return (
        <PageContainer withBottomNav>
            <MyAddressPage />
        </PageContainer>
    )
}