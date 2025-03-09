import { Modal, Spinner } from "zmp-ui";
import React, { FC, useState } from "react";
import { useMerchant } from "@/modules/merchants/use-merchant";

export interface ModalSpinnerProps {
    visible: boolean;
    onClose: (e: React.SyntheticEvent) => void
}

export const ModalSpinner: FC<ModalSpinnerProps> = ({
    visible,
    onClose
}) => {

      const { data: merchant } = useMerchant();

    return (
        <Modal
            visible={visible}
            onClose={onClose}
            modalClassName="flex items-center justify-center bg-transparent"
        >
            {
                merchant?.logoUrl ? <Spinner visible logo={merchant.logoUrl} /> : <Spinner visible />
            }

        </Modal>
    );
}


export default ModalSpinner;