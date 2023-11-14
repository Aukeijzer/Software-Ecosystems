"use client"

import { useRouter } from "next/navigation"

interface ecosystemButtonProps{
    ecosystem: string,
    projectCount: number,
    topics: number
}


export default function EcosystemButton(props: ecosystemButtonProps){
    const router = useRouter();

    return(
        <div>
            <ul>
                <li>
                    <b>projects</b> : {props.projectCount}
                </li>
                <li>
                    <b> topics</b>: {props.topics} 
                </li>
            </ul>
        </div>
    )
}