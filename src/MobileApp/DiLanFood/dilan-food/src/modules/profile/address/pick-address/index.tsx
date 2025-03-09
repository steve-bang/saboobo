
import ModalSpinner from "@/components/modal-spinner";
import RequiredAuth from "@/components/required-auth";
import { getAddresses } from "@/libs/user.action";
import { useCart, userShippingAddressCart } from "@/modules/orders/use-cart";
import useAddress from "@/state/address-state";
import { useUserStore } from "@/state/user-state";
import React, { FC, useEffect, } from "react";
import { Box, Header, Icon, Page, Radio, Text, useSnackbar } from "zmp-ui";
import { RadioValueType } from "zmp-ui/radio";

export function PickAddressPage() {

    const { openSnackbar } = useSnackbar();

    const { actions } = useCart();
    const shippingAddress = userShippingAddressCart();

    const { isAuthenticated, userId } = useUserStore((state) => state);

    const [modalVisible, setModalVisible] = React.useState(false);

    const { items: addresses, setAddresses } = useAddress((state) => state);


    const onChangeOption = (value: RadioValueType) => {

        const addressSelected = addresses?.find((address) => address.id === value.toString());

        if (addressSelected) {
            actions.updateAddressDetail(addressSelected.fullAddress);
            actions.updatePhoneNumber(addressSelected.phoneNumber);
        }
    }

    useEffect(() => {

        function fetchAddresses() {
            if (userId) {

                setModalVisible(true);

                getAddresses(userId).then((data) => {
                    if (data) {
                        setAddresses(data);
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

        if (addresses === null) {
            fetchAddresses();
        }

    }, [userId]);


    return (
        <Page className="my-10 space-y-3 px-4" >
            <Header
                backIcon={<Icon icon="zi-arrow-left" className="text-black" />}
                title={
                    (
                        <Text.Title size="small">Chọn địa chỉ</Text.Title>
                    ) as unknown as string
                }
            />

            {
                !isAuthenticated ? (
                    <RequiredAuth />
                ) : (
                    <Radio.Group
                        onChange={(value) => onChangeOption(value)}
                    >
                        {
                            addresses?.map((address, index) => (
                                <Box className="bg-white p-4 m-2 rounded-md shadow-sm flex flex-col gap-1">
                                    <Radio
                                        key={index}
                                        value={address.id}
                                        className="mb-2"
                                        size="small"
                                        checked={shippingAddress?.addressDetail === address.fullAddress}
                                    >

                                        <div className="flex items-center gap-1">
                                            <div>{address.contactName}</div>
                                            |
                                            <div>{address.phoneNumber}</div>
                                        </div>
                                        <Text className="text-gray-500"
                                        >{address.fullAddress}</Text>

                                    </Radio>
                                </Box>
                            ))
                        }
                    </Radio.Group>
                )
            }



            <ModalSpinner visible={modalVisible} onClose={() => setModalVisible(false)} />
        </Page>
    )
}