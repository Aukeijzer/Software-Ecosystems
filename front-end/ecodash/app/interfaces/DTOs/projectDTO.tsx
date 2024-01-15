import { languageDTO } from "./languageDTO"

export interface projectDTO {
    id: string,
    name: string,
    createdAt: string,
    ecosystem: string[],
    owner: string,
    description?: string,
    topics: string[],
    languages: languageDTO[],
    totalSize?: number,
    readMe?: string,
    numberOfStars: number
}