import { totalInformation } from "@/mockData/mockEcosystems";
import InfoCard from "./infoCard";

interface infoCardDataTotalProps{
    totalEcosystems: number,
    totalProjects: number,
    totalTopics: number,
    className: string
}

export default function InfoCardDataTotal(props: totalInformation){
    const data = (
        <div className="flex flex-col">
            <span> Total ecosystems: {props.totalEcosystems}</span>
            <span> Total projects: {props.totalProjects} </span>
            <span> Total topics: {props.totalTopics} </span>
        </div>
    )
    return(
        <InfoCard title="Total information" data={data} alert="this is mock data"/>
    )
}