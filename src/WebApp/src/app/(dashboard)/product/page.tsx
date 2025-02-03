"use client"

import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";
import { Metadata } from "next";
import Link from "next/link";
import { DataTable } from "./data-table";
import { columnsProduct } from "./columns";
import { useEffect, useState } from "react";
import { CategoryType } from "@/types/Category";
import { listCategoryByMerchantId } from "@/lib/actions/category.action";
import { ProductType } from "@/types/Product";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Label } from "@/components/ui/label";
import { Spinner } from "@/components/ui/spinner";
import { listProduct } from "@/lib/actions/product.action";
import { useMerchantContext } from "@/lib/MerchantContext";


export default function Product() {

  const [categories, setCategories] = useState<CategoryType[]>([]);
  const [products, setProducts] = useState<ProductType[]>([]);

  const [categorySelected, setCategorySelected] = useState<string | null>(null);

  const [isLoading, setIsLoading] = useState<boolean>(false);

  // Get the merchant from the context
  const { merchant } = useMerchantContext();

  useEffect(() => {
    const loadCategories = async () => {
      try {
        const data = await listCategoryByMerchantId(merchant.id);
        setCategories(data);

      } catch (error) {
        console.error(error);
      }
    };

    const loadProducts = async () => {
      try {

        setIsLoading(true);
        const data = await listProduct();
        setProducts(data);

        setIsLoading(false);

      } catch (error) {
        console.error(error);
      }
    }

    loadCategories();
    loadProducts();

  }, []);

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
            onValueChange={(value) => setCategorySelected(value)}
          >
            <SelectTrigger className="w-48">
              <SelectValue placeholder="Select Category" />
            </SelectTrigger>
            <SelectContent>
              {categories.map((category) => (
                <SelectItem key={category.id} value={category.id}>
                  {category.name}
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