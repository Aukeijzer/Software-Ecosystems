import LayoutEcosystem from "@/components/layoutEcosystem";
import { useSession } from "next-auth/react";

import { Metadata } from "next";
//This page has a dynamic path. Meaning you can put everything after /ecosytem/.... and it will go to that site and pass the .... as props to the page

interface ecosystemPageProps{
    params: { ecosystem : string},
}

//This whole block has functions that are defined by Next. Dont change any of the spelling! this will break things
// Do not enable params that are not defined as static
export const dynamicParams = false;
// Define static params
//So this is a list of all alowed ecosystems.
//If you want to add a new ecosystem you need to add it here
export function generateStaticParams() {
    return ["agriculture", "artificial-intelligence", "quantum"].map(ecosystem => ({ ecosystem }));
}                           
//Set title 
export function generateMetadata({params: {ecosystem}} : ecosystemPageProps): Metadata {
    return {
        title: ecosystem + " data page"
    }
}

export default function ecosystemPage({params: {ecosystem}}: ecosystemPageProps){

    
    return( 
        <div className="h-full">
            <LayoutEcosystem ecosystem={ecosystem} />
        </div>
       
    )
}