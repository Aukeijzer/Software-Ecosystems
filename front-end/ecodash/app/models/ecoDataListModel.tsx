import Link from "next/link"
import { apiNamedEcosystemModel } from "./apiResponseModel"

export interface Project {
    id?: number,
    name: string,
    displayName: string,
    about?: string,
    owner?: string,
    readMe?: string,
    numberOfStars?: number,
}

export const renderProject = (project: Project) => {
    return(
        <div className="flex flex-row">
            <p className="flex mr-20 max-w-xl basis-56"> {project.displayName? project.displayName : project.name}</p>
            <p className="flex mr-20 max-w-xl basis-56"> About: {project.about? project.about : "not available"} </p>
            <p className="flex mr-20 max-w-5xl basis-56"> Readme: {project.readMe? project.readMe : "not available" }</p>
        </div>
    )
}

export const renderEcosystem = (ecosystem : apiNamedEcosystemModel) => {
    return(
        <div className="flex flex-row">
            <Link href={`/ecosystem/${ecosystem.name}`} > 
                <p> Name: {ecosystem.displayName? ecosystem.displayName : ecosystem.name} </p>
                <p> Projects: {ecosystem.projects?.length} </p>
            </Link>
        </div>
    )
}