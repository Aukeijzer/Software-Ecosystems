"use client"
/*
NavBarTop exports:
- NavBarTop: renders a NavBar with clickable links to the main ecosystems
    - output: JSX.element

*/

import { Navbar } from 'flowbite-react';

import logo from '../public/logo.png';
import Image from 'next/image'
import Link from 'next/link';

export default function NavBarTop(){
    return(
        <Navbar fluid rounded className='border-b-2 border-odinAccent bg-amber shadow-xl' >
            <Navbar.Brand as={Link} href='/'>
                <Image
                    src={logo}
                    alt="SECODash logo"
                    //className='mr-3 h-6 sm:h-9'
                    width={80}
                    height={80}
                />
                <span className="self-center whitespace-nowrap text-xl font-semibold dark:text-white">
                    SECODash
                </span>

            </Navbar.Brand>
            
            <Navbar.Collapse>
                <Navbar.Link
                    href="/"
                   
                >
                        Home
                </Navbar.Link>

                <Navbar.Link
                    href="/ecosystem/agriculture"
                >
                    Agriculture
                    
                </Navbar.Link>

                <Navbar.Link
                    href="/ecosystem/artificial-intelligence"
                >
                    AI
                </Navbar.Link>

                <Navbar.Link
                    href="/ecosystem/quantum"
                >
                    Quantum
                </Navbar.Link>
            </Navbar.Collapse>
            <Navbar.Toggle /> 
            
        </Navbar>
    )   
}