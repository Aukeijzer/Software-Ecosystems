"use client"

import { Navbar } from 'flowbite-react';
import logo from '../public/logo.png';
import Image from 'next/image'
import React, {useState, useEffect} from 'react'
import { useRouter } from 'next/navigation';
import Link from 'next/link'

/**
 * Renders a NavBar with clickable links to the main ecosystems.
 * @returns {JSX.Element} The rendered NavBar component.
 */

export default function NavBarTop(){
    //For now has the basepath 
    return(
        <Navbar data-cy='navBar' fluid rounded className='border-b-2 border-odinAccent bg-amber shadow-xl' >
            <Navbar.Brand as={Link} href="http://localhost:3000" >
                <Image
                    data-cy='navLogo'
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
        </Navbar>
    )   
}