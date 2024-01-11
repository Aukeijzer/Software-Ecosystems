"use client"

import { ecosystemDTO } from "@/app/interfaces/DTOs/ecosystemDTO";
import InfoCard from "./infoCard";

/**
 * Props for the EcosystemInformationData component.
 */
interface ecosystemInformationDataProps{
    ecosystem: ecosystemDTO
}

/**
 * Renders the data for an ecosystem information card.
 * 
 * @param {Object} props - The props for the component.
 * @param {ecosystemDTO} props.ecosystem - The ecosystem object containing the information to be displayed.
 * @returns {JSX.Element} The rendered component.
 */
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

