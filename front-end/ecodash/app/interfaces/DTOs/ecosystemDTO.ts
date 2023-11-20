import subEcosystemClass from "@/app/classes/subEcosystemClass"
import { languageDTO } from "./languageDTO"

export interface ecosystemDTO{
    displayName?: string,
    description?: string,
    numberOfStars?: number,
    topLanguages: languageDTO[],
    subEcosystems: subEcosystemDTO[]
}

export interface subEcosystemDTO{
    topic: string,
    projectCount: number
}

