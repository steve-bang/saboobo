import React, { FC } from "react";
import { Header } from "zmp-ui";

export const HeaderMyAddress: FC = () => {
    return (
        <Header title="Địa chỉ của Tôi" className="text-white bg-primary" showBackIcon={true} />
    )
}
