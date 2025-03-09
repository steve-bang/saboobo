import React, { FC } from "react";
import { useNavigate } from "react-router-dom";
import { Icon } from "zmp-ui";

export interface HeaderProps {
    title: string;
    showBack?: boolean;
}

export default function Header(
    { title, showBack = false }: HeaderProps
) {
    const navigate = useNavigate();

    return (
        <div className="h-20 w-full flex items-center pl-2 pr-[106px] py-2 space-x-1 bg-white shadow-md">
            {showBack && (
                <div className="px-2 cursor-pointer" onClick={() => navigate(-1)}>
                    <Icon icon="zi-arrow-left" />
                </div>
            )}
            <div className="font-medium truncate">{title}</div>
        </div>
    );
}