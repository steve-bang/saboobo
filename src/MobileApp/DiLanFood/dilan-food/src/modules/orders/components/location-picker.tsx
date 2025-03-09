import { ListItem } from "@/components/list-item";
import React, { FC, useState } from "react";
import { useNavigate, useSnackbar } from "zmp-ui";
import { useCart } from "../use-cart";
import ModalSpinner from "@/components/modal-spinner";
import { Routes } from "@/constants/routes";

export const LocationPicker: FC = () => {

    const { openSnackbar } = useSnackbar();

    const navigation = useNavigate();

    const { shippingAddress } = useCart();
    const [modalVisible, setModalVisible] = useState(false);

    const onClick = async () => {

        navigation(Routes.address.pickAddress());

        // setModalVisible(true);

        // await getLocation({
        //     success: async (data) => {
        //         let { token } = data;

        //         if (token) {
        //             getAccessToken({
        //                 success: async (accessToken) => {
        //                     const locationActual = await getZaloDataByAccessTokenAndCode(accessToken, token);

        //                     console.log('locationActual', locationActual);

        //                     const addressDetails = await getAddressFromLatLong(locationActual.data.latitude, locationActual.data.longitude);

        //                     console.log('Address detail: ', addressDetails);

        //                     if (addressDetails) {
        //                         actions.updateAddressDetail(addressDetails.display_name);

        //                         setModalVisible(false);
        //                     }
        //                     else {
        //                         openSnackbar({
        //                             type: "error",
        //                             text: "Không thể lấy địa chỉ của bạn, vui lòng kiểm tra lại kết nối mạng!"
        //                         })
        //                     }

        //                 },
        //                 fail: (error) => {
        //                     console.log('error get access token', error);
        //                 },
        //             });
        //         }


        //     },
        //     fail: (error) => {
        //         openSnackbar({
        //             type: "error",
        //             text: "Không thể lấy vị trí của bạn, vui lòng kiểm tra lại kết nối mạng!"
        //         })
        //     },
        // });

        // setModalVisible(false);
    }


    return (
        <>
            <ListItem
                title="Địa chỉ"
                subtitle={shippingAddress?.addressDetail || "Chọn địa chỉ nhận hàng"}
                onClick={onClick}
            />

            <ModalSpinner visible={modalVisible} onClose={() => setModalVisible(false)} />
        </>
    );
};
