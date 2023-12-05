"use client";
import { useSession, signIn } from "next-auth/react";
import logo from '../public/logo.png';
import Image from 'next/image'

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
            <div className="flex flex-col justify-center items-center mb-10">
                <h1 className="text-4xl text-center font-bold ">Welcome to EcoDash!</h1>
                <h2 className="text-2xl font-bold mt-5 mb-3">Login to continue</h2>
                <div className="bg-white rounded-full p-5 border-4 border-gray-900">
                    <Image
                        src={logo}
                        alt="SECODash logo"
                        //className='mr-3 h-6 sm:h-9'
                        width={150}
                        height={150}
                    />
                </div>
               
            </div>
            
            <div className="flex flex-row justify-center items-center gap-3 ml-32 mr-32 mb-5">
                <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => onClickFunc("github")}>
                    Login with Github
                </button>
                <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => onClickFunc("google")} >
                    Login with Google
                </button>
                
            </div>
        </div>
    )
}