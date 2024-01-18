"use client"

import { Navbar } from 'flowbite-react';
import logo from '../public/logo.png';
import Image from 'next/image'
import React from 'react'
import { useRouter } from 'next/navigation';
import Link from 'next/link'
import PopUpBox from './popUpBox';
import LoginBox from './loginBox';
import { ExtendedUser } from '@/app/utils/authOptions';
import { useSession} from "next-auth/react";
/**
 * Renders the top navigation bar component. That contains login display and link to homepage.
 * @returns The JSX element representing the top navigation bar.
 */

export default function NavBarTop(){
    const { data: session } = useSession()
    const user = session?.user as ExtendedUser;
    const Router = useRouter();
    //For now has the basepath 
    return(
        <Navbar data-cy='navBar' fluid  className='bg-navBar shadow-sm mb-5 px-5 lg:px-36 md:px-20' >
            <Navbar.Brand as={Link}  href={process.env.NEXT_PUBLIC_LOCAL_ADRESS} >
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
            {/* If logged in display user profile image / userType and present sign out button */}
            {session && <div>
                            <div data-cy={"loggedInSelector"}className='flex flex-col'>
                                <Image src={session.user!.image!} 
                                    className='rounded-full ml-2'
                                    width={30}
                                    height={30}
                                    alt="Profile picture"
                                />
                               <b> {user.userType} </b>
                            </div>            
                            
                            <button className="bg-gray-500 rounded-md p-1" onClick={() => Router.push('/api/auth/signout')}>
                                Sign out
                            </button>
                        </div>}
            {/* If not logged in display login button */}
            {!session && <div className='flex flex-col'> 
                            <span> Not logged in: </span>
                            <PopUpBox buttonText='Login' data-cy="loginButton"> <LoginBox/> </PopUpBox>
                         </div>}
        </Navbar>
    )    
}