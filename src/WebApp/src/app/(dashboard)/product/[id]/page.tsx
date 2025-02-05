
"use client"

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Spinner } from "@/components/ui/spinner";
import { Textarea } from "@/components/ui/textarea";
import { useToast } from "@/hooks/use-toast";
import { listCategoryByMerchantId } from "@/lib/actions/category.action";
import { createProduct, getProductById, updateProduct } from "@/lib/actions/product.action";
import { useMerchantContext } from "@/lib/MerchantContext";
import { formatPrice } from "@/lib/utils";
import { CategoryType } from "@/types/Category";
import { ProductType } from "@/types/Product";
import { zodResolver } from "@hookform/resolvers/zod";
import { UploadCloudIcon } from "lucide-react";
import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import * as z from "zod";

// Schema validation using Zod
const productSchema = z.object({
    categoryId: z.string().min(1, { message: "Category is required" }),
    name: z.string().min(1, { message: "Name is required" }),
    sku: z.string(),
    price: z.number().min(1, { message: "Price is required" }),
    description: z.string(),
    //urlImage: z.string(),
    // toppings: z.array(
    //     z.object({
    //         name: z.string().min(1, { message: "Name is required" }),
    //         price: z.number().min(1, { message: "Price is required" }),
    //     })
    // ).optional(),
});


type ProductForm = z.infer<typeof productSchema>;

export default function EditProductById({
    params,
}: {
    params: Promise<{ id: string }>
}) {
    const { id } = React.use(params);

    const [imageReview, setImageReview] = useState<string | null>(null);

    const [categories, setCategories] = useState<CategoryType[]>([]);
    const [product, setProduct] = useState<ProductType>();
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const { toast } = useToast();

    // Get the merchant from the context
    const { merchant } = useMerchantContext();

    const {
        register,
        handleSubmit,
        formState: { errors },
        setValue,
        watch,
    } = useForm<ProductForm>({
        resolver: zodResolver(productSchema),
        defaultValues: {
            name: "",
            sku: "",
            price: 0,
            description: "",
            //urlImage: "",
            categoryId: "",
            //toppings: [],
        },
    });

    const loadProductById = async () => {
        try {
            setIsLoading(true);
            const data = await getProductById(id);
            setProduct(data);
            setValue("name", data.name);
            setValue("sku", data.sku ?? "");
            setValue("price", data.price);
            setValue("description", data.description);
            setValue("categoryId", data.categoryId);
            //setValue("urlImage", data.urlImage);
            //setValue("toppings", data.toppings);


        } catch (error) {
            console.error(error);
            toast({
                title: "Error fetching product",
                variant: 'destructive'
            })
        }
        setIsLoading(false);
    }

    useEffect(() => {
        const loadCategories = async () => {
            try {

                const data = await listCategoryByMerchantId(merchant.id);
                setCategories(data);

            } catch (error) {
                console.error(error);
            }
        };
        loadCategories();

        loadProductById();
    }, []);


    const price = watch("price");

    const handlePriceChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const rawValue = e.target.value.replace(/[^0-9]/g, "");
        const parsedValue = parseFloat(rawValue);
        setValue("price", isNaN(parsedValue) ? 0 : parsedValue);
    };

    const onSubmit = async (data: ProductForm) => {
        console.log("Data", data);

        try {
            // Create product
            var result = await updateProduct(id, {
                merchantId: merchant.id,
                categoryId: data.categoryId,
                name: data.name,
                sku: data.sku,
                price: data.price,
                description: data.description,
                urlImage: null, // default to null
                toppings: [], // default to empty array
            });

            if (result) {
                toast({
                    title: "Product updated successfully",
                });

            }

        } catch (error) {

            console.error(error);
            toast({
                title: "Product update failed",
                variant: 'destructive'
            })
        }

    };

    // Handle file input change for logo and cover image
    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) {
            const objectUrl = URL.createObjectURL(file);
            setImageReview(objectUrl);
        }
    };

    return (
        <>
            <h1 className="title-page">Edit Product</h1>
            {
                isLoading ? (
                    <Spinner />
                ) : (
                    <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col space-y-4 w-full">
                        <div className="grid grid-cols-2 gap-4">
                            {/* CategoryId Field */}
                            <div>
                                <Label htmlFor="categoryId">Category</Label>
                                <Select
                                    onValueChange={(value) => setValue("categoryId", value)}
                                    value={watch("categoryId")}
                                >
                                    <SelectTrigger className="w-full">
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
                                {errors.categoryId && (
                                    <span className="text-sm text-red-500">{errors.categoryId.message}</span>
                                )}
                            </div>

                            {/* Sku Field */}
                            <div>
                                <Label htmlFor="sku">Sku</Label>
                                <Input
                                    id="sku"
                                    type="text"
                                    {...register("sku")}
                                    className={errors.sku ? "border-red-500" : ""}
                                />
                                {errors.sku && <span className="text-sm text-red-500">{errors.sku.message}</span>}
                            </div>
                        </div>


                        {/* Name Field */}
                        <div>
                            <Label htmlFor="name">Name</Label>
                            <Input
                                id="name"
                                type="text"
                                placeholder="Name"
                                {...register("name")}
                                className={errors.name ? "border-red-500" : ""}
                            />
                            {errors.name && <span className="text-sm text-red-500">{errors.name.message}</span>}
                        </div>


                        {/* Price Field */}
                        <div>
                            <Label htmlFor="price">Price</Label>
                            <Input
                                id="price"
                                type="text"
                                value={formatPrice(price)}  // Display formatted currency in the input field
                                onChange={handlePriceChange}   // Handle price input changes
                                className={errors.price ? "border-red-500" : ""}
                            />
                            {errors.price && <span className="text-sm text-red-500">{errors.price.message}</span>}
                        </div>

                        {/* Description Field */}
                        <div>
                            <Label htmlFor="description">Description</Label>
                            <Textarea
                                id="description"
                                {...register("description")}
                                className={errors.description ? "border-red-500" : ""}
                            />
                            {errors.description && <span className="text-sm text-red-500">{errors.description.message}</span>}
                        </div>

                        {/* Description Field */}
                        <div className="max-w-28">
                            <Label htmlFor="description" >Image</Label>

                            <Label htmlFor="imageUrl" className="cursor-pointer" title="Click to upload image">
                                <Input
                                    id="imageUrl"
                                    type="file"
                                    accept="image/*"
                                    className="hidden"
                                    onChange={(e) => handleFileChange(e)}
                                />
                                {imageReview ? (
                                    <img src={imageReview} alt="Image" className="w-28 object-cover rounded-lg border border-dashed" />
                                ) : (
                                    <div className="px-8 py-4 border border-dashed border-gray-300 rounded-lg flex items-center justify-center">
                                        <UploadCloudIcon size={24} />
                                    </div>
                                )}
                            </Label>


                        </div>

                        {/* Submit Button */}
                        <div>
                            <Button type="submit">Save</Button>
                        </div>
                    </form>
                )
            }
        </>
    );

}