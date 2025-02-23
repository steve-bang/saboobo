import type { Metadata } from "next";

const appName = process.env.NEXT_PUBLIC_APP_NAME as string;

export const metadata: Metadata = {
    title: appName ? `Sign In | ${appName}` : "Sign In",
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
