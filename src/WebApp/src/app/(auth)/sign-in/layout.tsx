import type { Metadata } from "next";
import Sidebar from "@/components/SidebarMenu";


export const metadata: Metadata = {
    title: "Sign In | SaBooBo",
    description: "The SaBooBo dashboard",
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <section>
            {children}
        </section>
    );
}
