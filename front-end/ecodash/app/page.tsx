"use client"
import LayoutHomePage from "@/components/layoutHomePage";
import { useSession, signIn } from "next-auth/react";
import { useRouter } from 'next/navigation'
import Image from 'next/image'
import PopUpBox from "@/components/popUp";
import LoginBox from "@/components/loginBox";

export default function Home() {
  const Router = useRouter()
  return(
    <div className="">
      <div className="">
        <LayoutHomePage /> 
      </div>
        
    </div>
  
  )
}