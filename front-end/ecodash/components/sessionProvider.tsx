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

"use client"

import React from "react";
import { SessionProvider } from 'next-auth/react';

type sessionProps = {
    children: React.ReactNode;
};

/**
 * Provides a session context using NextAuth.
 * 
 * @component
 * @param {Object} props - The component props.
 * @param {React.ReactNode} props.children - The child components.
 * @returns {React.ReactNode} The session provider component.
 */

function NextAuthSessionProvider({ children }: sessionProps) {
    return (
        <SessionProvider>
            {children}
        </SessionProvider>
    );
}

export default NextAuthSessionProvider;
