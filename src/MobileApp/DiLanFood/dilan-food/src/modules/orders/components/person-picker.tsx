
import { ListItem } from "@/components/list-item";
import { getCurrentLocation } from "@/libs/zalo.action";
import React, { FC } from "react";
import { getAccessToken, getPhoneNumber } from "zmp-sdk/apis";


export const RequestPersonPickerPhone: FC = () => {


  return (
    <ListItem
      onClick={() =>

        getPhoneNumber({
          success: async (data) => {
            let { token } = data;
            // Xử lý khi gọi api thành công
            console.log('token phone', token);

            if (token) {
              getAccessToken({
                success: async (accessToken) => {
                  console.log('access Token', accessToken);

                  const phone = await getCurrentLocation(accessToken, token);

                  console.log('phone', phone.data.number);

                },
                fail: (error) => {
                  console.log('error get access token', error);
                },
              });
            }


          },
          fail: (error) => {
            // Xử lý khi gọi api thất bại
            console.log(error);
          },
        })
      }
      title="Chọn người nhận"
      subtitle="Yêu cầu truy cập số điện thoại"
    />
  );
};

