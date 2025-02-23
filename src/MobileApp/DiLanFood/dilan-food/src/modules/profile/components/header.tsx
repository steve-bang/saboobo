import React from 'react'
import { Header, Text } from "zmp-ui";


export default function ProfilePageHeader() {
    return (
        <Header
            className="app-header no-border pl-4 flex-none pb-[6px]"
            showBackIcon={false}
            title={
                (
                    <Text.Title size="small">Thông tin tài khoản</Text.Title>
                ) as unknown as string
            }
        />
    )
}