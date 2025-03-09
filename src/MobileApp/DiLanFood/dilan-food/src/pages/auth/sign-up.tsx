
import { MERCHANT_ID } from "@/constants/common";
import { loginWithZalo, register } from "@/libs/auth.action";
import { getCurrentUserInfoZalo } from "@/libs/zalo.user.action";
import { useMerchant } from "@/modules/merchants/use-merchant";
import { useUserStore } from "@/state/user-state";
import React, { ChangeEvent, FC } from "react";
import { Box, Button, Header, Input, Page, Text, useNavigate, useSnackbar } from "zmp-ui";

export interface SignUpParams {
    merchantId: string;
    phoneNumber: string;
    password: string;
    confirmPassword: string;
}

export const SignUp: FC = () => {

    const { openSnackbar } = useSnackbar();
    const { login } = useUserStore((state) => state);

    const [isLoading, setIsLoading] = React.useState(false);
    const [signUpForm, setSignUpForm] = React.useState<SignUpParams>({
        merchantId: "",
        phoneNumber: "",
        password: "",
        confirmPassword: ""
    });

    const navigate = useNavigate();

    const { data: mechantState, isLoading: isMerchantLoading } = useMerchant();


    const onClickSignIn = () => {
        navigate("/sign-in");
    }

    const onChangePhoneNumber = (phoneNumber: ChangeEvent<HTMLInputElement>) => {
        setSignUpForm({
            ...signUpForm,
            phoneNumber: phoneNumber.target.value
        })
    }

    const onChangePassword = (password: ChangeEvent<HTMLInputElement>) => {
        setSignUpForm({
            ...signUpForm,
            password: password.target.value
        })
    }

    const onChangeConfirmPassword = (confirmPassword: ChangeEvent<HTMLInputElement>) => {
        setSignUpForm({
            ...signUpForm,
            confirmPassword: confirmPassword.target.value
        })
    }

    const onClickSignUp = async () => {

        setIsLoading(true);


        // Call API to register
        // If success, navigate to home page
        // If fail, show error message
        if (mechantState) {
            try {
                const response = await register({
                    merchantId: mechantState.id,
                    phoneNumber: signUpForm.phoneNumber,
                    password: signUpForm.password,
                    confirmPassword: signUpForm.confirmPassword
                });

                if (response.success) {
                    onRegisterSuccess();
                }
                else {
                    openSnackbar({
                        type: 'error',
                        text: response.error?.message,
                    });
                }
            }
            catch (error: any) {
                openSnackbar({
                    type: 'error',
                    text: "Đã xảy ra lỗi, không thể đăng ký",
                });
                console.error("Error registering:", error);
            }
            finally {
                setIsLoading(false);
            }
        }
    }


    const onClickSignInWithZalo = async () => {
        try {
            setIsLoading(true);
            const userInfo = await getCurrentUserInfoZalo();

            if (userInfo) {
                const response = await loginWithZalo(MERCHANT_ID, userInfo);

                if (response.success) {
                    // Save token in hook
                    if (response.data) login(response.data);

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

    const onRegisterSuccess = () => {
        openSnackbar({
            type: 'info',
            text: 'Đăng ký thành công',
        });


        navigate("/sign-in");
    }

    return (
        <Page className="flex flex-col justify-center" >
            <Header title="Đăng ký thành viên" className="text-white bg-primary" showBackIcon={true} />

            <Box className="flex flex-col items-center px-4 py-4">
                <Text size="xLarge" className="py-2 font-extrabold">
                    {mechantState?.name} Xin chào
                </Text>


                <Input
                    placeholder="Nhập số điện thoại"
                    label="Số điện thoại"
                    className="w-full py-2"
                    value={signUpForm.phoneNumber}
                    onChange={(text) => onChangePhoneNumber(text)}
                    type="number"
                />

                <Input
                    placeholder="Mật khẩu"
                    label="Mật khẩu"
                    className="w-full py-2"
                    type="password"
                    value={signUpForm.password}
                    onChange={(text) => onChangePassword(text)}
                />

                <Input
                    placeholder="Nhập lại mật khẩu"
                    label="Nhập lại mật khẩu"
                    className="w-full py-2"
                    type="password"
                    value={signUpForm.confirmPassword}
                    onChange={(text) => onChangeConfirmPassword(text)}
                />
                <Button
                    className="w-full mt-4"
                    disabled={isLoading || signUpForm.phoneNumber === "" || signUpForm.password === "" || signUpForm.confirmPassword === ""}
                    onClick={onClickSignUp}
                    loading={isLoading}

                >
                    Đăng ký
                </Button>

                <Box className="flex flex-col gap-2 py-2">
                    <Box className="flex flex-row justify-center items-center">
                        <Text>Đã có tài khoản?</Text>
                        <Button
                            variant="tertiary"
                            size="small"
                            onClick={onClickSignIn}
                        >
                            Đăng nhập
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