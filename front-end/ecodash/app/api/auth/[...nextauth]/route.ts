/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

import { authOptions } from "@/utils/authOptions";
import NextAuth from "next-auth/next";

/**
 * Handles the authentication route using NextAuth.
 * @param {NextAuthOptions} authOptions - The authentication options.
 * @returns {NextApiHandler} - The Next.js API handler.
 */

const handler = NextAuth(authOptions);
export {  handler as GET, handler as POST}