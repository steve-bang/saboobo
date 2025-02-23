import React, { FC } from "react";
import { Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import { Box } from "zmp-ui"; // Assuming this is a UI component
import { useBanners } from "../use-merchant";
import { MerchantPageLoading } from "./merchant-page-loading";

export const Banner: FC = () => {
  const { data: banners, isLoading: bannersLoading } = useBanners();

  const isLoading = bannersLoading;

  if (isLoading) return <MerchantPageLoading />

  return (
    <Box className="bg-white flex items-center justify-center">

      <Swiper
        modules={[Pagination]}
        slidesPerView={1}
        pagination={{
          clickable: true,
        }}
        autoplay={{ delay: 3000 }}
        loop
      >
        {banners?.map((i, index) => {
          const banner = i.imageUrl;  // Ensure the image URL is correct

          return (
            <SwiperSlide key={index} className="px-4">
              <Box className="">
                <img
                  src={banner}
                  alt={i.name}
                  className="w-full h-auto max-h-full rounded-lg bg-cover bg-center bg-skeleton object-contain mx-auto"
                />
              </Box>
            </SwiperSlide>
          );
        })}
      </Swiper>

    </Box>
  );
};
