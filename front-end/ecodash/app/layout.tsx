import NavBarTop from '@/components/NavbarTop'
import './globals.css'
import type { Metadata } from 'next'
import { Inter } from 'next/font/google'

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
      <script src="https://unpkg.com/flowbite@1.4.4/dist/flowbite.js"></script>
          <NavBarTop />
          <main>
            {children}
          </main>
      </body>
    </html>
  )
}



