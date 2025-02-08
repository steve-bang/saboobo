"use client";

import { useEffect, useState } from "react";
import { ColumnDef } from "@tanstack/react-table";
import { IBannerType, BannerCreateParams, BannerUpdateParams } from "@/types/Banner";
import { useAppSelector } from "@/lib/store/store";
import { createBanner, deleteBanner, listBannersByMerchantId, updateBanner } from "@/lib/actions/banner.action";
import { Button } from "@/components/ui/button";
import { ChevronDown, ChevronUp, Edit, MoreHorizontal, Plus, Trash } from "lucide-react";
import { Spinner } from "@/components/ui/spinner";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { DataTable } from "./data-table";
import Image from "next/image";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { IFileType } from "@/types/Common";
import { uploadFile } from "@/lib/actions/media.action";
import { useToast } from "@/hooks/use-toast";

export enum ItemType {
  BANNER = "BANNER",
}

export enum DialogType {
  CREATE = "CREATE",
  EDIT = "EDIT",
}

const BannerManagementPage = () => {

  const { toast } = useToast();

  const merchantState = useAppSelector(state => state.merchant.merchant); // Example of getting merchantId from user store

  const [banners, setBanners] = useState<IBannerType[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [newBanner, setNewBanner] = useState<BannerCreateParams>({
    name: "",
    imageUrl: "",
    link: "",
  });
  const [imageReview, setImagePreview] = useState<IFileType | null>(null);
  const [dialogType, setDialogType] = useState<DialogType>(DialogType.CREATE);

  const [isDialogOpen, setIsDialogOpen] = useState<boolean>(false);
  const [isImageModalOpen, setIsImageModalOpen] = useState<boolean>(false);
  const [selectedImageUrl, setSelectedImageUrl] = useState<string>("");
  const [isSaving, setIsSaving] = useState<boolean>(false);
  const [bannerSelected, setBannerSelected] = useState<IBannerType | null>(null);



  // Fetch banners when the component mounts
  useEffect(() => {
    if (!merchantState) return;

    const fetchBanners = async () => {
      try {
        setLoading(true);
        const fetchedBanners = await listBannersByMerchantId(merchantState.id);
        setBanners(fetchedBanners);
      } catch (err) {
        setError("Failed to load banners");
        toast({
          title: "Failed to load banners",
          variant: "destructive",
        });
      } finally {
        setLoading(false);
      }
    };

    fetchBanners();
  }, [merchantState]);

  const columnsCategory: ColumnDef<IBannerType>[] = [
    {
      accessorKey: "imageUrl",
      header: "Image",
      cell: ({ row }) => {
        const banner = row.original as IBannerType;
        return (
          <Image
            src={banner.imageUrl}
            alt={banner.name}
            className="object-cover cursor-move"
            width={100}
            height={100}
            onClick={() => handleImageClick(banner.imageUrl)} // Open modal on image click
          />
        );
      },
    },
    {
      accessorKey: "name",
      header: "Name",
    },
    {
      accessorKey: "link",
      header: "Link",
    },
    {
      id: "actions",
      cell: ({ row }) => {
        const banner = row.original as IBannerType;

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
                onClick={() => onClickEditBanner(banner)}
              >
                <Edit /> Edit
              </DropdownMenuItem>

              <DropdownMenuItem
                className="cursor-pointer"
                disabled={banner.id === banners[0].id}
                onClick={() => onClickMoveToTop(banner.id)}
              >
                <ChevronUp /> Move to Top
              </DropdownMenuItem>

              <DropdownMenuItem
                className="cursor-pointer"
                disabled={banner.id === banners[banners.length - 1].id}
                onClick={() => onClickMoveToBottom(banner.id)}
              >
                <ChevronDown /> Move to Bottom
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
                      Delete Banner
                    </DialogTitle>
                    <DialogDescription>
                      Are you sure you want to delete this banner? This action cannot be undone.
                    </DialogDescription>
                  </DialogHeader>
                  <DialogFooter>
                    <Button
                      className="bg-rose-500 hover:bg-rose-700"
                      onClick={() => handleDeleteBanner(banner.id)}
                    >
                      Delete
                    </Button>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
            </DropdownMenuContent>
          </DropdownMenu>
        );
      },
    },
  ];

  const handleCreateBanner = async () => {
    try {
      if (!newBanner) return;

      await createBanner(merchantState.id, newBanner);
      setNewBanner({ name: "", imageUrl: "", link: "" }); // Reset form
      const fetchedBanners = await listBannersByMerchantId(merchantState.id);
      setBanners(fetchedBanners);
      setIsDialogOpen(false); // Close the dialog after creating the banner

    } catch (err) {
      setError("Failed to create banner");
      toast({
        title: "Failed to create banner",
        variant: "destructive"
      });
    }
  };

  const handleSaveOnDialog = async () => {
    // update banner into list banners
    const updatedBanners = banners.map(banner => {
      if (banner.id === bannerSelected?.id) {
        return bannerSelected;
      }
      return banner;
    });

    setBanners(updatedBanners);

    setIsDialogOpen(false);
  }

  const handleDeleteBanner = async (id: string) => {
    try {
      await deleteBanner(merchantState.id, id);
      setBanners(banners.filter(banner => banner.id !== id)); // Update local state
    } catch (err) {
      setError("Failed to delete banner");
    }
  };

  const onClickSaveChanges = () => {

    // Convert IBannerType to array of BannerUpdateParams
    const updatedBanners = banners.map(banner => ({
      id: banner.id,
      name: banner.name,
      imageUrl: banner.imageUrl,
      link: banner.link,
    }));

    handleUpdateBanner(updatedBanners);
  };

  const handleUpdateBanner = async (updatedData: BannerUpdateParams[]) => {
    try {
      setIsSaving(true);
      await updateBanner(merchantState.id, updatedData);
      const fetchedBanners = await listBannersByMerchantId(merchantState.id);
      setBanners(fetchedBanners);
    } catch (err) {
      setError("Failed to update banner");
      toast({
        title: "Failed to update banner",
        variant: "destructive",
      });
    }
    finally {
      setIsSaving(false);
    }
  };

  const uploadImageFile = async (file: File) => {

    try {
      const data = await uploadFile(file);

      return data.url;
    } catch (error) {
      console.error("Error uploading logo:", error);
      toast({
        title: "Failed to upload logo",
        variant: "destructive",
      });
    }

  };


  const handleImageClick = (imageUrl: string) => {
    setSelectedImageUrl(imageUrl);
    setIsImageModalOpen(true); // Open the modal with the clicked image
  };

  const onClickMoveToTop = (id: string) => {
    const banner = banners.find(banner => banner.id === id);
    if (!banner) return;

    const updatedBanners = banners.filter(banner => banner.id !== id);
    setBanners([banner, ...updatedBanners]);
  }

  const onClickMoveToBottom = (id: string) => {
    const banner = banners.find(banner => banner.id === id);
    if (!banner) return;

    const updatedBanners = banners.filter(banner => banner.id !== id);
    setBanners([...updatedBanners, banner]);
  }

  // Handle file input change for logo and cover image
  const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {

      const imageUrlUpload = await uploadImageFile(file);

      if (dialogType === DialogType.CREATE) {
        setNewBanner({ ...newBanner, imageUrl: imageUrlUpload });
      }
      else {
        setBannerSelected({ ...bannerSelected, imageUrl: imageUrlUpload });
      }

      console.log("imageUrlUpload", imageUrlUpload);

      const objectUrl = URL.createObjectURL(file);
      setImagePreview({
        file: file,
        url: objectUrl,
      });
    }
  };

  const onClickCreateBanner = () => {
    setDialogType(DialogType.CREATE);
    setIsDialogOpen(true);
  }

  const onClickEditBanner = (banner: IBannerType) => {
    setDialogType(DialogType.EDIT);
    setBannerSelected(banner);
    setIsDialogOpen(true);
  }

  const onClickButton = async () => {

    if (dialogType === DialogType.CREATE) {
      await handleCreateBanner();
    }
    else {
      await handleSaveOnDialog();
    }
  }


  return (
    <div className="container mx-auto">
      <div className="flex justify-between items-center mb-2">
        <h1 className="title-page">Banner</h1>
        <Button onClick={onClickCreateBanner}> <Plus /> Create Banner</Button>
      </div>

      {loading ? (
        <Spinner />
      ) : (
        <DataTable columns={columnsCategory} data={banners} />
      )}

      <Button className="mt-4" onClick={onClickSaveChanges} disabled={!banners || banners.length === 0 || isSaving}>
        {isSaving ? <Spinner /> : "Save Changes"}
      </Button>

      {/* Image Modal */}
      <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
        <DialogTrigger asChild>
        </DialogTrigger>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>
              {
                dialogType === DialogType.CREATE ? "Create Banner" : "Edit Banner"
              }
            </DialogTitle>
            <DialogDescription>

              <div>
                <Label htmlFor="name">Name</Label>
                <Input
                  id="name"
                  type="text"
                  value={dialogType === DialogType.CREATE ? newBanner.name : bannerSelected?.name}
                  onChange={(e) =>
                    dialogType === DialogType.CREATE ? setNewBanner({ ...newBanner, name: e.target.value }) : setBannerSelected({ ...bannerSelected, name: e.target.value })
                  }
                />
              </div>

              <div>
                <Label htmlFor="link">Link</Label>
                <Input
                  id="link"
                  type="text"
                  value={dialogType === DialogType.CREATE ? newBanner.link : bannerSelected?.link}
                  onChange={(e) =>
                    dialogType === DialogType.CREATE ? setNewBanner({ ...newBanner, link: e.target.value }) : setBannerSelected({ ...bannerSelected, link: e.target.value })
                  }
                />
              </div>
              <div>
                <Label htmlFor="imageUrl">Image</Label>
                <Label htmlFor="imageUrl" className="flex items-center justify-center cursor-pointer h-48 w-96 border border-dashed border-gray-300 rounded-lg">
                  {
                    !imageReview ? (
                      dialogType === DialogType.EDIT && bannerSelected?.imageUrl ? (
                        <img
                          src={bannerSelected.imageUrl}
                          alt="Preview"
                          className="object-cover max-w-full max-h-44 rounded-lg"
                        />
                      ) : (
                        <span className="text-gray-400">Click to upload image</span>
                      )
                    ) : (
                      <img
                        src={imageReview.url}
                        alt="Preview"
                        className="object-cover max-w-full max-h-44 rounded-lg"
                      />
                    )
                  }


                  <Input
                    id="imageUrl"
                    type="file"
                    accept="image/*"
                    className="hidden"
                    onChange={(e) => handleFileChange(e)}
                  />
                </Label>

              </div>
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            <Button

              onClick={onClickButton}
            >
              {
                dialogType === DialogType.CREATE ? "Create" : "Save"
              }
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default BannerManagementPage;
