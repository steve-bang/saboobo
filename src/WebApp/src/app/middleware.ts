import { CookieKey } from "@/constants/CookieKey";
import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

// Define paths that are allowed without the accessToken
const publicPaths = ["/sign-in", "/sign-up", "/forgot-password"]; // Update paths as needed

export function middleware(req: NextRequest) {
    // Check if the requested URL matches any public paths
    const { pathname } = req.nextUrl;

    // Allow access to public paths without token
    if (publicPaths.some((path) => pathname.startsWith(path))) {
        return NextResponse.next();
    }

    // Check if the accessToken cookie exists
    const accessToken = req.cookies.get(CookieKey.accessToken);

    // If accessToken is not found, redirect to sign-in page
    if (!accessToken) {
        const url = req.nextUrl.clone();
        url.pathname = "/sign-in";  // Redirect to your sign-in page

        console.log("Redirecting to sign-in page");
        return NextResponse.redirect(url);
    }

    // If accessToken exists, continue processing the request
    return NextResponse.next();
}
