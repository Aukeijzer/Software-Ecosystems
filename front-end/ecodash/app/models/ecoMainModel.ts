import { Project } from "./ecoDataListModel";

export interface ecoMainResponse {
    id: number,
    name: string,
    displayName: string,
    description: string,
    projects: Project[],
    numberOfStars: number,
}


