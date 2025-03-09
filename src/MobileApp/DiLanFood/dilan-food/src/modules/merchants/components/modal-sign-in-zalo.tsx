import ModalSpinner from '@/components/modal-spinner';
import { MERCHANT_ID } from '@/constants/common';
import { loginWithZalo } from '@/libs/auth.action';
import { getCurrentUserInfoZalo } from '@/libs/zalo.user.action';
import { useUserStore } from '@/state/user-state';
import React from 'react'
import { Modal, useSnackbar } from 'zmp-ui'

export interface ModalSignInZaloProps {
    modalVisible: boolean
    setModalVisible: (value: boolean) => void
}

export default function ModalSignInZalo({ modalVisible, setModalVisible }: ModalSignInZaloProps) {

    const { openSnackbar } = useSnackbar();

    const [isLoading, setIsLoading] = React.useState(false);

    const login = useUserStore(state => state.login);

    const onClickSignInWithZalo = async () => {
        try {
            setIsLoading(true);
            const userInfo = await getCurrentUserInfoZalo();

            if (userInfo) {
                const response = await loginWithZalo(MERCHANT_ID, userInfo);

                if (response.success) {
                    // Save token in hook
                    if (response.data) {
                        //userAuthActions.initToken(response.data); // #Deprecated
                        login(response.data);
                    }

                    onLoginSuccess();
                }
                else {
                    openSnackbar({
                        type: 'error',
                        text: response.error?.message,
                    });
                }
            }
        }
        catch (error) {
            console.error("Error fetching Zalo user info:", error);

            openSnackbar({
                type: 'error',
                text: "Lỗi khi đăng nhập bằng Zalo, vui lòng thử lại sau.",
            });
        }
        finally {
            setIsLoading(false);
            setModalVisible(false);
        }
    }

    const onLoginSuccess = () => {
        openSnackbar({
            type: 'info',
            text: 'Đăng nhập thành công',
        });
        setModalVisible(false);
    }

    return (
        <>
            <Modal
                visible={modalVisible}
                onClose={() => setModalVisible(false)}
                title="Đăng nhập bằng Zalo"
                description='Vui lòng đăng nhập bằng tài khoản Zalo để tiếp tục sử dụng ứng dụng.'
                coverSrc={"https://static.vecteezy.com/system/resources/previews/005/879/539/non_2x/cloud-computing-modern-flat-concept-for-web-banner-design-man-enters-password-and-login-to-access-cloud-storage-for-uploading-and-processing-files-illustration-with-isolated-people-scene-free-vector.jpg"}
                actions={[
                    {
                        text: 'Cho Phép',
                        highLight: true,
                        onClick: onClickSignInWithZalo,
                    },
                ]}
            >
            </Modal>

            <ModalSpinner visible={isLoading} onClose={() => setIsLoading(false)} />
        </>
    )
}