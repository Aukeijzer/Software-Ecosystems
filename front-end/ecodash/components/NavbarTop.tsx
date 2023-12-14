"use client"

import { Navbar } from 'flowbite-react';
import logo from '../public/logo.png';
import Image from 'next/image'
import React, {useState, useEffect} from 'react'
import { useRouter } from 'next/navigation';
import { useSession} from "next-auth/react";
import { ExtendedUser } from '@/app/utils/authOptions';
import Link from 'next/link'
import PopUpBox from './popUp';
import LoginBox from './loginBox';

/**
 * Renders a NavBar with clickable links to the main ecosystems.
 * @returns {JSX.Element} The rendered NavBar component.
 */

export default function NavBarTop(){
    const { data: session } = useSession()
    const user = session?.user as ExtendedUser;
    const Router = useRouter();
    //For now has the basepath 
    return(
        <Navbar data-cy='navBar' fluid rounded className='border-b-2 border-odinAccent bg-amber shadow-xl' >
            <Navbar.Brand as={Link} href={process.env.NEXT_PUBLIC_LOCAL_ADRESS} >
                <Image
                    data-cy='navLogo'
                    src={logo}
                    alt="SECODash logo"
                    width={40}
                    height={40}
                />
                <span className="self-center whitespace-nowrap text-l font-semibold dark:text-white">
                    SECODash
                </span>

            
            </Navbar.Brand>
           {session && <div>
                            <div className='flex flex-col'>
                                <Image src={session.user!.image!} 
                                    className='rounded-full ml-2'
                                    width={30}
                                    height={30}
                                    alt="Profile picture"
                                />
                               {user.userType}
                            </div>            
                            <button className="bg-gray-500 rounded-md p-1" onClick={() => Router.push('/api/auth/signout')}>Sign out</button>
                          
                        </div>}
            {!session && <div className='flex flex-col'> 
                            <span> Not logged in: </span>
                            <PopUpBox buttonText='Login'> <LoginBox/> </PopUpBox>
                        </div>}
        </Navbar>
    )   
}