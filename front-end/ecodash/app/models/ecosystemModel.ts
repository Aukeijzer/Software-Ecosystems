import { projectModel } from "./projectModel"
import { languageModel } from "./languageModel"

export interface ecosystemModel{
    id: string,
    name: string,
    displayName?: string,
    description?: string,
    projects: projectModel[],
    numberOfStars: number,
    topLanguages: languageModel[]
}