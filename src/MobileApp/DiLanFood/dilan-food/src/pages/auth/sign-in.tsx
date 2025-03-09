// import { login } from "libs/auth.action";
// import { useAppSelector } from "libs/store/store";
import { MERCHANT_ID } from "@/constants/common";
import { login, loginWithZalo } from "@/libs/auth.action";
import { getCurrentUserInfoZalo } from "@/libs/zalo.user.action";
import { MerchantPageLoading } from "@/modules/merchants/components/merchant-page-loading";
import { useMerchant } from "@/modules/merchants/use-merchant";
import { useUserStore } from "@/state/user-state";
import React, { ChangeEvent, FC } from "react";
import { Box, Button, Header, Input, Page, Text, useNavigate, useSnackbar } from "zmp-ui";

export interface SignInParams {
    merchantId: string;
    phoneNumber: string;
    password: string;
}

export const SignIn: FC = () => {


    const { openSnackbar } = useSnackbar();
    const login = useUserStore((state) => state.login);

    const { data: mechantState, isLoading: isMerchantLoading } = useMerchant();

    const [isLoading, setIsLoading] = React.useState(false);
    const [signInForm, setSignInForm] = React.useState<SignInParams>({
        merchantId: "",
        phoneNumber: "",
        password: ""
    });

    const navigate = useNavigate();


    const onClickSignUp = () => {
        navigate("/sign-up");
    }

    const onChangePhoneNumber = (phoneNumber: ChangeEvent<HTMLInputElement>) => {
        setSignInForm({
            ...signInForm,
            phoneNumber: phoneNumber.target.value
        })
    }

    const onChangePassword = (password: ChangeEvent<HTMLInputElement>) => {
        setSignInForm({
            ...signInForm,
            password: password.target.value
        })
    }

    const onClickContinue = async () => {
        // setIsLoading(true);

        // // Call API to login
        // // If success, navigate to home page
        // // If fail, show error message
        // if (mechantState) {
        //     try {
        //         const response = await login({
        //             merchantId: mechantState.id,
        //             phoneNumber: signInForm.phoneNumber,
        //             password: signInForm.password
        //         });

        //         if (response.success) {
        //             openSnackbar({
        //                 type: 'success',
        //                 text: 'Đăng nhập thành công',
        //             });

        //             // Save token in hook
        //             if (response.data) userAuthActions.initToken(response.data);

        //             navigate("/");
        //         }
        //         else {
        //             openSnackbar({
        //                 type: 'error',
        //                 text: response.error?.message,
        //             });
        //         }

        //     }
        //     catch (error) {
        //         console.error("Error login:", error);
        //     }
        //     finally {
        //         setIsLoading(false);
        //     }
        // }

        console.log("Sign in form:", signInForm);

    }
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
        }
    }

    const onLoginSuccess = () => {
        openSnackbar({
            type: 'info',
            text: 'Đăng nhập thành công',
        });

        navigate("/");
    }

    if (isMerchantLoading) return <MerchantPageLoading />;

    return (
        <Page className="flex flex-col justify-center">
            <Header title="Đăng nhập" className="text-white bg-primary" showBackIcon />

            <Box className="flex flex-col items-center px-4 py-4">
                <Text size="xLarge" className="py-2 font-extrabold">
                    {mechantState?.name} xin chào
                </Text>

                <Input
                    label="Số điện thoại"
                    placeholder="Nhập số điện thoại"
                    className="w-full py-2"
                    value={signInForm.phoneNumber}
                    onChange={(text) => onChangePhoneNumber(text)}
                    type="number"
                />

                <Input
                    placeholder="Mật khẩu"
                    label="Mật khẩu"
                    className="w-full py-2"
                    type="password"
                    value={signInForm.password}
                    onChange={(text) => onChangePassword(text)}
                />
                <Button
                    className="w-full mt-4"
                    loading={isLoading}
                    onClick={onClickContinue}
                    disabled={signInForm.phoneNumber === "" || signInForm.password === ""}
                >
                    Đăng Nhập
                </Button>

                <Box className="flex flex-col gap-2 py-2">
                    <Box className="flex  justify-center items-center mt-4">
                        <Text>Chưa có tài khoản?</Text>
                        <Button

                            variant="tertiary"
                            size="small"
                            onClick={onClickSignUp}
                        >
                            Đăng ký
                        </Button>
                    </Box>
                    <Button
                        variant="secondary"
                        size="small"
                        onClick={onClickSignInWithZalo}
                        loading={isLoading}
                    >
                        Tiếp tục với tài khoản Zalo
                    </Button>
                </Box>

            </Box>
        </Page>
    )
}