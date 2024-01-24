import { authOptions } from "@/app/utils/authOptions";
import NextAuth from "next-auth/next";

/**
 * Handles the authentication route using NextAuth.
 * @param {NextAuthOptions} authOptions - The authentication options.
 * @returns {NextApiHandler} - The Next.js API handler.
 */

const handler = NextAuth(authOptions);
export {  handler as GET, handler as POST}