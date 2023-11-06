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
import React, {useState, useEffect} from 'react'
import { IconContext } from 'react-icons'
import { BsMenuUp } from 'react-icons/bs';
import { useRouter } from 'next/navigation';
import { redirect } from 'next/navigation';
import { Router } from 'next/router';

export default function NavBarTop(){
    const [toggle, setToggle] = useState(false);
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

           {toggle? (null):(
                <div className="flex align-middle justify-center p-1 text-gray-800 dark:text-white">
                <IconContext.Provider value={{ size: '30' }}>
                    <BsMenuUp onClick={(e : Event) => setToggle(!toggle)} />
                </IconContext.Provider>
            </div>
           )} 
            
           
            {toggle? (
                <div className='flex flex-row gap-5'>
                    <Navbar.Link
                        href="/"
                    >   
                            Home
                    </Navbar.Link>
                
                    <Navbar.Link
                        href="http://agriculture.localhost:3000"
                    >
                        Agriculture
                        
                    </Navbar.Link>

                    <Navbar.Link
                        href="http://artificial-intelligence.localhost:3000"
                    >
                        AI
                    </Navbar.Link>

                    <Navbar.Link
                        href="http://quantum.localhost:3000"
                    >
                        Quantum
                    </Navbar.Link>
                </div>
                ) : null} 
        </Navbar>
    )   
}