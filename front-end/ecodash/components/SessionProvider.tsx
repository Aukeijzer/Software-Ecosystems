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
