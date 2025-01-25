
'use client'

import { useState } from "react";
import Link from "next/link";
import { Drawer, DrawerContent } from "./ui/drawer"; // Make sure this path is correct

const Sidebar = () => {
    const [open, setOpen] = useState(false);

    return (
        <div className="flex h-screen">
            {/* Desktop Sidebar */}
            <div className="hidden lg:flex flex-shrink-0 flex-col w-64 bg-gray-800 text-white">
                <div className="p-6 text-xl font-bold">SaBooBo</div>
                <ul className="space-y-4">
                    <li>
                        <Link href="/profile" className="block py-2 px-4 hover:bg-gray-700">
                            Profile
                        </Link>
                    </li>
                    <li>
                        <Link href="/merchant" className="block py-2 px-4 hover:bg-gray-700">
                            Merchant
                        </Link>
                    </li>
                    <li>
                        <Link href="/category" className="block py-2 px-4 hover:bg-gray-700">
                            Category
                        </Link>
                    </li>
                    <li>
                        <Link href="/product" className="block py-2 px-4 hover:bg-gray-700">
                            Product
                        </Link>
                    </li>
                </ul>
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
                        <li>
                            <Link href="/">
                                <a className="block py-2 px-4 hover:bg-gray-700" onClick={() => setOpen(false)}>
                                    Dashboard
                                </a>
                            </Link>
                        </li>
                        <li>
                            <Link href="/profile">
                                <a className="block py-2 px-4 hover:bg-gray-700" onClick={() => setOpen(false)}>
                                    Profile
                                </a>
                            </Link>
                        </li>
                        <li>
                            <Link href="/settings">
                                <a className="block py-2 px-4 hover:bg-gray-700" onClick={() => setOpen(false)}>
                                    Settings
                                </a>
                            </Link>
                        </li>
                        <li>
                            <Link href="/help">
                                <a className="block py-2 px-4 hover:bg-gray-700" onClick={() => setOpen(false)}>
                                    Help
                                </a>
                            </Link>
                        </li>
                    </ul>
                </DrawerContent>
            </Drawer>

            {/* Main Content Area */}
            <div className="flex-1 p-6">{/* Page content goes here */}</div>
        </div>
    );
};

export default Sidebar;