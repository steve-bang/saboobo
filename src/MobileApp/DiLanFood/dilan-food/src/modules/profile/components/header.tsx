import React from 'react'
import { Header, Text } from "zmp-ui";


export default function ProfilePageHeader() {
    return (
        <Header
            showBackIcon={false}
            title={
                (
                    <Text.Title size="small">Thông tin tài khoản</Text.Title>
                ) as unknown as string
            }
        />
    )
}