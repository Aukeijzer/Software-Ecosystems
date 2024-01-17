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
        <Navbar data-cy='navBar' fluid  className='bg-navBar shadow-sm mb-5 pl-36 pr-36' >
            <Navbar.Brand as={Link} href="http://localhost:3000" >
                <Image
                    data-cy='navLogo'
                    src={logo}
                    alt="SECODash logo"
                    //className='mr-3 h-6 sm:h-9'
                    width={40}
                    height={40}
                />
                <span className="self-center whitespace-nowrap text-l font-semibold">
                    SECODash
                </span>
            </Navbar.Brand>
        </Navbar>
    )   
}