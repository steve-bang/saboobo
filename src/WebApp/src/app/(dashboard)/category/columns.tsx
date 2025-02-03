
import { CategoryType } from "@/types/Category";
import { ColumnDef } from "@tanstack/react-table";

import { Button } from "@/components/ui/button"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { MoreHorizontal } from "lucide-react";
import Link from "next/link";
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { AvatarImage } from "@radix-ui/react-avatar";

export const columnsCategory: ColumnDef<CategoryType>[] = [
    {
        accessorKey: "iconUrl",
        header: "Icon",
        cell: ({ row }) => {
            const category = row.original as CategoryType;

            return (
                <Avatar className="scale-75">
                    <AvatarImage src={category.iconUrl} alt={category.name} />
                    <AvatarFallback>U</AvatarFallback>
                </Avatar>
            )
        },
    },
    {
        accessorKey: "name",
        header: "Name",
    },
    {
        accessorKey: "description",
        header: "Description",
    },
    {
        accessorKey: "totalProduct",
        header: "Product",
    },
    {
        accessorKey: "createdDate",
        header: "Created At",
        accessorFn: (data) => {
            return new Date(data.createdDate).toLocaleDateString();
        }
    },
    {
        id: "actions",
        cell: ({ row }) => {
            const payment = row.original as CategoryType;

            return (
                <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                        <Button variant="ghost" className="h-8 w-8 p-0">
                            <span className="sr-only">Open menu</span>
                            <MoreHorizontal className="h-4 w-4" />
                        </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                        <DropdownMenuLabel>Actions</DropdownMenuLabel>
                        <DropdownMenuItem
                            className="cursor-pointer"
                        >
                            <Link href={`/category/${payment.id}`} target="_blank">
                                Edit
                            </Link>
                        </DropdownMenuItem>
                        <DropdownMenuItem
                            className="text-rose-600 focus:text-rose-700 cursor-pointer focus:bg-rose-50"
                        >
                            Delete
                        </DropdownMenuItem>
                    </DropdownMenuContent>
                </DropdownMenu>
            )
        },
    },

];