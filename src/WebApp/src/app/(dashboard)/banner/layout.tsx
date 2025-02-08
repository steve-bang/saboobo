
import { Metadata } from "next";


export const metadata: Metadata = {
    title: 'Banner',
}

export default function ProfileLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <>
            {children}
        </>
    )
}