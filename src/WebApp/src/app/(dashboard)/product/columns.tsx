
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
import { IProductType } from "@/types/Product";
import { redirect } from "next/navigation";

export const columnsProduct: ColumnDef<IProductType>[] = [
    {
        accessorKey: "name",
        header: "Name",
        cell: ({ row }) => {

            const product = row.original as IProductType;

            return <div className="font-medium flex items-center gap-2">
                {
                    product.urlImage &&
                    <img src={product.urlImage} alt={product.name} className="w-12 h-12 rounded-md" />
                }
                {product.name}
            </div>
        }
    },
    {
        accessorKey: "sku",
        header: "Sku",
    },
    {
        accessorKey: "price",
        header: "Price",
        cell: ({ row }) => {
            const amount = parseInt(row.getValue("price"))
            const formatted = new Intl.NumberFormat("vi-VN", {
                style: "currency",
                currency: "VND",
            }).format(amount)

            return <div className="font-medium">{formatted}</div>
        },
    },
    {
        accessorKey: "description",
        header: "Description",
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
            const payment = row.original as IProductType;

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
                            onClick={() => redirect(`/product/${payment.id}`)}
                        >
                            Edit
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