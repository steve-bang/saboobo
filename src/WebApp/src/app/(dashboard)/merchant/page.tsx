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
import { IMerchantType } from "@/types/Merchant";
import { useToast } from "@/hooks/use-toast";
import { coverImageDefault } from "@/constants/Common";
import { store, useAppDispatch, useAppSelector } from "@/lib/store/store";
import { setMerchant } from "@/lib/store/merchantSlice";
import { Spinner } from "@/components/ui/spinner";
import { IFileType } from "@/types/Common";
import { uploadFile } from "@/lib/actions/media.action";

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
  const [logoPreview, setLogoPreview] = useState<IFileType | null>(null);
  const [coverImagePreview, setCoverImagePreview] = useState<IFileType | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [merchantUser, setMerchantUser] = useState<IMerchantType | null>(null);

  const merchantState = useAppSelector((state) => state.merchant.merchant);
  const dispatch = useAppDispatch();
  const { toast } = useToast();

  console.log('state store', store.getState())

  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<MerchantForm>({
    resolver: zodResolver(merchantSchema),
    defaultValues: {
      name: merchantState?.name ?? "",
      phoneNumber: merchantState?.phoneNumber ?? "",
      emailAddress: merchantState?.emailAddress ?? "",
      description: merchantState?.description ?? "",
      address: merchantState?.address ?? "",
      oaUrl: merchantState?.oaUrl ?? "",
      website: merchantState?.website ?? "",
    },
  });

  useEffect(() => {
    async function fetchMerchants() {
      try {
        setIsLoading(true);
        const merchant = await getMerchantsByUserLogged();
        setMerchantUser(merchant);
        dispatch(setMerchant(merchant));

        // Update form values after fetching the merchant
        if (merchant) {
          setValue("name", merchant.name);
          setValue("phoneNumber", merchant.phoneNumber);
          setValue("emailAddress", merchant.emailAddress ?? "");
          setValue("description", merchant.description ?? "");
          setValue("address", merchant.address ?? "");
          setValue("oaUrl", merchant.oaUrl ?? "");
          setValue("website", merchant.website ?? "");
        }
      } catch (error) {
        console.error("Error fetching merchant:", error);
        toast({
          title: "Failed to load merchant!",
          variant: "destructive",
        });
      } finally {
        setIsLoading(false);
      }
    }
    fetchMerchants();
  }, [setValue, dispatch]);

  // Handle file input change for logo and cover image
  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>, field: keyof MerchantForm) => {
    const file = e.target.files?.[0];
    if (file) {
      const objectUrl = URL.createObjectURL(file);
      if (field === "logo") {
        setLogoPreview({
          file: file,
          url: objectUrl,
        });
      } else if (field === "coverUrl") {
        setCoverImagePreview({
          file: file,
          url: objectUrl,
        });
      }
      setValue(field, file); // Update form state
    }
  };

  // Form submission handler
  const onSubmit = async (data: MerchantForm) => {
    if (!merchantUser) return;

    try {
      // Update the merchant (logo and cover images should be handled separately)
      const logoUrlUploaded = await uploadLogoFile();
      const coverImageUrlUploaded = await uploadCoverImageFile();

      const updatedMerchant = await updateMerchant(merchantUser.id, {
        name: data.name,
        phoneNumber: data.phoneNumber,
        emailAddress: data.emailAddress,
        description: data.description,
        address: data.address,
        oaUrl: data.oaUrl,
        website: data.website,
        logoUrl: logoUrlUploaded, // Use preview or previous value
        coverUrl: coverImageUrlUploaded, // Use preview or previous value
      });

      // Notify success
      toast({
        title: "Merchant updated successfully!",
      });

      // Refresh data after successful update
      setMerchantUser(updatedMerchant);
    } catch (error) {
      console.error("Error updating merchant:", error);
      toast({
        title: "Failed to update merchant",
        variant: "destructive",
      });
    }

    // Clear preview after submission
    setLogoPreview(null);
    setCoverImagePreview(null);
  };

  const uploadLogoFile = async () => {
    let logoUrlUploaded = merchantState.logoUrl;
    if (logoPreview?.file) {
      try {
        const data = await uploadFile(logoPreview.file);
        logoUrlUploaded = data.url;
      } catch (error) {
        console.error("Error uploading logo:", error);
        toast({
          title: "Failed to upload logo",
          variant: "destructive",
        });
      }
    }
    return logoUrlUploaded;
  };

  const uploadCoverImageFile = async () => {
    let coverImageUrlUploaded = merchantState.coverUrl;
    if (coverImagePreview?.file) {
      try {
        const data = await uploadFile(coverImagePreview.file);
        coverImageUrlUploaded = data.url;
      } catch (error) {
        console.error("Error uploading cover image:", error);
        toast({
          title: "Failed to upload cover image",
          variant: "destructive",
        });
      }
    }
    return coverImageUrlUploaded;
  };

  return (
    <div>
      <div className="mb-4">
        <h1 className="title-page">Merchant</h1>
      </div>

      {
        isLoading ? (
          <Spinner />
        ) : (
          <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col space-y-4 w-full">

            <div className="flex flex-col md:flex-row md:space-x-4 space-y-4 md:space-y-0">
              {/* Logo Field */}
              <div className="w-1/5 flex items-center justify-center">
                <div>
                  <Label>Logo</Label>
                  <div className="flex items-center space-x-2">
                    <Label htmlFor="logo" className="cursor-pointer px-4 py-2 border border-dashed border-gray-300 rounded-lg">
                      <Avatar
                        className="w-20 h-20"
                      >
                        {logoPreview || merchantUser?.logoUrl ? (
                          <AvatarImage src={
                            logoPreview?.url || merchantUser?.logoUrl
                          } alt="Logo" />
                        ) : (
                          <AvatarFallback className="w-20 h-20">L</AvatarFallback>
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
              </div>

              <div className="w-4/5">
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
              </div>
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
                  <AspectRatio ratio={8} className="w-64">
                    <Image
                      src={
                        coverImagePreview?.url || merchantUser?.coverUrl || coverImageDefault
                      }
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
              <Button type="submit">
                Save
              </Button>
            </div>
          </form>
        )
      }
    </div>
  );

}
