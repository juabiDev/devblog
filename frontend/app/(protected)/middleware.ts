import { NextRequest, NextResponse } from "next/server"

export function middleware(request: NextRequest) {
    const cookie = request.cookies.get("devblog_auth");

    // especific protected routes
    const protectedRoutes = ["/dashboard", "/profile"];

    const isProtected = protectedRoutes.some((path) => {
        return request.nextUrl.pathname.startsWith(path);
    })

    if(isProtected && !cookie) {
        return NextResponse.redirect(new URL("/login", request.url));
    }

    return NextResponse.next();
}

export const config = {
    matcher: ["/dashboard/:path*", "/profile/:path*"],
}