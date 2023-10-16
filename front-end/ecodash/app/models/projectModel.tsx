import { languageModel } from "./languageModel"

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
