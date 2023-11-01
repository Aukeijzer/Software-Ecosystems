"use client"

import { ecosystemModel } from "@/app/models/ecosystemModel";
import { Card } from "flowbite-react";
import InfoCard from "./infoCard";

interface ecosystemInformationDataProps{
    ecosystem: ecosystemModel
}

export default function EcosystemInformationData(props : ecosystemInformationDataProps){
    
    const data = (
        <div className="flex flex-col">
            <span> {props.ecosystem.description} </span>
            <span> amount of projects: </span>
        </div>
    )
    
    return(
        <InfoCard title={props.ecosystem.displayName!} className="w-1/3" data={data} />
       

    )
}

