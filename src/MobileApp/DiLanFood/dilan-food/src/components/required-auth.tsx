import React, { FC, useEffect } from "react";
import { Routes } from "@/constants/routes";
import { useNavigate } from "react-router-dom";
import { Box, Button, Text } from "zmp-ui";

export default function RequiredAuth() {

    const navigate = useNavigate();

    return (
        <Box className="bg-white p-4 mb-4 rounded-md shadow-sm flex flex-col gap-1">
            <Text className="text-gray-500 text-center">Vui lòng đăng nhập để xem địa chỉ!</Text>
            <Button
                className="mt-4"
                onClick={() => navigate(Routes.auth.signIn())}
            >Đăng nhập</Button>

        </Box>
    )
}