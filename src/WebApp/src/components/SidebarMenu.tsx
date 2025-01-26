
'use client'

import { useState } from "react";
import Link from "next/link";
import { Drawer, DrawerContent } from "./ui/drawer"; // Make sure this path is correct
import { Layers, LogOut, Package, Store, User } from "lucide-react";
import { usePathname } from "next/navigation";

const Links = [
    { href: "/profile", label: "Profile", icon: <User />, active: true, current: false },
    { href: "/merchant", label: "Merchant", icon: <Store />, active: false, current: false },
    { href: "/category", label: "Category", icon: <Layers />, active: false, current: true },
    { href: "/product", label: "Product", icon: <Package />, active: false, current: false },
];

const Sidebar = () => {
    const [open, setOpen] = useState(false);

    const pathname = usePathname();

    Links.forEach((link) => {
        link.current = link.href.startsWith(pathname);
    });

    return (
        <div className="flex h-screen">
            {/* Desktop Sidebar */}
            <div className="hidden lg:flex flex-shrink-0 flex-col w-64 bg-gray-800 text-white">
                <div className="p-6 text-xl font-bold">SaBooBo</div>
                <ul className="space-y-4 px-4">
                    {/* Dynamic Links */}
                    {Links.map((link) => (
                        <li key={link.href}>
                            <Link href={link.href} className={`py-2 px-4 hover:bg-gray-700 hover:rounded-lg flex gap-2 ${link.current ? "bg-gray-700 rounded-lg" : ""}`}>
                                {link.icon} {link.label}
                            </Link>
                        </li>
                    ))}
                </ul>

                <div className="mt-auto px-4">
                    <Link href={'/'} className='py-2 px-4 hover:bg-gray-700 hover:rounded-lg flex gap-2'>
                        <LogOut /> Log out
                    </Link>

                    <div className="text-xs text-gray-400 mt-2 text-center">v1.0.0</div>

                </div>
            </div>

            {/* Mobile Hamburger Button */}
            <div className="lg:hidden p-6">
                <button
                    className="text-white bg-gray-800 p-2 rounded"
                    onClick={() => setOpen(true)}
                >
                    ☰
                </button>
            </div>

            {/* Drawer for mobile */}
            <Drawer open={open} onClose={() => setOpen(false)}>
                <DrawerContent className="w-64 bg-gray-800 text-white p-6">
                    <div className="text-xl font-bold">Logo</div>
                    <ul className="space-y-4 mt-6">
                        {/* Dynamic Links */}
                        {Links.map((link) => (
                            <li key={link.href}>
                                <Link href={link.href} className={`py-2 px-4 hover:bg-gray-700 hover:rounded-lg flex items-center gap-2 ${link.current ? "bg-gray-700 rounded-lg" : ""}`}>
                                    {link.icon} {link.label}
                                </Link>
                            </li>
                        ))}
                    </ul>
                </DrawerContent>
            </Drawer>
        </div>
    );
};

export default Sidebar;