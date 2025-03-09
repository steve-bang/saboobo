
import { PageContainer } from "@/components";
import { AddNewAddressPage } from "@/modules/profile/address/add-new-address/components";
import React, { FC} from "react";


export const AddNewAddress: FC = () => {

    return (
        <PageContainer withBottomNav>
            <AddNewAddressPage />
        </PageContainer>
    )
}