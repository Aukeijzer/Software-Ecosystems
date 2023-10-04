

export interface apiNamedEcosystemModel {
    id?: number,
    name: string,
    displayName?: string,
    description?: string,
    projects?: Project[],
    numberOfStars?: number,
}

export interface Project {
    id?: number,
    name: string,
    displayName?: string,
    about?: string,
    owner?: string,
    readMe?: string,
    numberOfStars?: number,
}

export type apiGetAllEcosystems = apiNamedEcosystemModel[];


export type apiResponse = apiGetAllEcosystems | apiNamedEcosystemModel;
