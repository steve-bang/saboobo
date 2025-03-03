import { Metadata } from "next";

export const metadata: Metadata = {
    title: 'Profile',
  }

export default function ProfileLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (<>
        {children}
    </>)
}