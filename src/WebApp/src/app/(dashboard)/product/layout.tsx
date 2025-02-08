import { Metadata } from "next";

export const metadata: Metadata = {
    title: 'Product',
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