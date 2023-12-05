import NavBarTop from '@/components/NavbarTop'
import './globals.css'
import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import { SessionProvider } from 'next-auth/react'
import { getServerSession } from "next-auth/next"
import NextAuthSessionProvider from '@/components/SessionProvider'

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  title: 'SECODash',
  description: 'The Software Ecosystem Dashboard'
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">  
          <body className={inter.className}>
             <NextAuthSessionProvider>
              <NavBarTop />
                <main>
                  {children}             
                </main>  
             </NextAuthSessionProvider>
          </body>
    </html>
  )
}



