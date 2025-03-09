
import ModalSpinner from "@/components/modal-spinner";
import { Routes } from "@/constants/routes";
import { GetAllCities, GetDistrictByCityName, GetWardByDistrictName } from "@/libs/location.action";
import { addAddress, IUserAddressProps } from "@/libs/user.action";
import { HeaderAddAddress } from "@/modules/profile/address/add-new-address/components/header";
import useAddress from "@/state/address-state";
import { useUserStore } from "@/state/user-state";
import { ICity } from "@/types/address";
import React, { FC, useEffect, useState } from "react";
import { Box, Button, Input, Page, Select, useNavigate, useSnackbar } from "zmp-ui";
import { SelectValueType } from "zmp-ui/select";

const { Option } = Select;

export const AddNewAddressPage: FC = () => {

    const { clearAddress } = useAddress((state) => state);

    const [states, setCities] = useState<ICity[]>([]);
    const [cities, setDistricts] = useState<any[]>([]);
    const [wards, setWards] = useState<any[]>([]);

    const navigate = useNavigate();

    const { openSnackbar } = useSnackbar();
    const [modalVisible, setModalVisible] = useState(false);
    const { userId } = useUserStore((state) => state);

    const [addressForm, setAddressForm] = useState<IUserAddressProps>({
        contactName: "",
        phoneNumber: "",
        addressLine1: "",
        addressLine2: "",
        state: "",
        city: "",
        country: "Vietnam",
        isDefault: false,
    });

    useEffect(() => {
        const fetchCities = async () => {
            const cities = await GetAllCities();
            setCities(cities);
        }

        fetchCities();
    }, []);


    const onSelectState = async (value: SelectValueType | SelectValueType[] | undefined) => {

        if (value) {

            const cityValue = value as string;
            setAddressForm({ ...addressForm, state: cityValue });


            setModalVisible(true);
            const districts = await GetDistrictByCityName(states, cityValue);
            setDistricts(districts);
            setModalVisible(false);
        }
    }

    const onSelectCity = async (value: SelectValueType | SelectValueType[] | undefined) => {

        if (value) {

            const districtValue = value as string;

            setWards([]);

            setModalVisible(true);

            setAddressForm({ ...addressForm, city: districtValue });

            const wards = await GetWardByDistrictName(cities, districtValue);
            setWards(wards);

            setModalVisible(false);

        }
    }

    const onSelectAddressLin2 = (value: SelectValueType | SelectValueType[] | undefined) => {

        if (value) {
            setAddressForm({ ...addressForm, addressLine2: value as string });
        }
    }

    const onClickSaveAddress = async () => {

        if (!userId) return;

        if (!addressForm.contactName) {
            openSnackbar({
                type: "error",
                text: "Vui lòng nhập tên người nhận!"
            });
            return;
        }

        if (!addressForm.phoneNumber) {
            openSnackbar({
                type: "error",
                text: "Vui lòng nhập số điện thoại!"
            });
            return;
        }

        if (!addressForm.city) {
            openSnackbar({
                type: "error",
                text: "Vui lòng chọn tỉnh/thành phố!"
            });
            return;
        }

        if (!addressForm.state) {
            openSnackbar({
                type: "error",
                text: "Vui lòng chọn quận/huyện!"
            });
            return;
        }

        if (!addressForm.addressLine2) {
            openSnackbar({
                type: "error",
                text: "Vui lòng chọn phường/xã!"
            });
            return;
        }

        if (!addressForm.addressLine1) {
            openSnackbar({
                type: "error",
                text: "Vui lòng nhập địa chỉ!"
            });
            return;
        }


        setModalVisible(true);

        addAddress(userId, addressForm)
            .then((data) => {
                clearAddress();

                openSnackbar({
                    type: "success",
                    text: "Thêm địa chỉ thành công!"
                });

                navigate(Routes.address.myAddress());

            }).catch((error) => {
                openSnackbar({
                    type: "error",
                    text: "Có lỗi xảy ra khi thêm địa chỉ, vui lòng thử lại sau!"
                });
            }).finally(() => {
                setModalVisible(false);
            });
    }



    return (

        <Page className="my-10 space-y-3 px-4" >
            <HeaderAddAddress />
            <Box className="flex flex-col items-center">

                <Input
                    label="Tên người nhận"
                    className="w-full"
                    type="text"
                    size="small"
                    onChange={(e) => setAddressForm({ ...addressForm, contactName: e.currentTarget.value })}
                    value={addressForm.contactName}
                />
                <Input
                    label="Số điện thoại"
                    className="w-full"
                    size="small"
                    type="number"
                    onChange={(e) => setAddressForm({ ...addressForm, phoneNumber: e.currentTarget.value })}
                    value={addressForm.phoneNumber}
                />

                <Box className="w-full">
                    <Select
                        label="Tỉnh/Thành phố"
                        placeholder="Chọn tỉnh/thành phố"
                        value={addressForm.state}
                        onChange={(e) => onSelectState(e)}
                        className="max-h-2/5"
                        closeOnSelect
                    >
                        {states?.map((state, index) => (
                            <Option key={index} value={state.name} title={state.name}>{state.name}</Option>
                        ))}
                    </Select>
                </Box>

                <Box className="w-full section-container">
                    <Select
                        label="Quận/Huyện"
                        placeholder="Chọn quận/huyện"
                        value={addressForm.city}
                        onChange={(e) => onSelectCity(e)}
                        disabled={cities.length === 0}
                        closeOnSelect
                    >
                        {cities?.map((city, index) => (
                            <Option key={index} value={city.name} title={city.name_with_type}>{city.name_with_type}</Option>
                        ))}
                    </Select>
                </Box>


                <Box className="w-full">
                    <Select
                        label="Phường/Xã"
                        placeholder="Chọn phường/xã"
                        value={addressForm.addressLine2}
                        onChange={(e) => onSelectAddressLin2(e)}
                        disabled={wards.length === 0}
                        closeOnSelect
                    >
                        {wards?.map((ward, index) => (
                            <Option key={index} value={ward.name} title={ward.name_with_type}>{ward.name_with_type}</Option>
                        ))}
                    </Select>
                </Box>

                <Input
                    label="Địa chỉ"
                    className="w-full"
                    size="small"
                    placeholder="Số nhà, tên đường"
                    onChange={(e) => setAddressForm({ ...addressForm, addressLine1: e.currentTarget.value })}
                    value={addressForm.addressLine1}
                />

                <Button
                    className="w-full mt-8"
                    onClick={onClickSaveAddress}
                >
                    Lưu
                </Button>

            </Box>

            <ModalSpinner visible={modalVisible} onClose={() => setModalVisible(false)} />
        </Page>

    )
}