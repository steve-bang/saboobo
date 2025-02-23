"use client"

import * as React from "react"
import {
  AudioWaveform,
  BookOpen,
  Command,
  GalleryVerticalEnd,
  Images,
  Layers,
  LayoutDashboard,
  Package2,
  Settings2,
  ShoppingBag,
  Store,
  Wallet,
} from "lucide-react"
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarRail,
} from "@/components/ui/sidebar"
import { NavMain } from "./nav-main"
import { NavUser } from "./nav-user"
import { TeamSwitcher } from "./team-switcher"
import { useAppSelector } from "@/lib/store/store"
import { useRouter } from "next/navigation"


export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {

  const router = useRouter();

  // Get the merchant from the context
  const { user, merchant } = useAppSelector((state) => state);

  if (!user.data) {
    router.push("/sign-in");
  }

  // This is sample data.
  const data = {
    user: {
      name: user.data && user.data.name && user.data?.name !== '' ? user.data?.name : "Unnamed",
      email: user.data && user.data.email && user.data?.email !== '' ? user.data?.email : "example@email.com",
      avatar: user.data && user.data.avatarUrl && user.data?.avatarUrl !== '' ? user.data?.avatarUrl : "",
    },
    teams: [
      {
        name: merchant.merchant.name ?? "Untitled",
        logo: GalleryVerticalEnd,
        plan: merchant.merchant.address ?? "No address",
      }
    ],
    navMain: [
      {
        title: "Dashboard",
        url: "#",
        icon: LayoutDashboard,
        isActive: true,
      },
      {
        title: "Merchant",
        url: "#",
        icon: Store,
        items: [
          {
            title: "Category",
            url: "/category",
            icon: Layers,
          },
          {
            title: "Product",
            url: "/product",
            icon: Package2
          },
          {
            title: "Order (*)",
            url: "#",
            icon: ShoppingBag
          },
          {
            title: "Payment (*)",
            url: "#",
            icon: Wallet
          },
          {
            title: "Shipping (*)",
            url: "#",
            icon: Command
          },
          {
            title: "Promotion (*)",
            url: "#",
            icon: AudioWaveform
          },
          {
            title: "Banner",
            url: "/banner",
            icon: Images
          },
          {
            title: "Settings",
            url: "/merchant",
            icon: Settings2,
          },
        ],
      },
      {
        title: "Documentation",
        url: "#",
        icon: BookOpen,
        items: [
          {
            title: "Introduction",
            url: "#",
          },
          {
            title: "Get Started",
            url: "#",
          },
          {
            title: "Tutorials",
            url: "#",
          },
          {
            title: "Changelog",
            url: "#",
          },
        ],
      },
      {
        title: "Settings",
        url: "#",
        icon: Settings2,
        items: [
          {
            title: "General",
            url: "#",
          },
          {
            title: "Team",
            url: "#",
          },
          {
            title: "Billing",
            url: "#",
          },
          {
            title: "Limits",
            url: "#",
          },
        ],
      },
    ]
  }

  return (
    <Sidebar collapsible="icon" {...props}>
      <SidebarHeader>
        <TeamSwitcher teams={data.teams} />
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={data.navMain} />
      </SidebarContent>
      <SidebarFooter>
        <NavUser user={data.user} />
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  )
}
