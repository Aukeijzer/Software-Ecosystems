"use client"

import { ecosystemDTO } from "@/app/interfaces/DTOs/ecosystemDTO";
import InfoCard from "./infoCard";

interface ecosystemInformationDataProps{
    ecosystem: ecosystemDTO
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

