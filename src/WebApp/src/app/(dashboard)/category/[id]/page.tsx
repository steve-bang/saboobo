"use client"

import { Button } from "@/components/ui/button";
import { ICategoryType } from "@/types/Category";
import React, { useEffect, useState } from "react";
import * as z from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { getCategoryById, updateCategory } from "@/lib/actions/category.action";
import { useToast } from "@/hooks/use-toast";
import { Spinner } from "@/components/ui/spinner";
import { useAppSelector } from "@/lib/store/store";

// Schema validation using Zod
const categorySchema = z.object({
    code: z.string().min(1, { message: "Code is required" }).optional(),
    name: z.string().min(1, { message: "Name is required" }),
    description: z.string().min(1, { message: "Description is required" }),
    //iconUrl: z.string().url({ message: "Invalid icon URL" }).optional(),
});

type CategoryForm = z.infer<typeof categorySchema>;

export default function Page({
    params,
}: {
    params: Promise<{ id: string }>
}) {
    const { id } = React.use(params);
    const [, setCategory] = useState<ICategoryType>();
    const { toast } = useToast();
    const [isLoading, setIsLoading] = useState(true);

    // Get the merchant from the context
    const merchantState = useAppSelector((state) => state.merchant.merchant);

    const {
        register,
        handleSubmit,
        formState: { errors },
        setValue,
    } = useForm<CategoryForm>({
        resolver: zodResolver(categorySchema),
        defaultValues: {
            code: "",
            name: "",
            description: "",
        },
    });

    useEffect(() => {
        async function fetchCategory() {
            try {
    
                setIsLoading(true);
    
                const category = await getCategoryById(merchantState.id, id);
                setCategory(category);
    
                // Update form values after fetching the category
                if (category) {
                    setValue("code", category.code ?? "");
                    setValue("name", category.name);
                    setValue("description", category.description ?? "");
    
                    console.log("Fetched category:", category); // Log the fetched category data
    
                    setIsLoading(false);
                }
            } catch (error) {
                console.error("Error fetching category:", error);
                toast({
                    title: "Error fetching category"
                })
            }
        }

        fetchCategory();
    }, [id, setValue, merchantState.id, toast]); // Add id to dependencies to refetch when it's updated


    const onClickSubmit = async (data: CategoryForm) => {
        if (Object.keys(errors).length > 0) {
            console.log("Form validation errors:", errors); // Log any validation errors
            return;
        }

        try {
            console.log("Submitting form with data:", data); // Log the data that is being submitted
            // Here, you can make your API call to save the updated category data
            await updateCategory(merchantState.id, id,
                {
                    code: data.code ?? null,
                    name: data.name,
                    description: data.description,
                    iconUrl: null,
                });

            toast({
                title: 'Category updated successfully!'
            })
        } catch (error) {
            console.error("Error updating category:", error);
            toast({
                title: "Error updating category"
            })
        }
    }

    return (
        <div>
            <div className="mb-4">
                <h1 className="text-xl font-semibold">Category Details</h1>
            </div>
            {
                isLoading ? <Spinner /> : (
                    <form onSubmit={handleSubmit(onClickSubmit)} className="flex flex-col space-y-4 w-full">
                        {/* Code Field */}
                        <div>
                            <Label htmlFor="code">Code</Label>
                            <Input
                                id="code"
                                type="text"
                                {...register("code")}
                                className={errors.code ? "border-red-500" : ""}
                            />
                            {errors.code && <span className="text-sm text-red-500">{errors.code.message}</span>}
                        </div>

                        {/* Name Field */}
                        <div>
                            <Label htmlFor="name">Name</Label>
                            <Input
                                id="name"
                                type="text"
                                {...register("name")}
                                className={errors.name ? "border-red-500" : ""}
                            />
                            {errors.name && <span className="text-sm text-red-500">{errors.name.message}</span>}
                        </div>

                        {/* Description Field */}
                        <div>
                            <Label htmlFor="description">Description</Label>
                            <Input
                                id="description"
                                type="text"
                                placeholder="This is a description"
                                {...register("description")}
                                className={errors.description ? "border-red-500" : ""}
                            />
                            {errors.description && <span className="text-sm text-red-500">{errors.description.message}</span>}
                        </div>

                        {/* Submit Button */}
                        <div>
                            <Button type="submit">Save</Button>
                        </div>
                    </form>
                )
            }
        </div>
    );
}
