"use client"

import { Button } from "@/components/ui/button";
import { ColumnDef } from "@tanstack/react-table";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { createCategory, deleteCategory, listCategoryByMerchantId, updateCategory } from "@/lib/actions/category.action";
import { ICategoryType } from "@/types/Category";
import { useState, useEffect } from "react";
import { DataTable } from "./data-table";
import { Edit, Plus, Trash } from "lucide-react";
import { Spinner } from "@/components/ui/spinner";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { MoreHorizontal } from "lucide-react";
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { AvatarImage } from "@radix-ui/react-avatar";
import { redirect } from "next/navigation";
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { useAppSelector } from "@/lib/store/store";
import { useToast } from "@/hooks/use-toast";


export default function Categories() {

  const { toast } = useToast();

  const [categories, setCategories] = useState<ICategoryType[]>([]);
  const [newCategory, setNewCategory] = useState({
    name: "",
    code: "",
    description: "",
    iconUrl: "",
  });
  const [selectedCategory, ] = useState<ICategoryType | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  // Get the merchant from the context
  const merchantState = useAppSelector((state) => state.merchant.merchant);

  useEffect(() => {
    const loadCategories = async () => {
      if (merchantState.id && merchantState.id !== '') {
        try {
          setIsLoading(true);

          const data = await listCategoryByMerchantId(merchantState.id);

          setCategories(data);
        } catch (error) {
          console.error(error);
          
          toast({
            title: "Failed to fetch categories!",
            variant: "destructive"
          })
        }
        finally {
          setIsLoading(false);
        }
      }
    };
    loadCategories();
  }, [merchantState]);


  const handleCreateCategory = async () => {
    try {
      await createCategory(merchantState.id, newCategory);
      //toast.success("Category created!");
      setIsModalOpen(false);
      setNewCategory({
        name: "",
        code: "",
        description: "",
        iconUrl: "",
      });
      // Reload categories
      const data = await listCategoryByMerchantId(merchantState.id);
      setCategories(data);
    } catch (error) {
      console.error(error);
      //toast.error("Error creating category.");
    }
  };

  const handleUpdateCategory = async (id: string) => {
    try {
      await updateCategory(merchantState.id, id, newCategory);
      //toast.success("Category updated!");
      setIsModalOpen(false);
      // Reload categories
      const data = await listCategoryByMerchantId(merchantState.id);
      setCategories(data);
    } catch (error) {
      //toast.error("Error updating category.");
      console.error(error);
    }
  };

  const handleDeleteCategory = async (id: string) => {
    try {
      await deleteCategory(merchantState.id, id);
      //toast.success("Category deleted!");
      // Reload categories
      const data = await listCategoryByMerchantId(merchantState.id);
      setCategories(data);
    } catch (error) {
      console.error(error);
      //toast.error("Error deleting category.");
    }
  };

  const columnsCategory: ColumnDef<ICategoryType>[] = [
    {
      accessorKey: "iconUrl",
      header: "Icon",
      cell: ({ row }) => {
        const category = row.original as ICategoryType;

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
        const categoryId = row.original as ICategoryType;

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
                onClick={() => redirect(`/category/${categoryId.id}`)}
              >
                <Edit /> Edit
              </DropdownMenuItem>

              <Dialog>
                <DialogTrigger asChild>
                  <DropdownMenuItem
                    className="text-rose-600 focus:text-rose-700 cursor-pointer focus:bg-rose-50"
                    onSelect={(e) => e.preventDefault()}
                  >
                    <Trash /> Delete
                  </DropdownMenuItem>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                  <DialogHeader>
                    <DialogTitle>
                      Delete Category
                    </DialogTitle>
                    <DialogDescription>
                      Are you sure you want to delete this category? This action cannot be undone.
                    </DialogDescription>
                  </DialogHeader>
                  <DialogFooter>
                    <Button
                      className="bg-rose-500 hover:bg-rose-700"
                      onClick={() => {
                        handleDeleteCategory(categoryId.id);
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

  return (
    <div>
      <div className="flex justify-between items-center">
        <h1 className="title-page">Categories</h1>
        <Button onClick={() => setIsModalOpen(true)}> <Plus /> Create Category</Button>
      </div>
      <div className="container mx-auto py-10">
        {
          isLoading ? <Spinner /> : <DataTable columns={columnsCategory} data={categories} />
        }
      </div>

      {/* Modal for creating/editing categories */}
      <Dialog open={isModalOpen} onOpenChange={setIsModalOpen}>
        <DialogTrigger asChild>
        </DialogTrigger>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>{selectedCategory ? "Edit Category" : "Create Category"}</DialogTitle>
          </DialogHeader>

          <div className="space-y-4">
            <div>
              <Label>Name</Label>
              <Input
                type="text"
                value={newCategory.name}
                onChange={(e) =>
                  setNewCategory({ ...newCategory, name: e.target.value })
                }
              />
            </div>
            <div>
              <Label>Code</Label>
              <Input
                type="text"
                value={newCategory.code}
                onChange={(e) =>
                  setNewCategory({ ...newCategory, code: e.target.value })
                }
              />
            </div>
            <div>
              <Label>Description</Label>
              <Textarea
                value={newCategory.description}
                onChange={(e) =>
                  setNewCategory({
                    ...newCategory,
                    description: e.target.value,
                  })
                }
              />
            </div>
            <div>
              <Label>Icon URL</Label>
              <Input
                type="text"
                value={newCategory.iconUrl}
                onChange={(e) =>
                  setNewCategory({ ...newCategory, iconUrl: e.target.value })
                }
              />
            </div>
          </div>

          <DialogFooter>
            <Button
              onClick={() =>
                selectedCategory
                  ? handleUpdateCategory(selectedCategory.id)
                  : handleCreateCategory()
              }
            >
              {selectedCategory ? "Update" : "Create"}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
