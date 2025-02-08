"use client"

import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import { Toaster } from "@/components/ui/toaster";
import { Provider } from "react-redux";
import { persistor, store } from "@/lib/store/store";
import { PersistGate } from 'redux-persist/integration/react';
import { Spinner } from "@/components/ui/spinner";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});


export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <Provider store={store}>
          <PersistGate loading={<div className="flex items-center justify-center min-h-full"><Spinner /></div>} persistor={persistor}>
            <div >{children}</div>
            <Toaster />
          </PersistGate>
        </Provider>
      </body>
    </html>
  );
}
