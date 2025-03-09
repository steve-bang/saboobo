import { PageContainer } from "@/components";
import { PickAddressPage } from "@/modules/profile/address/pick-address";
import React, { FC } from "react";

export const PickAddress: FC = () => {

    return (
        <PageContainer withBottomNav>
            <PickAddressPage />
        </PageContainer>
    )
}