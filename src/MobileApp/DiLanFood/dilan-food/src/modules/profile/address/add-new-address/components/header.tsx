import React, { FC } from "react";
import { Header, Icon, Text } from "zmp-ui";

export const HeaderAddAddress: FC = () => {
    return (
        <Header
            backIcon={<Icon icon="zi-arrow-left" className="text-black" />}
            title={
                (
                    <Text.Title size="small">Địa chỉ mới</Text.Title>
                ) as unknown as string
            }
        />
    )
}
