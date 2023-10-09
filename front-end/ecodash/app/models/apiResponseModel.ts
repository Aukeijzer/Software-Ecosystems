import { programmingLanguage } from "../enums/ProgrammingLanguage"

//Todo:
//EcosystemByNameDTO
//Same as ecosystem model  backend
// Functie die een DTO omzet in een ecosystemModel

export interface ecosystemModel{
    id: string,
    name: string,
    displayName?: string,
    description?: string,
    projects: projectModel[],
    numberOfStars: number,
    topLanguages: languageModel[]
}

export interface projectModel {
    id: string,
    name: string,
    createdAt: string,
    ecosystem: string[],
    owner: string,
    description?: string,
    topics: string[],
    languages: languageModel[],
    totalSize?: number,
    readMe?: string,
    numberOfStars: number
}

export interface languageModel {
    id: string,
    language: programmingLanguage,
    percentage: number
}




