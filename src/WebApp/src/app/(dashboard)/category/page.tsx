"use client"

import { Button } from "@/components/ui/button";
import { DialogContent, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { createCategory, deleteCategory, listCategoryByMerchantId, updateCategory } from "@/lib/actions/category.action";
import { CategoryType } from "@/types/Category";
import { Dialog, DialogTrigger } from "@radix-ui/react-dialog";
import { useState, useEffect } from "react";
import { DataTable } from "./data-table";
import { columnsCategory } from "./columns";
import { Plus } from "lucide-react";
import { Spinner } from "@/components/ui/spinner";
import { useMerchantContext } from "@/lib/MerchantContext";


export default function Categories() {
  const [categories, setCategories] = useState<CategoryType[]>([]);
  const [newCategory, setNewCategory] = useState({
    name: "",
    code: "",
    description: "",
    iconUrl: "",
  });
  const [selectedCategory, setSelectedCategory] = useState<CategoryType | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  // Get the merchant from the context
  const { merchant } = useMerchantContext();

  useEffect(() => {
    const loadCategories = async () => {
      try {
        setIsLoading(true);

        const data = await listCategoryByMerchantId(merchant.id);
        setCategories(data);

        setIsLoading(false);
      } catch (error) {
        console.error(error);

        //toast.error("Error fetching categories.");
      }
    };
    loadCategories();
  }, []);

  const handleCreateCategory = async () => {
    try {
      await createCategory(merchant.id, newCategory);
      //toast.success("Category created!");
      setIsModalOpen(false);
      setNewCategory({
        name: "",
        code: "",
        description: "",
        iconUrl: "",
      });
      // Reload categories
      const data = await listCategoryByMerchantId(merchant.id);
      setCategories(data);
    } catch (error) {
      console.error(error);
      //toast.error("Error creating category.");
    }
  };

  const handleUpdateCategory = async (id: string) => {
    try {
      await updateCategory(merchant.id, id, newCategory);
      //toast.success("Category updated!");
      setIsModalOpen(false);
      // Reload categories
      const data = await listCategoryByMerchantId(merchant.id);
      setCategories(data);
    } catch (error) {
      //toast.error("Error updating category.");
    }
  };

  const handleDeleteCategory = async (id: string) => {
    try {
      await deleteCategory(merchant.id, id);
      //toast.success("Category deleted!");
      // Reload categories
      const data = await listCategoryByMerchantId(merchant.id);
      setCategories(data);
    } catch (error) {
      console.error(error);
      //toast.error("Error deleting category.");
    }
  };

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
