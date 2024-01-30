/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

"use client";
import { signIn } from "next-auth/react";
import logo from '../public/logo.png';
import Image from 'next/image'

/**
 * Renders a login box component.
 * The login box allows users to sign in using different providers such as GitHub, Google, and LinkedIn.
 */

export default function LoginBox(){

    function onClickFunc(provider: string){
        //event.stopPropagation();
        signIn(provider);
    }
    function onClickDiv(event: React.MouseEvent<HTMLDivElement, MouseEvent>){
        event.stopPropagation();
    }

    return(
        <div className=" mt-5 z-10 " onClick={event => onClickDiv(event)} >
            <div className="flex flex-col justify-center items-center mb-10 text-white">
                <h1 className="text-4xl text-center font-bold ">Welcome to EcoDash!</h1>
                <h2 className="text-2xl font-bold mt-5 mb-3">Login to continue</h2>
                <div className="bg-white rounded-3xl p-5 border-4 border-gray-900">
                    <Image
                        src={logo}
                        alt="SECODash logo"
                        width={150}
                        height={150}
                    />
                </div>
               
            </div>
            
            <div className="flex flex-col justify-center items-center gap-3  mb-10 ml-32 mr-32">
                <button className="text-center w-52 bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded " onClick={() => onClickFunc("github")}>
                    Login with Github
                </button>
                <button className="text-center w-52 bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" data-cy="googleLoginButton" onClick={() => onClickFunc("google")} >
                    Login with Google
                </button>
                <button className="text-center w-52 bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => onClickFunc("linkedin")} >
                    Login with Linkedin
                </button>
            </div>
        </div>
    )
}