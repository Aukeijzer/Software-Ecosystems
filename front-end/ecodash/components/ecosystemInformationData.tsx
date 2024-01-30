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

"use client"

import { ecosystemDTO } from "@/interfaces/DTOs/ecosystemDTO";
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

