import React, { FC } from 'react'
import { Box, Icon, Page, Text, useNavigate } from 'zmp-ui'
import ProfilePageHeader from './header'
import { ListRenderer } from '@/components/list-renderer';
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

    return (
        <Box className="">
            <ListRenderer
                title="Cá nhân"
                //onClick={}
                items={[
                    {
                        left: <Icon icon="zi-user" />,
                        right: (
                            <Box flex>
                                <Text.Header className="flex-1 items-center font-normal">
                                    Thông tin tài khoản
                                </Text.Header>
                                <Icon icon="zi-chevron-right" />
                            </Box>
                        ),
                    },
                    {
                        left: <Icon icon="zi-clock-2" />,
                        right: (
                            <Box flex>
                                <Text.Header className="flex-1 items-center font-normal">
                                    Lịch sử đơn hàng
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
    return (
        <Page className='my-10 space-y-3 px-4'>
            <ProfilePageHeader />
            <Subscription />
            <Personal />
        </Page>
    )
}