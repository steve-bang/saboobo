
"use client"

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { useToast } from "@/hooks/use-toast";
import { listCategoryByMerchantId } from "@/lib/actions/category.action";
import { uploadFile } from "@/lib/actions/media.action";
import { createProduct } from "@/lib/actions/product.action";
import { useAppSelector } from "@/lib/store/store";
import { formatPrice } from "@/lib/utils";
import { ICategoryType } from "@/types/Category";
import { zodResolver } from "@hookform/resolvers/zod";
import { UploadCloudIcon } from "lucide-react";
import Image from "next/image";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import * as z from "zod";

// Schema validation using Zod
const productSchema = z.object({
    categoryId: z.string().min(1, { message: "Category is required" }),
    name: z.string().min(1, { message: "Name is required" }),
    sku: z.string(),
    price: z.number().min(1, { message: "Price is required" }),
    description: z.string(),
    urlImage: z.string(),
    // toppings: z.array(
    //     z.object({
    //         name: z.string().min(1, { message: "Name is required" }),
    //         price: z.number().min(1, { message: "Price is required" }),
    //     })
    // ).optional(),
});


type ProductForm = z.infer<typeof productSchema>;

export default function CreateProduct() {

    const [imageReview, setImageReview] = useState<string | null>(null);

    const [categories, setCategories] = useState<ICategoryType[]>([]);

    const { toast } = useToast();

    // Get the merchant from the context
    const merchantState = useAppSelector((state) => state.merchant.merchant);

    useEffect(() => {
        const loadCategories = async () => {
            try {

                const data = await listCategoryByMerchantId(merchantState.id);
                setCategories(data);

            } catch (error) {
                console.error(error);

                //toast.error("Error fetching categories.");
            }
        };
        loadCategories();
    }, [merchantState.id]);

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
            urlImage: "",
            categoryId: "",
            //toppings: [],
        },
    });

    const price = watch("price");

    const handlePriceChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const rawValue = e.target.value.replace(/[^0-9]/g, "");
        const parsedValue = parseFloat(rawValue);
        setValue("price", isNaN(parsedValue) ? 0 : parsedValue);
    };

    const uploadImageFile = async (file: File) => {

        try {
            const data = await uploadFile(file);

            return data.url;
        } catch (error) {
            console.error("Error uploading image:", error);
            toast({
                title: "Failed to upload image",
                variant: "destructive",
            });
        }

    };

    const onSubmit = async (data: ProductForm) => {
        try {
            // Create product
            const result = await createProduct({
                merchantId: merchantState.id,
                categoryId: data.categoryId,
                name: data.name,
                sku: data.sku,
                price: data.price,
                description: data.description,
                urlImage: data.urlImage,
                toppings: [],
            });

            if (result) {
                toast({
                    title: "Product created successfully!",
                });

                // Reset the form
                setValue("name", "");
                setValue("sku", "");
                setValue("price", 0);
                setValue("description", "");
                setValue("urlImage", "");
                setValue("categoryId", "");
                setImageReview(null);

            }

        } catch (error) {

            console.error(error);
            toast({
                title: "Product creation failed!",
                variant: 'destructive'
            })
        }

    };

    // Handle file input change for logo and cover image
    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (file) {
            
            // Upload the image file
            uploadImageFile(file).then((url) => {
                if(url) setValue("urlImage", url);
            });

            const objectUrl = URL.createObjectURL(file);
            setImageReview(objectUrl);

        }
    };

    return (
        <>
            <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col space-y-4 w-full">
                <div className="grid grid-cols-2 gap-4">
                    {/* CategoryId Field */}
                    <div>
                        <Label htmlFor="categoryId">Category</Label>
                        <Select
                            onValueChange={(value) => setValue("categoryId", value)}
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
                            <Image src={imageReview} alt="Image" width={120} height={112} className="object-cover rounded-lg border border-dashed" style={{objectFit: "contain"}} />
                        ) : (
                            <div className="px-10 py-6 border border-dashed border-gray-300 rounded-lg flex items-center justify-center">
                                <UploadCloudIcon size={24} />
                            </div>
                        )}
                    </Label>


                </div>

                {/* Submit Button */}
                <div>
                    <Button type="submit">Create</Button>
                </div>
            </form>
        </>
    );

}