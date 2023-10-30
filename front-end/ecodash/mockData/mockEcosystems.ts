import { ecosystemModel } from "@/app/models/ecosystemModel";

export interface totalInformation {
    totalProjects : number,
    totalEcosystems: number,
    totalTopics: number,
}

export interface risingEcosystems {
    risingEcosystems : ecosystemModel[]
}

export interface ogranization {
    name: string,
    active_ecosystems: number,
    members: number,
}

export const topOrganizations: ogranization[] = [
    {name: "Microsoft", active_ecosystems: 3, members: 1200},
    {name: "Phillips", active_ecosystems: 5, members: 919},
    {name: "ASML", active_ecosystems: 2, members: 831}
]
    
export const totalInformation: totalInformation  =  {
    totalProjects: 2913,
    totalEcosystems: 3,
    totalTopics: 1405,
}
