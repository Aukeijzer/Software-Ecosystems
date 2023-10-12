import EcoMain from "@/components/ecoMain";
import { Metadata } from "next";
//This page has a dynamic path. Meaning you can put everything after /ecosytem/.... and it will go to that site and pass the .... as props to the page

interface ecosystemPageProps{
    params: { ecosystem : string},
}

//This whole block has functions that are defined by Next. Dont change any of the spelling! this will break things
// Do not enable params that are not defined as static
export const dynamicParams = false;
// Define static params
export function generateStaticParams() {
    return ["agriculture", "ai", "quantum"].map(ecosystem => ({ ecosystem }));
}
//Set title 
export function generateMetadata({params: {ecosystem}} : ecosystemPageProps): Metadata {
    return {
        title: ecosystem + " data page"
    }
}

export default function ecosystemPage({params: {ecosystem}}: ecosystemPageProps){
    return(
        <EcoMain ecosystem={ecosystem}/> 
    )
}