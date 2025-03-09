
import { ListItem } from "@/components/list-item";
import React, { FC, useState } from "react";
import { useCart } from "../use-cart";
import { useSnackbar } from "zmp-ui";
import ModalSpinner from "@/components/modal-spinner";
import { getPhoneNumberUser } from "@/libs/zalo.user.action";


export const RequestPersonPickerPhone: FC = () => {

  const { openSnackbar } = useSnackbar();

  const { shippingAddress, actions } = useCart();

  const [modalVisible, setModalVisible] = useState(false);



  const onClickRequestPersonPickerPhone = async () => {

    setModalVisible(true);
    
    const phoneNumber = await getPhoneNumberUser();

    if (phoneNumber) {
      actions.updatePhoneNumber(phoneNumber);
    }
    else {
      openSnackbar({
        type: "error",
        text: "Không thể lấy số điện thoại người dùng, vui lòng kiểm tra lại kết nối mạng!"
      })
    }

    setModalVisible(false);
  };

  return (
    <>
      <ListItem
        onClick={onClickRequestPersonPickerPhone}
        title={"Số điện thoại"}
        subtitle={shippingAddress?.phoneNumber || "Yêu cầu truy cập số điện thoại"}
      />

      <ModalSpinner visible={modalVisible} onClose={() => setModalVisible(false)} />
    </>
  );
};

