"use client"

import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";
import Link from "next/link";
import { DataTable } from "./data-table";
import { useCallback, useEffect, useState } from "react";
import { ICategoryType } from "@/types/Category";
import { listCategoryByMerchantId } from "@/lib/actions/category.action";
import { IProductType } from "@/types/Product";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Label } from "@/components/ui/label";
import { Spinner } from "@/components/ui/spinner";
import { deleteProduct, listProduct } from "@/lib/actions/product.action";
import { ColumnDef } from "@tanstack/react-table";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { MoreHorizontal } from "lucide-react";
import { redirect } from "next/navigation";
import { useToast } from "@/hooks/use-toast";
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { useAppSelector } from "@/lib/store/store";
import Image from "next/image";
import { Badge } from "@/components/ui/badge";


export default function Product() {

  const [, setSelectedImage] = useState<string>("");
  const [, setIsImageModalOpen] = useState<boolean>(false);

  const [categories, setCategories] = useState<ICategoryType[]>([]);
  const [products, setProducts] = useState<IProductType[]>([]);

  const [categorySelected, setCategorySelected] = useState<string | null>(null);

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const { toast } = useToast();

  // Get the merchant from the context
  const merchantState = useAppSelector((state) => state.merchant.merchant);

  const columnsProduct: ColumnDef<IProductType>[] = [
    {
      accessorKey: "name",
      header: "Name",
      cell: ({ row }) => {

        const product = row.original as IProductType;

        return <div className="font-medium flex items-center gap-2">
          {
            product.urlImage &&
            <Image 
              src={product.urlImage} 
              alt={product.name} 
              className="rounded-md cursor-pointer" 
              width={60} 
              height={60} 
              style={{ objectFit: "contain" }} 
              onClick={() => handleImageClick(product.urlImage)}
              />
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
        const product = row.original as IProductType;

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
                onClick={() => redirect(`/product/${product.id}`)}
              >
                Edit
              </DropdownMenuItem>
              <Dialog>
                <DialogTrigger asChild>
                  <DropdownMenuItem
                    className="text-rose-600 focus:text-rose-700 cursor-pointer focus:bg-rose-50"
                    onSelect={(e) => e.preventDefault()}
                  >
                    Delete
                  </DropdownMenuItem>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle>
                      Delete Product
                    </DialogTitle>
                    <DialogDescription>
                      Are you sure you want to delete this <b>{product.name}</b> product? This action cannot be undone.
                    </DialogDescription>
                  </DialogHeader>
                  <DialogFooter>
                    <Button
                      className="bg-rose-500 hover:bg-rose-700"
                      onClick={() => {
                        handleDeleteProduct(product.id);
                      }}
                    >
                      Delete
                    </Button>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
            </DropdownMenuContent>
          </DropdownMenu>
        )
      },
    },

  ];

  const handleDeleteProduct = async (id: string) => {
    try {

      await deleteProduct(id);

      toast({
        title: "Product deleted successfully",
        description: "The product has been deleted",
      });

      // Reload the products
      loadProducts();

    } catch (error) {
      console.error(error);
      toast({
        title: "Failed to delete product",
        description: "An error occurred while deleting the product",
        variant: "destructive"
      });
    }
  }

  const handleImageClick = (imageUrl: string) => {
    setSelectedImage(imageUrl);
    setIsImageModalOpen(true); // Open the modal with the clicked image
  };

  const loadProducts = useCallback(async () => {
    try {
      setIsLoading(true);
      const data = await listProduct({
        merchantId: merchantState.id,
        categoryId: categorySelected ?? undefined,
      });
      setProducts(data);
      setIsLoading(false);
    } catch (error) {
      console.error(error);
    }
  }, [merchantState.id, categorySelected]);

  useEffect(() => {
    const loadCategories = async () => {
      try {
        const data = await listCategoryByMerchantId(merchantState.id);
        setCategories(data);

      } catch (error) {
        console.error(error);
      }
    };

    loadCategories();
    loadProducts();

  }, [merchantState.id, categorySelected, loadProducts]);


  return (
    <div>
      <div className="flex justify-between items-center">
        <h1 className="title-page">Products</h1>
        <div>
          <Button>
            <Link href={'/product/create'} className="flex gap-2 items-center"><Plus />  Create Product</Link>
          </Button>
        </div>
      </div>
      <div>
        <div className="filter">
          <Label>Category</Label>
          <Select
            onValueChange={(value) => {

              if (value === "all")
                setCategorySelected(null);
              else
                setCategorySelected(value);

              loadProducts();

            }}
          >
            <SelectTrigger className="w-48">
              <SelectValue placeholder="All Categories" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem key='all' value={"all"}>
                All Categories
              </SelectItem>
              {categories.map((category) => (
                <SelectItem key={category.id} value={category.id}>
                  {category.name} <Badge variant={'secondary'}>{category.totalProduct ?? 0}</Badge>
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>
        {/* Data Table */}
        {
          isLoading ? (
            <Spinner />
          ) : (
            <DataTable
              columns={columnsProduct}
              data={products}
            />
          )
        }
      </div>
    </div>
  );
}