"use client";

import { AspectRatio } from "@/components/ui/aspect-ratio";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import Image from "next/image";
import { useEffect, useState } from "react";
import * as z from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { getMerchantsByUserLogged, updateMerchant } from "@/lib/actions/merchant.action";
import { MerchantType } from "@/types/Merchant";
import { useToast } from "@/hooks/use-toast";

const coverImageDefault =
  "https://images.unsplash.com/photo-1588345921523-c2dcdb7f1dcd?w=800&dpr=2&q=80";

// Schema validation using Zod
const merchantSchema = z.object({
  name: z.string().min(1, { message: "Name is required" }),
  phoneNumber: z.string().min(10, { message: "Phone number should be at least 10 digits" }),
  emailAddress: z
    .string()
    .refine((val) => val === "" || /\S+@\S+\.\S+/.test(val), { message: "Invalid email address" }),
  description: z.string().min(1, { message: "Description is required" }),
  address: z.string().min(1, { message: "Address is required" }),
  oaUrl: z.string().url({ message: "Invalid OA URL" }).optional(),
  website: z
    .string()
    .refine((val) => val === "" || /^https?:\/\/.+/i.test(val), { message: "Invalid website URL" })
    .optional(),
  logo: z.instanceof(File).optional(),
  coverUrl: z.instanceof(File).optional(),
});

type MerchantForm = z.infer<typeof merchantSchema>;

