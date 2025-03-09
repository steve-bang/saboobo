import React, { FC } from 'react'
import { Box, Button, Icon, Page, Text, useNavigate, useSnackbar } from 'zmp-ui'
import ProfilePageHeader from './header'
import { ListRenderer } from '@/components/list-renderer';
import { useUserStore } from '@/state/user-state';
import ModalSignInZalo from '@/modules/merchants/components/modal-sign-in-zalo';
const Subscription: FC = () => {

    const navigate = useNavigate();

    // const requestUserInfo = useRecoilCallback(
    //     ({ snapshot }) =>
    //         async () => {
    //             const userInfo = await snapshot.getPromise(userState);
    //             console.warn("Các bên tích hợp có thể sử dụng userInfo ở đây...", {
    //                 userInfo,
    //             });
    //         },
    //     []
    // );

    const onClick = () => {
        navigate("/sign-in");
    }

    return (
        <Box className="">
            <Box
                className="bg-primary text-white rounded-xl p-4 space-y-2"
                style={{
                    backgroundPosition: "right 8px center",
                    backgroundRepeat: "no-repeat",
                }}
                onClick={onClick}
            >
                <Text.Title className="font-bold">Đăng ký thành viên</Text.Title>
                <Text size="xxSmall">Tích điểm đổi thưởng, mở rộng tiện ích</Text>
            </Box>
        </Box>
    );
};


const Personal: FC = () => {

    const { openSnackbar } = useSnackbar();

    const navigate = useNavigate();

    const onClick = () => {
        openSnackbar({
            type: "info",
            text: "Chức năng đang phát triển",
        });
    }

    const onClickAddress = () => {
        navigate("/my-address");
    }

    return (
        <Box className="">
            <ListRenderer
                title="Cá nhân"
                items={[
                    {
                        left: <Icon icon="zi-user" />,
                        right: (
                            <Box flex
                                onClick={onClick}
                            >
                                <Text.Header className="flex-1 items-center font-normal">
                                    Thông tin tài khoản
                                </Text.Header>
                                <Icon icon="zi-chevron-right" />
                            </Box>
                        ),
                    },
                    {
                        left: <Icon icon="zi-location" className="my-auto" />,
                        right: (
                            <Box flex
                                onClick={onClickAddress}
                            >
                                <Text.Header className="flex-1 items-center font-normal">
                                    Địa chỉ
                                </Text.Header>
                                <Icon icon="zi-chevron-right" />
                            </Box>
                        ),
                    },
                    {
                        left: <Icon icon="zi-clock-2" />,
                        right: (
                            <Box flex onClick={onClick}>
                                <Text.Header className="flex-1 items-center font-normal">
                                    Lịch sử đơn hàng
                                </Text.Header>
                                <Icon icon="zi-chevron-right" />
                            </Box>
                        ),
                    },
                    {
                        left: <Icon icon="zi-notif-ring" />,
                        right: (
                            <Box flex onClick={onClick}>
                                <Text.Header className="flex-1 items-center font-normal">
                                    Thông báo
                                </Text.Header>
                                <Icon icon="zi-chevron-right" />
                            </Box>
                        ),
                    },
                ]}
                renderLeft={(item) => item.left}
                renderRight={(item) => item.right}
            />
        </Box>
    );
};

export default function Profile() {

    const { logout, isAuthenticated } = useUserStore((state) => state);

    const { openSnackbar } = useSnackbar();

    const onClickSignOut = () => {
        logout();

        openSnackbar({
            type: "success",
            text: "Đăng xuất thành công",
        });
    }


    return (
        <Page className='my-10 space-y-3 px-4'>
            <ProfilePageHeader />
            <Personal />

            {
                isAuthenticated && (
                    <Button
                        className="w-full text-rose-500 bg-rose-100"
                        onClick={onClickSignOut}>
                        Đăng xuất
                    </Button>
                )
            }

            <ModalSignInZalo modalVisible={!isAuthenticated} setModalVisible={() => console.log('')} />
        </Page>
    )
}