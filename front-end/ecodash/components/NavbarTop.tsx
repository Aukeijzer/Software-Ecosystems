"use client"
/*
NavBarTop exports:
- NavBarTop: renders a NavBar with clickable links to the main ecosystems
    - output: JSX.element

*/
import { Navbar } from 'flowbite-react';
import logo from '../public/logo.png';
import Image from 'next/image'
import React, {useState, useEffect} from 'react'
import { useRouter } from 'next/navigation';


export default function NavBarTop(){
    const router = useRouter();
    //For now has the basepath 
    return(
        <Navbar fluid rounded className='border-b-2 border-odinAccent bg-amber shadow-xl' >
            <Navbar.Brand onClick={() => router.push("http://localhost:3000")}>
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
        </Navbar>
    )   
}