import { Project } from "./ecoDataListModel";

export interface apiNamedEcosystemModel {
    id: number,
    name: string,
    displayName: string,
    description: string,
    projects: Project[],
    numberOfStars: number,
}


export type apiGetAllEcosystems = apiNamedEcosystemModel[];


export type apiResponse = apiGetAllEcosystems | apiNamedEcosystemModel;
