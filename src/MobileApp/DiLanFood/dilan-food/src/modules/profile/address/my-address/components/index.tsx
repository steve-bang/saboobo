
import ModalSpinner from "@/components/modal-spinner";
import { Routes } from "@/constants/routes";
import { getAddresses } from "@/libs/user.action";
import { AddressItem } from "@/modules/profile/address/my-address/components/address-item";
import useAddress from "@/state/address-state";
import { useUserStore } from "@/state/user-state";
import { IUserAddress } from "@/types/address";
import React, { FC, useEffect } from "react";
import { Button, Header, Icon, Page, Text, useNavigate, useSnackbar } from "zmp-ui";

export function MyAddressPage() {
    const navigate = useNavigate();

    const { openSnackbar } = useSnackbar();

    const { userId } = useUserStore((state) => state);

    const [modalVisible, setModalVisible] = React.useState(false);

    const { items, setAddresses: setAddress } = useAddress((state) => state);

    useEffect(() => {

        function fetchAddresses() {
            if (userId) {

                setModalVisible(true);

                getAddresses(userId).then((data) => {
                    if (data) {
                        setAddress(data);
                    }
                })
                    .catch((error) => {
                        openSnackbar({
                            type: "error",
                            text: "Có lỗi xảy ra khi lấy dữ liệu địa chỉ, vui lòng thử lại sau!"
                        });
                    })
                    .finally(() => {
                        setModalVisible(false);
                    });

            }
        }

        if (items === null) {
            fetchAddresses();
        }

    }, [userId]);

    return (
        <Page className="my-10 space-y-3 px-4" >
            <Header
                backIcon={<Icon icon="zi-arrow-left" className="text-black" />}
                title={
                    (
                        <Text.Title size="small">Địa chỉ của Tôi</Text.Title>
                    ) as unknown as string
                }
            />
            {
                items?.map((address, index) => (
                    <AddressItem key={index} address={address} />
                ))
            }

            <Button className="w-full" onClick={() => navigate(Routes.address.addNewAddress())} >Thêm địa chỉ</Button>

            <ModalSpinner visible={modalVisible} onClose={() => setModalVisible(false)} />
        </Page>
    )
}