export default function Merchant() {
  const [logoPreview, setLogoPreview] = useState<string | null>(null);
  const [coverPreview, setCoverPreview] = useState<string | null>(null);

  const [merchantUser, setMerchantUser] = useState<MerchantType>();

  const { toast } = useToast();

  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<MerchantForm>({
    resolver: zodResolver(merchantSchema),
    defaultValues: {
      name: "",
      phoneNumber: "",
      emailAddress: "",
      description: "",
      address: "",
      oaUrl: "",
      website: "",
    },
  });

  useEffect(() => {
    async function fetchMerchants() {
      try {
        const merchant = await getMerchantsByUserLogged();
        setMerchantUser(merchant);

        // Update form values after fetching the merchant
        if (merchant) {
          setValue("name", merchant.name);
          setValue("phoneNumber", merchant?.phoneNumber);
          setValue("emailAddress", merchant.emailAddress ?? "");
          setValue("description", merchant.description ?? "");
          setValue("address", merchant.address ?? "");
          setValue("oaUrl", merchant.oaUrl ?? "");
          setValue("website", merchant.website ?? "");
        }
      } catch (error) {
        console.error("Error fetching merchant:", error);
      }
    }
    fetchMerchants();
  }, [setValue]);

  // Handle file input change for logo and cover image
  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>, field: keyof MerchantForm) => {
    const file = e.target.files?.[0];
    if (file) {
      const objectUrl = URL.createObjectURL(file);
      if (field === "logo") {
        setLogoPreview(objectUrl);
      } else if (field === "coverUrl") {
        setCoverPreview(objectUrl);
      }
      setValue(field, file); // Update form state
    }
  };

  // Form submission handler
  const onSubmit = async (data: MerchantForm) => {
    try {
      console.log("Form data:", data);

      // Update the merchant (logo and cover images should be handled separately)
      if (merchantUser) {
        const updatedMerchant = await updateMerchant(merchantUser.id, {
          name: data.name,
          phoneNumber: data.phoneNumber,
          emailAddress: data.emailAddress,
          description: data.description,
          address: data.address,
          oaUrl: data.oaUrl,
          website: data.website,
          logoUrl: logoPreview || merchantUser.logoUrl, // Use preview or previous value
          coverUrl: coverPreview || merchantUser.coverUrl, // Use preview or previous value
        });

        // Notify success
        toast({
          title: "Merchant updated successfully!"
        });

        // Refresh data after successful update
        setMerchantUser(updatedMerchant);
      }
    } catch (error) {
      console.error("Error updating merchant:", error);
      toast({
        title: "Failed to update merchant"
      });
    }

    // Clear preview after submission
    setLogoPreview(null);
    setCoverPreview(null);
  };

  return (
    <div className="px-2 py-4">
      <div className="mb-4">
        <h1 className="text-xl font-semibold">Merchant</h1>
      </div>

      <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col space-y-4 max-w-2xl">
        {/* Logo Field */}
        <div>
          <Label>Logo</Label>
          <div className="flex items-center space-x-2">
            <Label htmlFor="logo" className="cursor-pointer">
              <Avatar>
                {logoPreview || merchantUser?.logoUrl ? (
                  <AvatarImage src={logoPreview || merchantUser?.logoUrl} alt="Logo" />
                ) : (
                  <AvatarFallback>U</AvatarFallback>
                )}
              </Avatar>

              <Input
                id="logo"
                type="file"
                accept="image/*"
                className="hidden"
                onChange={(e) => handleFileChange(e, "logo")}
              />
            </Label>
            {errors.logo && <span className="text-sm text-red-500">{errors.logo.message}</span>}
          </div>
        </div>

        {/* Name Field */}
        <div>
          <Label htmlFor="name">Name</Label>
          <Input
            id="name"
            type="text"
            placeholder="John Doe"
            {...register("name")}
            className={errors.name ? "border-red-500" : ""}
          />
          {errors.name && <span className="text-sm text-red-500">{errors.name.message}</span>}
        </div>

        {/* Phone Number Field */}
        <div>
          <Label htmlFor="phoneNumber">Phone Number</Label>
          <Input
            id="phoneNumber"
            type="text"
            placeholder="08123456789"
            {...register("phoneNumber")}
            className={errors.phoneNumber ? "border-red-500" : ""}
          />
          {errors.phoneNumber && <span className="text-sm text-red-500">{errors.phoneNumber.message}</span>}
        </div>

        {/* Email Address Field */}
        <div>
          <Label htmlFor="emailAddress">Email Address</Label>
          <Input
            id="emailAddress"
            type="text"
            placeholder="example@gmail.com"
            {...register("emailAddress")}
            className={errors.emailAddress ? "border-red-500" : ""}
          />
          {errors.emailAddress && <span className="text-sm text-red-500">{errors.emailAddress.message}</span>}
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

        {/* Address Field */}
        <div>
          <Label htmlFor="address">Address</Label>
          <Input
            id="address"
            type="text"
            placeholder="Jl. Raya Bogor No. 1"
            {...register("address")}
            className={errors.address ? "border-red-500" : ""}
          />
          {errors.address && <span className="text-sm text-red-500">{errors.address.message}</span>}
        </div>

        {/* OA URL Field */}
        <div>
          <Label htmlFor="oaUrl">OA URL</Label>
          <Input
            id="oaUrl"
            type="text"
            placeholder="Enter OA URL"
            {...register("oaUrl")}
            className={errors.oaUrl ? "border-red-500" : ""}
          />
          {errors.oaUrl && <span className="text-sm text-red-500">{errors.oaUrl.message}</span>}
        </div>

        {/* Website Field */}
        <div>
          <Label htmlFor="website">Website</Label>
          <Input
            id="website"
            type="text"
            placeholder="Enter Website"
            {...register("website")}
            className={errors.website ? "border-red-500" : ""}
          />
          {errors.website && <span className="text-sm text-red-500">{errors.website.message}</span>}
        </div>

        {/* Cover Image Field */}
        <div>
          <Label htmlFor="coverUrl">Cover Image</Label>
          <div>
            <Label htmlFor="coverUrl" className="cursor-pointer">
              <AspectRatio ratio={5} className="w-64">
                <Image
                  src={coverPreview || merchantUser?.coverUrl || coverImageDefault}
                  alt="Cover Image"
                  fill
                  className="h-full w-full rounded-md object-cover"
                />
              </AspectRatio>
              <Input
                id="coverUrl"
                type="file"
                accept="image/*"
                className="hidden"
                onChange={(e) => handleFileChange(e, "coverUrl")}
              />
              {errors.coverUrl && <span className="text-sm text-red-500">{errors.coverUrl.message}</span>}
            </Label>
          </div>
        </div>

        {/* Submit Button */}
        <div>
          <Button type="submit">Save</Button>
        </div>
      </form>
    </div>
  );
}
