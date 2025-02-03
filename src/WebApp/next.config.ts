import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  reactStrictMode: true,
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "images.unsplash.com",
        pathname: "**"
      },
      {
        protocol: "https",
        hostname: "www.google.com",
        pathname: "**"
      },
    ],
  },

};

export default nextConfig;
