"use client"
import { useSession, signIn } from "next-auth/react";
import { useRouter } from "next/navigation";
import LoginBox from "@/components/loginBox";
export default function loginPage(){
    const { data: session, status } = useSession()
    const Router = useRouter();
    if(session){
        Router.push('/');
    }
    return(
        <div>
            <LoginBox />
        </div>
    )
}