import { languageModel } from "./languageModel"

export interface ecosystemModel{
    displayName?: string,
    description?: string,
    numberOfStars?: number,
    topLanguages: languageModel[],
    subEcosystems: subEcosystem[]
}

export interface subEcosystem{
    topic: string,
    projectCount: number
}