export interface ecoMainResponse {
    id: number,
    name: string,
    displayName: string,
    description: string,
    projects: project[],
    numberOfStars: number,
}

export interface project {
    id: number,
    name: string,
    about: string,
    owner: string,
    readMe: string,
    numberOfStars: number,
}