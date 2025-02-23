import { ListItem } from "@/components/list-item";
import { SECRET_KEY } from "@/constants/common";
import { getCurrentLocation } from "@/libs/zalo.action";
import React, { FC } from "react";
import { getAccessToken, getLocation } from "zmp-sdk/apis";

export const LocationPicker: FC = () => {

    const onClick = () => {
        getLocation({
            success: async (data) => {
                let { token } = data;

                console.log('token location', token);

                if (token) {
                    getAccessToken({
                        success: async (accessToken) => {
                            console.log('access Token', accessToken);

                            const locationActual = await getCurrentLocation(accessToken, token);

                            console.log('locationActual', locationActual);

                        },
                        fail: (error) => {
                            console.log('error get access token', error);
                        },
                    });
                }


            },
            fail: (error) => {
                // xử lý khi gọi api thất bại
                console.log('error get location', error);
            },
        });
    }

    return (
        <ListItem
            title="Chọn địa chỉ"
            subtitle="Chọn địa chỉ nhận hàng"
            onClick={onClick}
        />
    );
};
